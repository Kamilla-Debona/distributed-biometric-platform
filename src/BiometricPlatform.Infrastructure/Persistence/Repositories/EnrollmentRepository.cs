using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Enrollments;
using Microsoft.EntityFrameworkCore;

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

    public Task<Enrollment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Enrollments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}