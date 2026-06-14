using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IBiometricSampleRepository
{
    Task AddAsync(BiometricSample biometricSample, CancellationToken cancellationToken);
}