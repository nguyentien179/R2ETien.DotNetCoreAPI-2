using System;
using System.ComponentModel.DataAnnotations;
using _netcore_2.Domain.Enum;

namespace _netcore_2.Entities;

public class Person
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public required string BirthPlace { get; set; }
}
