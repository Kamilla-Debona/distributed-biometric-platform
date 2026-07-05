using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;
using Microsoft.EntityFrameworkCore;

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

    public Task<BiometricSample?> GetByEnrollmentIdAsync(Guid enrollmentId, CancellationToken cancellationToken)
    {
        return _dbContext.BiometricSamples
            .FirstOrDefaultAsync(x => x.EnrollmentId == enrollmentId, cancellationToken);
    }
    
    public Task<BiometricSample?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return _dbContext.BiometricSamples
            .FirstOrDefaultAsync(
                sample => sample.Id == id,
                cancellationToken);
    }
}