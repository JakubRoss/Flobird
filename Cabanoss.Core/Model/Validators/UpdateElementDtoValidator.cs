using Cabanoss.Core.Model.Element;
using FluentValidation;

namespace Cabanoss.Core.Model.Validators
{
    public class UpdateElementDtoValidator : AbstractValidator<UpdateElementDto>
    {
        public UpdateElementDtoValidator()
        {
            RuleFor(e => e.Description)
                .Custom((value, context) =>
                {
                    var updateAttachment = context.InstanceToValidate;

                    if (updateAttachment.Description == null && updateAttachment.IsComplete == null)
                        context.AddFailure("Description", "Description or check must be filled in");

                    if (value != null)
                    {
                        if (value.Length < 3)
                            context.AddFailure("Name", "Name is too short");
                        if (value.Length > 249)
                            context.AddFailure("Name", "Name is too long");
                    }

                });
            RuleFor(e => e.IsComplete)
                .Custom((value, context) =>
                {
                    var updateAttachment = context.InstanceToValidate;

                    if (updateAttachment.Description == null && updateAttachment.IsComplete == null)
                        context.AddFailure("Description", "Description or check must be filled in");
                });
        }
    }
}
