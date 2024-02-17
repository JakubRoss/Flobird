using Application.Model.Element;
using FluentValidation;

namespace Application.Model.Validators
{
    public class UpdateElementDtoValidator : AbstractValidator<UpdateElementDto>
    {
        public UpdateElementDtoValidator()
        {
            RuleFor(e => e.Description)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        if (value.Length < 3)
                            context.AddFailure("Name", "Name is too short");
                        if (value.Length > 249)
                            context.AddFailure("Name", "Name is too long");
                    }
                });
        }
    }
}
