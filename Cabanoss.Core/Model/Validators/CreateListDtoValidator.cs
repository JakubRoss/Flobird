using Cabanoss.Core.Model.List;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class CreateListDtoValidator : AbstractValidator<CreateListDto>
    {
        public CreateListDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(15);
        }
    }
}
