using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Identity;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class BiographicDataRepository : IBiographicDataRepository
{
    private readonly BiometricPlatformDbContext _dbContext;

    public BiographicDataRepository(BiometricPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(BiographicData biographicData, CancellationToken cancellationToken)
    {
        await _dbContext.BiographicData.AddAsync(biographicData, cancellationToken);
    }
}