using Application.Model.User;
using Domain.Repositories;
using FluentValidation;

namespace Application.Model.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator(IUserRepository userRepository)
        {
            RuleFor(e => e.Password)
                .MinimumLength(8)
                .Equal(x => x.ConfirmPassword);

            RuleFor(e => e.Email)
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = userRepository.GetFirstAsync(x => x.Email == value).Result;
                    if (emailInUse != null)
                    {
                        context.AddFailure("Email", "Email Address is taken");
                    }
                });

            RuleFor(e => e.Login)
                .Custom((value, context) =>
                {
                    var emailInUse = userRepository.GetFirstAsync(x => x.Login == value).Result;
                    if (emailInUse != null)
                    {
                        context.AddFailure("Login", "Login is taken");
                    }
                })
                .MinimumLength(4);
        }
    }
}
