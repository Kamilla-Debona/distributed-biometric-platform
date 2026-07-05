using BiometricPlatform.Domain.Identifications;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IIdentificationRepository
{
    Task AddAsync(
        Identification identification,
        CancellationToken cancellationToken);

    Task<Identification?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}

