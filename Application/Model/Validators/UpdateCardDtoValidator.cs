﻿using Application.Model.Card;
using FluentValidation;

namespace Application.Model.Validators
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

                        if (value.Length < 3 || string.IsNullOrEmpty(value) || value.Length > 25)
                            context.AddFailure("Name", "Name is too short/long or is empty");
                    }
                });
            RuleFor(e => e.Description)
                .Custom((value, context) =>
                {
                    if (value != null)
                    {

                        if (value.Length > 250)
                            context.AddFailure("Name", "the description can contain a maximum of 250 characters");
                    }
                });
        }
    }
}
