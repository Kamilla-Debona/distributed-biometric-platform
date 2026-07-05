using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Identifications;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class IdentificationCandidateRepository(BiometricPlatformDbContext dbContext)
    : IIdentificationCandidateRepository
{
    public async Task AddAsync(
        IdentificationCandidate candidate,
        CancellationToken cancellationToken)
    {
        await dbContext.IdentificationCandidates.AddAsync(
            candidate,
            cancellationToken);
    }
}