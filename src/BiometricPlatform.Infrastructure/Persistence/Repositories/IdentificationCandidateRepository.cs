using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Identifications;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyCollection<IdentificationCandidate>> GetByIdentificationIdAsync(
        Guid identificationId,
        CancellationToken cancellationToken)
    {
        return await dbContext.IdentificationCandidates
            .Where(candidate => candidate.IdentificationId == identificationId)
            .OrderBy(candidate => candidate.Rank)
            .ToListAsync(cancellationToken);
    }
}