using System;
using FluentValidation;

namespace _netcore_2.Application.Validators;

public static class ValidationExtensions
{
    public static async Task<IResult?> Validate<T>(this T dto, IValidator<T> validator)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = result
                .Errors.GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            return Results.ValidationProblem(errors);
        }

        return null;
    }
}
