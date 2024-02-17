using Application.Model.Board;
using FluentValidation;

namespace Application.Model.Validators
{
    public class CreateBoardDtoValidator : AbstractValidator<CreateBoardDto>
    {
        public CreateBoardDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(25);
        }
    }
}
