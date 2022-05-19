using RA.Entitys;
namespace RA
{
    
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _db;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
          _db = dbContext;
        }
        public void Seed()
        {
            if(_db.Database.CanConnect())
            {
                if(!_db.Roles.Any())
                {
                 var roles = GetRoles();
                    _db.Roles.AddRange(roles);
                    _db.SaveChanges();
                }
                if(!_db.Restaurants.Any())
                {

                }
            }
        }
        private IEnumerable<Role>GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role() { Name = "User"},
                new Role() { Name ="Manager"},
                new Role() { Name ="Admin"},
            };
            return roles;
        }
    }
}
