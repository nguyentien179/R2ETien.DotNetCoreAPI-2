using System;
using _netcore_2.Entities;

namespace _netcore_2.Application.Interface;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(Guid id);
    Task CreateAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(Person person);
    IQueryable<Person> GetQueryable();
}
