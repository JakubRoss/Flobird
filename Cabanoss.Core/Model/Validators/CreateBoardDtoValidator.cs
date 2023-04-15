using Cabanoss.Core.Data;
using Cabanoss.Core.Model.Board;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class CreateBoardDtoValidator : AbstractValidator<CreateBoardDto>
    {
        public CreateBoardDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3);
        }
    }
}
