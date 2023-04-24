using Cabanoss.Core.Model.Comment;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class CommentDtoValidator : AbstractValidator<CommentDto>
    {
        public CommentDtoValidator()
        {
            RuleFor(t => t.Text)
                .NotEmpty()
                .NotNull()
                .MaximumLength(250)
                .MinimumLength(2);
            
        }
    }
}
