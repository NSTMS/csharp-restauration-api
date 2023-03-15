using FluentValidation;
using restAPI.Entities;
using System.Linq;

namespace restAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Email).Custom((val, ctx) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == val);
                if(emailInUse)
                {
                    ctx.AddFailure("Email", "Email jest już w bazie danych");
                }
            });

            RuleFor(x=> x.Password).NotEmpty().MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Nationality).NotEmpty();
        }
    }
}
