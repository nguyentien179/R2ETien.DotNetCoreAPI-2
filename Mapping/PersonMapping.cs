using System;
using _netcore_2.Application.DTOs;
using _netcore_2.Entities;

namespace _netcore_2.Mapping;

public static class PersonMapping
{
    public static PersonDTO ToDTO(this Person person)
    {
        return new PersonDTO(
            person.Id,
            person.FirstName,
            person.LastName,
            person.DateOfBirth,
            person.Gender.ToString(),
            person.BirthPlace
        );
    }

    public static Person ToEnity(this CreatePersonDTO createPersonDTO)
    {
        return new Person
        {
            Id = Guid.NewGuid(),
            FirstName = createPersonDTO.FirstName,
            LastName = createPersonDTO.LastName,
            DateOfBirth = createPersonDTO.DateOfBirth,
            Gender = createPersonDTO.Gender,
            BirthPlace = createPersonDTO.BirthPlace,
        };
    }
}
