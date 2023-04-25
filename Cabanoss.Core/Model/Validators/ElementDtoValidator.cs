using Cabanoss.Core.Model.Element;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
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
