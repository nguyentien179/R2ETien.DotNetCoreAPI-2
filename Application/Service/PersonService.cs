using System;
using _netcore_2.Application.DTOs;
using _netcore_2.Application.Interface;
using _netcore_2.Domain.Enum;
using _netcore_2.Entities;
using _netcore_2.Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace _netcore_2.Application.Service;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Results<Ok<PaginatedPersonResponseDTO>, BadRequest<string>>> GetAllAsync(
        int page = 1,
        int pageSize = 10,
        string? name = null,
        string? gender = null,
        string? birthPlace = null
    )
    {
        if (page < 1 || pageSize < 1)
        {
            return TypedResults.BadRequest("Invalid page or pageSize.");
        }

        try
        {
            var query = _personRepository.GetQueryable();

            query = ApplyFilters(query, name, gender, birthPlace);

            int totalCount = await query.CountAsync();

            var persons = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var personDTOs = persons.Select(p => p.ToDTO());

            var result = new PaginatedPersonResponseDTO
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Items = personDTOs,
            };

            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
            return TypedResults.BadRequest("An error occurred.");
        }
    }

    private IQueryable<Person> ApplyFilters(
        IQueryable<Person> query,
        string? name,
        string? gender,
        string? birthPlace
    )
    {
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name));
        }

        if (
            !string.IsNullOrWhiteSpace(gender)
            && Enum.TryParse<Gender>(gender, true, out var parsedGender)
        )
        {
            query = query.Where(p => p.Gender == parsedGender);
        }

        if (!string.IsNullOrEmpty(birthPlace))
        {
            query = query.Where(p => p.BirthPlace.Contains(birthPlace));
        }

        return query;
    }

    public async Task<PersonDTO> GetByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        return person?.ToDTO() ?? throw new KeyNotFoundException($"Person with ID {id} not found.");
    }

    public async Task CreateAsync(CreatePersonDTO createPersonDTO)
    {
        if (!Enum.IsDefined(typeof(Gender), createPersonDTO.Gender))
        {
            throw new ArgumentException("Invalid Gender value.");
        }
        await _personRepository.CreateAsync(createPersonDTO.ToEnity());
    }

    public async Task UpdateAsync(Guid id, UpdatePersonDTO updatePersonDTO)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {id} not found.");
        }

        person.FirstName = updatePersonDTO.FirstName;
        person.LastName = updatePersonDTO.LastName;
        person.DateOfBirth = updatePersonDTO.DateOfBirth;
        person.Gender = updatePersonDTO.Gender;
        person.BirthPlace = updatePersonDTO.BirthPlace;

        await _personRepository.UpdateAsync(person);
    }

    public async Task DeleteAsync(Guid id)
    {
        var person =
            await _personRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Person with ID {id} not found.");
        await _personRepository.DeleteAsync(person);
    }
}
