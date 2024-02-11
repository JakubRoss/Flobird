using Application.Model.Card;
using FluentValidation;

namespace Application.Model.Validators
{
    public class CreateCardValidator : AbstractValidator<CreateCardDto>
    {
        public CreateCardValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(15);
        }
    }
}
