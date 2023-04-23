using Cabanoss.Core.Model.Task;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class TaskDtoValidator : AbstractValidator<TaskDto>
    {
        public TaskDtoValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(15);
        }
    }
}
