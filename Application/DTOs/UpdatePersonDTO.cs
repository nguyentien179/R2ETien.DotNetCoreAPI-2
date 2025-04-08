using System.ComponentModel.DataAnnotations;
using _netcore_2.Domain.Enum;

namespace _netcore_2.Application.DTOs;

public record class UpdatePersonDTO(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    Gender Gender,
    string BirthPlace
);
