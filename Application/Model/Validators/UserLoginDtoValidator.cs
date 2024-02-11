using Application.Model.User;
using FluentValidation;

namespace Application.Model.Validators
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
