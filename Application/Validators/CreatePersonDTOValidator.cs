using System;
using _netcore_2.Application.Common.Helpers;
using _netcore_2.Application.DTOs;
using _netcore_2.Domain.Enum;
using FluentValidation;

namespace _netcore_2.Application.Validators;

public class CreatePersonDTOValidator : AbstractValidator<CreatePersonDTO>
{
    public CreatePersonDTOValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessagesHelper.GetMessage(ValidationErrors.FirstNameRequired)
            );

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessagesHelper.GetMessage(ValidationErrors.LastNameRequired)
            );

        RuleFor(x => x.DateOfBirth)
            .Must(d => d != default(DateTime))
            .WithMessage(
                ValidationErrorMessagesHelper.GetMessage(ValidationErrors.DateOfBirthRequired)
            );

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage(ValidationErrorMessagesHelper.GetMessage(ValidationErrors.InvalidGender));

        RuleFor(x => x.BirthPlace)
            .NotEmpty()
            .WithMessage(
                ValidationErrorMessagesHelper.GetMessage(ValidationErrors.BirthPlaceRequired)
            );
    }
}
