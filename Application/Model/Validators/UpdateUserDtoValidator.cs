using Application.Data;
using Application.Model.User;
using FluentValidation;

namespace Application.Model.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(DatabaseContext dbContext)
        {
            RuleFor(e => e.Password)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var updateUserDto = context.InstanceToValidate;
                        if (!(value.Equals(updateUserDto.ConfirmPassword) || value.Length < 6))
                            context.AddFailure("Password", "passwords are not equal or password is too short");
                    }
                });

            RuleFor(e => e.Email)
                .Custom((value, context) =>
                {
                    if(value!=null)
                    {
                        var emailInUse = dbContext.Users.Any(x => x.Email == value);
                        if (emailInUse)
                        {
                            context.AddFailure("Email", "Adres email jest zajety");
                        }
                    }
                });
            RuleFor(e => e.Login)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        var emailInUse = dbContext.Users.Any(x => x.Login == value);
                        if (emailInUse)
                        {
                            context.AddFailure("Login", "Login jest zajety");
                        }
                    }
                });
        }
    }
}
