using System;
using _netcore_2.Domain.Enum;
using _netcore_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace _netcore_2.Infrastructure.Persistence;

public class PersonContext(DbContextOptions<PersonContext> options) : DbContext(options)
{
    public DbSet<Person> Person => Set<Person>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public void SeedData()
    {
        if (!Person.Any())
        {
            Person.AddRange(
                new Person
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1980, 1, 15),
                    Gender = Gender.MALE,
                    BirthPlace = "New York",
                },
                new Person
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1985, 5, 22),
                    Gender = Gender.FEMALE,
                    BirthPlace = "London",
                },
                new Person
                {
                    FirstName = "David",
                    LastName = "Lee",
                    DateOfBirth = new DateTime(1992, 10, 8),
                    Gender = Gender.MALE,
                    BirthPlace = "Tokyo",
                },
                new Person
                {
                    FirstName = "Emily",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1998, 3, 12),
                    Gender = Gender.FEMALE,
                    BirthPlace = "Paris",
                },
                new Person
                {
                    FirstName = "Michael",
                    LastName = "Wilson",
                    DateOfBirth = new DateTime(1975, 7, 28),
                    Gender = Gender.MALE,
                    BirthPlace = "Berlin",
                }
            );
        }
    }
}
