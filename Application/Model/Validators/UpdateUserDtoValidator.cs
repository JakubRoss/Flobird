using Application.Model.User;
using Domain.Repositories;
using FluentValidation;

namespace Application.Model.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(IUserRepository userRepository)
        {
            RuleFor(e => e.Password)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var updateUserDto = context.InstanceToValidate;
                        if (!(value.Equals(updateUserDto.ConfirmPassword) || value.Length < 8))
                            context.AddFailure("Password", "passwords are not equal or password is too short");
                    }
                });

            RuleFor(e => e.Email)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var emailInUse = userRepository.GetFirstAsync(x => x.Email == value).Result;
                        if (emailInUse != null)
                        {
                            context.AddFailure("Email", "Address email is taken");
                        }
                    }
                });
            RuleFor(e => e.Login)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var emailInUse = userRepository.GetFirstAsync(x => x.Login == value).Result;
                        if (emailInUse != null)
                        {
                            context.AddFailure("Login", "Login is taken");
                        }
                    }
                })
                .MinimumLength(4)
                .MaximumLength(20);
        }
    }
}
