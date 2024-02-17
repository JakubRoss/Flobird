using Application.Model.List;
using FluentValidation;

namespace Application.Model.Validators
{
    public class CreateListDtoValidator : AbstractValidator<CreateListDto>
    {
        public CreateListDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(25);
        }
    }
}
