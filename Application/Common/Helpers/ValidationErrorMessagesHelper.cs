using System;
using _netcore_2.Domain.Enum;

namespace _netcore_2.Application.Common.Helpers;

public class ValidationErrorMessagesHelper
{
    public static string GetMessage(ValidationErrors error)
    {
        return error switch
        {
            ValidationErrors.FirstNameRequired => "First name is required.",
            ValidationErrors.LastNameRequired => "Last name is required.",
            ValidationErrors.DateOfBirthRequired => "Date of birth is required.",
            ValidationErrors.InvalidGender => "Invalid gender value.",
            ValidationErrors.BirthPlaceRequired => "Birth place is required.",
            _ => "Validation error.",
        };
    }
}
