using System;

namespace _netcore_2.Application.DTOs;

public class PaginatedPersonResponseDTO
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<PersonDTO> Items { get; set; }

    public PaginatedPersonResponseDTO() { }

    public PaginatedPersonResponseDTO(
        int totalCount,
        int page,
        int pageSize,
        IEnumerable<PersonDTO> items
    )
    {
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
        Items = items;
    }
}
