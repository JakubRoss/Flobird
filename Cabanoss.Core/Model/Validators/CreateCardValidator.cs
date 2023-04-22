using Cabanoss.Core.Model.Card;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class CreateCardValidator : AbstractValidator<CreateCardDto>
    {
        public CreateCardValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3);
        }
    }
}
