using Application.Model.Board;
using FluentValidation;

namespace Application.Model.Validators
{
    public class UpdateBoardDtoValidator : AbstractValidator<UpdateBoardDto>
    {
        public UpdateBoardDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(15);
        }
    }
}
