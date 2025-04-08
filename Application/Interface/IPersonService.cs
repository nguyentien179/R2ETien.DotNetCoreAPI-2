using System;
using _netcore_2.Application.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace _netcore_2.Application.Interface;

public interface IPersonService
{
    Task<Results<Ok<PaginatedPersonResponseDTO>, BadRequest<string>>> GetAllAsync(
        int page = 1,
        int pageSize = 10,
        string? name = null,
        string? gender = null,
        string? birthPlace = null
    );
    Task<PersonDTO> GetByIdAsync(Guid id);
    Task CreateAsync(CreatePersonDTO personDto);
    Task UpdateAsync(Guid id, UpdatePersonDTO personDto);
    Task DeleteAsync(Guid id);
}
