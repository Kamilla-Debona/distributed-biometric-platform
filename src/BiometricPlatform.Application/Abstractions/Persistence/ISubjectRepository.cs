using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface ISubjectRepository
{
    Task AddAsync(Subject subject, CancellationToken cancellationToken);
}