using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RA.Authorization;
using RA.Entitys;
using RA.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RA.services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);

        PagedResult<RestaurantDto> GetAll(RestaurantQuery query);

        RestaurantDto GetById(int id);

        void Delete(int id);

        void Update(int id, UbdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _db = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _db.Restaurants
           .Include(r => r.Address)
           .Include(r => r.Dishes)
           .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
                throw new NotFundExcept("Restaurant not fund");

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public PagedResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var baseQuery =_db
           .Restaurants
           .Include(r => r.Address)
           .Include(r => r.Dishes)
           .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
           || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            if(!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Restaurant, object>>>
                    {
                        { nameof(Restaurant.Name), r=>r.Name},
                        { nameof(Restaurant.Description), r=>r.Description},
                        { nameof(Restaurant.Category), r=>r.Category}
                    };

                var SelectedColumn = columnsSelectors[query.SortBy];
                
                baseQuery = query.SortDirection == SortDirection.ASC 
                    ?baseQuery.OrderBy(SelectedColumn)
                    : baseQuery.OrderByDescending (SelectedColumn);

            }

            var restaurant = baseQuery 
           .Skip(query.PageSize * (query.PageNumber -1))
           .Take(query.PageSize)
           .ToList();

            var totalitemsCount = baseQuery.Count();

            var RestaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurant);

            var result = new PagedResult<RestaurantDto>(RestaurantDtos, totalitemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = _userContextService.GetVGetUserId;
            _db.Restaurants.Add(restaurant);
            _db.SaveChanges();
            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id {id} Deleted action invoked");

            var restaurant = _db.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
                throw new NotFundExcept("Restaurant not fund");

            var aythorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
             new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!aythorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _db.Restaurants.Remove(restaurant);
            _db.SaveChanges();
        }

        public void Update(int id, UbdateRestaurantDto dto)
        {
            var restaurant = _db.Restaurants
                          .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
                throw new NotFundExcept("Restaurant not fund");

            var aythorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!aythorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            _db.SaveChanges();
        }
    }
}