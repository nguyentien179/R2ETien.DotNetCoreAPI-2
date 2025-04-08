using System;
using System.ComponentModel.DataAnnotations;
using _netcore_2.Application.DTOs;
using _netcore_2.Application.Interface;
using _netcore_2.Application.Validators;
using FluentValidation;

namespace _netcore_2.Presentation.Endpoints;

public static class PersonEndpoints
{
    public static RouteGroupBuilder MapPersonEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/person");

        group
            .MapGet(
                "/",
                async (
                    int? page,
                    int? pageSize,
                    string? name,
                    string? gender,
                    string? birthPlace,
                    IPersonService personService
                ) =>
                {
                    return await personService.GetAllAsync(
                        page: page ?? 1,
                        pageSize: pageSize ?? 5,
                        name: name,
                        gender: gender,
                        birthPlace: birthPlace
                    );
                }
            )
            .Produces<PaginatedPersonResponseDTO>();

        group
            .MapGet(
                "/{id:guid}",
                async (Guid id, IPersonService personService) =>
                {
                    var person = await personService.GetByIdAsync(id);
                    return person is not null
                        ? Results.Ok(person)
                        : Results.NotFound($"Person with ID {id} not found.");
                }
            )
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound);
        ;

        group
            .MapPost(
                "/",
                async (
                    CreatePersonDTO createPersonDTO,
                    IPersonService personService,
                    IValidator<CreatePersonDTO> validator
                ) =>
                {
                    var validation = await createPersonDTO.Validate(validator);
                    if (validation is not null)
                        return validation;
                    await personService.CreateAsync(createPersonDTO);
                    return Results.Ok(createPersonDTO);
                }
            )
            .Produces<CreatePersonDTO>(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest);
        ;

        group
            .MapPut(
                "/{id:guid}",
                async (
                    Guid id,
                    UpdatePersonDTO updatePersonDTO,
                    IValidator<UpdatePersonDTO> validator,
                    IPersonService personService
                ) =>
                {
                    var validation = await updatePersonDTO.Validate(validator);
                    if (validation is not null)
                        return validation;
                    await personService.UpdateAsync(id, updatePersonDTO);
                    return Results.Ok("updated");
                }
            )
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound);

        group
            .MapDelete(
                "/{id:guid}",
                async (Guid id, IPersonService personService) =>
                {
                    await personService.DeleteAsync(id);
                    return Results.Ok("deleted");
                }
            )
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status404NotFound);
        ;
        return group;
    }
}
