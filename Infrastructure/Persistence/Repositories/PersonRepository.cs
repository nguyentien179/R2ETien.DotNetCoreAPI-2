using System;
using _netcore_2.Application.Interface;
using _netcore_2.Entities;
using Microsoft.EntityFrameworkCore;

namespace _netcore_2.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonContext _context;

    public PersonRepository(PersonContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.Person.AsNoTracking().ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _context.Person.FindAsync(id);
    }

    public async Task CreateAsync(Person person)
    {
        await _context.Person.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Person person)
    {
        _context.Person.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person person)
    {
        _context.Person.Remove(person);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Person> GetQueryable()
    {
        return _context.Person;
    }
}
