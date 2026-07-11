using BiometricPlatform.Domain.Identity;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IPersonRepository
{
    Task AddAsync(Person person, CancellationToken cancellationToken);

    Task<Person?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}