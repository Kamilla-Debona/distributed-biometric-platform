using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class BiometricSampleRepository : IBiometricSampleRepository
{
    private readonly BiometricPlatformDbContext _dbContext;

    public BiometricSampleRepository(BiometricPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(BiometricSample biometricSample, CancellationToken cancellationToken)
    {
        await _dbContext.BiometricSamples.AddAsync(biometricSample, cancellationToken);
    }
}