using Application.Model.Element;
using FluentValidation;

namespace Application.Model.Validators
{
    public class ElementDtoValidator : AbstractValidator<ElementDto>
    {
        public ElementDtoValidator()
        {
            RuleFor(d => d.Description)
                .MinimumLength(3)
                .MaximumLength(249);
        }
    }
}
