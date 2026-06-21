using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class BiometricTemplateRepository
    : IBiometricTemplateRepository
{
    private readonly BiometricPlatformDbContext _dbContext;

    public BiometricTemplateRepository(
        BiometricPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        BiometricTemplate biometricTemplate,
        CancellationToken cancellationToken)
    {
        await _dbContext.BiometricTemplates.AddAsync(
            biometricTemplate,
            cancellationToken);
    }
}