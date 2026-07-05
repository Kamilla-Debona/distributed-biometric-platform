using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IBiometricSampleRepository
{
    Task AddAsync(
        BiometricSample biometricSample,
        CancellationToken cancellationToken);

    Task<BiometricSample?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<BiometricSample?> GetByEnrollmentIdAsync(
        Guid enrollmentId,
        CancellationToken cancellationToken);
}