using Microsoft.AspNetCore.Mvc;
using RA.Models;
using RA.services;

namespace RA.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountConrtoller : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountConrtoller(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();

        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GeneralJwt(dto);
            return Ok(token);
        }
    }
}
