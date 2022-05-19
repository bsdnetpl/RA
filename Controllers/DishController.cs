using Microsoft.AspNetCore.Mvc;
using RA.Models;
using RA.services;

namespace RA.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController:ControllerBase
    {
        private readonly IDishService _dishservice;
        public DishController(IDishService dishService)
        {
            _dishservice = dishService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId, [FromBody] CreateDishDto dto)
        {
            var newDishId = _dishservice.Create(restaurantId,dto);
            return Created($"api/restaurant{restaurantId}/dish/{newDishId}",null);
        }
        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId,[FromRoute] int dishId)
        {
            var dish = _dishservice.GetById(restaurantId,dishId);
            return Ok(dish);
        }
        public ActionResult<DishDto> Get([FromRoute] int restaurantId)
        {
            var result = _dishservice.GetAll(restaurantId);
            return Ok(result);
        }
        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _dishservice.RemoveAll(restaurantId);
            return NoContent();

        }
    }
}
