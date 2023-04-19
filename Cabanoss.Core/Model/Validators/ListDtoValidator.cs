using Cabanoss.Core.Model.List;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class ListDtoValidator : AbstractValidator<ListDto>
    {
        public ListDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3);
        }
    }
}
