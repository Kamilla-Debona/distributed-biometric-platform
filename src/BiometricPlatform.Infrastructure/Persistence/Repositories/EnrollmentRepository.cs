using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Enrollments;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class EnrollmentRepository : IEnrollmentRepository
{
    private readonly BiometricPlatformDbContext _dbContext;

    public EnrollmentRepository(BiometricPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken)
    {
        await _dbContext.Enrollments.AddAsync(enrollment, cancellationToken);
    }
}