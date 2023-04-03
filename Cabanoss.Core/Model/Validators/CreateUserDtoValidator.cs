using Cabanoss.Core.Data;
using Cabanoss.Core.Model.User;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator(CabanossDbContext dbContext)
        {
            RuleFor(e => e.Password)
                .MinimumLength(6)
                .Equal(x => x.ConfirmPassword);

            RuleFor(e => e.Email)
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Email == value);
                    if(emailInUse)
                    {
                        context.AddFailure("Email", "Email Adress is taken");
                    }
                });

            RuleFor(e => e.Login)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Login == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Login", "Login is taken");
                    }
                })
                .MinimumLength(4);
        }
    }
}
