using BiometricPlatform.Domain.Identifications;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface IIdentificationCandidateRepository
{
    Task AddAsync(
        IdentificationCandidate candidate,
        CancellationToken cancellationToken);
}