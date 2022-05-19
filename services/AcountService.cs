using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RA.Controllers;
using RA.Entitys;
using RA.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RA.services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GeneralJwt(LoginDto dto);
    }
    public class AcountService: IAccountService
    {
        private readonly RestaurantDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSttetings _authenticationStetings;

        public AcountService(RestaurantDbContext context, IPasswordHasher<User>passwordHasher, AuthenticationSttetings authenticationStetings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationStetings = authenticationStetings;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
               FirstName = "aaaa",
               LastName = "sdsdsd",
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public string GeneralJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u=>u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");

            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role,$"{user.Role.Name}"),
                new Claim("Dataof birth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
               
            };

            if(!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(
                    new Claim("Nationality", user.Nationality)
                    );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationStetings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationStetings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationStetings.JwTIssuer,
                _authenticationStetings.JwTIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

    }
}
