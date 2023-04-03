using Cabanoss.Core.Model.User;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(p => p.Password)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.Login)
                .NotEmpty()
                .NotNull();
        }
    }
}
