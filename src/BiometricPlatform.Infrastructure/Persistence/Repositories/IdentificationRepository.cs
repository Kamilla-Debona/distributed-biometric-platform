using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Identifications;
using Microsoft.EntityFrameworkCore;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class IdentificationRepository(BiometricPlatformDbContext dbContext) : IIdentificationRepository
{
    public async Task AddAsync(
        Identification identification,
        CancellationToken cancellationToken)
    {
        await dbContext.Identifications.AddAsync(
            identification,
            cancellationToken);
    }

    public Task<Identification?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return dbContext.Identifications
            .FirstOrDefaultAsync(
                identification => identification.Id == id,
                cancellationToken);
    }
}