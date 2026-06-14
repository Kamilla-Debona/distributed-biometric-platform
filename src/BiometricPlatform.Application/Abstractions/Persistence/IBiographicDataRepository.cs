using BiometricPlatform.Domain.Identity;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IBiographicDataRepository
{
    Task AddAsync(BiographicData biographicData, CancellationToken cancellationToken);
}