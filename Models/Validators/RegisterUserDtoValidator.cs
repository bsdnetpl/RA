using FluentValidation;
using RA.Entitys;
using Microsoft.EntityFrameworkCore;

namespace RA.Models.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto> 
    {
        public RegisterUserDtoValidator()
        {
            RuleFor( x => x.Email).NotEmpty().EmailAddress();
            RuleFor( x => x.Password).MinimumLength(6);
            RuleFor(x =>x.ConformPassword).Equal(e => e.Password);
            //RuleFor(x => x.Email)
            //    .Custom((value, context) =>
            //    {
            //        var emailInUse =  dbContext.Users.Any(u => u.Email == value);
            //        if (emailInUse)
            //        {

            //        }

            //    });
        }
    }
}
