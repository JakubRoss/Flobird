using Application.Model.Attachments;
using FluentValidation;

namespace Application.Model.Validators
{
    public class AttachmentDtoValidator : AbstractValidator<AttachmentDto>
    {
        public AttachmentDtoValidator()
        {
            RuleFor(e => e.Name)
                .Custom((value, context) =>
                {
                    var updateAttachment= context.InstanceToValidate;

                    if (updateAttachment.Path ==null && updateAttachment.Name == null)
                            context.AddFailure("Name", "Name or path must be filled in");

                    if(value != null)
                    {
                        if (value.Length < 3)
                            context.AddFailure("Name", "Name is too short");
                        if (value.Length > 249)
                            context.AddFailure("Name", "Name is too long");
                    }

                });

            RuleFor(e => e.Path)
                .Custom((value, context) =>
                {
                    var updateAttachment = context.InstanceToValidate;

                    if (updateAttachment.Path == null && updateAttachment.Name == null)
                        context.AddFailure("Path", "Name or path must be filled in");

                    if (value != null)
                    {
                        if (value.Length < 5)
                            context.AddFailure("Path", "Path is too short");
                        if (value.Length > 249)
                            context.AddFailure("Path", "Path is too long");
                    }

                });
        }
    }
}
