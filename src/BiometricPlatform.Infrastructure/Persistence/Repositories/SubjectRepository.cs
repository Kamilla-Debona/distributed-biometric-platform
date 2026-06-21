using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class SubjectRepository
    : ISubjectRepository
{
    private readonly BiometricPlatformDbContext _dbContext;

    public SubjectRepository(
        BiometricPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Subject subject,
        CancellationToken cancellationToken)
    {
        await _dbContext.Subjects.AddAsync(
            subject,
            cancellationToken);
    }
}