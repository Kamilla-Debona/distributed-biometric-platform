using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class PersonRepository : IPersonRepository
{
    private readonly BiometricPlatformDbContext _dbContext;

    public PersonRepository(BiometricPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
        await _dbContext.Persons.AddAsync(person, cancellationToken);
    }

    public Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Persons
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}