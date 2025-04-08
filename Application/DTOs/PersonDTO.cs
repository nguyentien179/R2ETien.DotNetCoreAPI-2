namespace _netcore_2.Application.DTOs;

public record class PersonDTO(
    Guid Id,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Gender,
    string BirthPlace
);
