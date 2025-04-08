using System;
using System.ComponentModel.DataAnnotations;
using _netcore_2.Domain.Enum;

namespace _netcore_2.Application.DTOs;

public record class CreatePersonDTO(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] DateTime DateOfBirth,
    [Required] [EnumDataType(typeof(Gender))] Gender Gender,
    [Required] string BirthPlace
);
