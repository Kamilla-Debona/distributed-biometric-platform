using BiometricPlatform.Domain.Enrollments;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IEnrollmentRepository
{
    Task AddAsync(
        Enrollment enrollment,
        CancellationToken cancellationToken);

    Task<Enrollment?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}