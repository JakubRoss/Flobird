using Cabanoss.Core.Model.Task;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<TaskDto>
    {
        public CreateTaskDtoValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(15);
        }

    }
}
