using Cabanoss.Core.Model.Card;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Cabanoss.Core.Model.Validators
{
    public class UpdateCardDtoValidator : AbstractValidator<UpdateCardDto>
    {
        public UpdateCardDtoValidator()
        {
            RuleFor(e => e.Name)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {
                        
                        if (value.Length<3 || value.IsNullOrEmpty() ||value.Length>15)
                            context.AddFailure("Name", "Name is too short/long or is empty");
                    }
                });
            RuleFor(e => e.Description)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {

                        if (value.Length >250)
                            context.AddFailure("Name", "the description can contain a maximum of 250 characters");
                    }
                });
        }
    }
}
