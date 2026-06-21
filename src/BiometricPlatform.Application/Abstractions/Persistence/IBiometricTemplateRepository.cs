using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IBiometricTemplateRepository
{
    Task AddAsync(BiometricTemplate biometricTemplate, CancellationToken cancellationToken);
}