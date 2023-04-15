using Cabanoss.Core.Model.Board;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class UpdateBoardDtoValidator :AbstractValidator<UpdateBoardDto>
    {
        public UpdateBoardDtoValidator() 
        {
            RuleFor(p=>p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3);
        }
    }
}
