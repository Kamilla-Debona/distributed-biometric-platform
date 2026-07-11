using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;
using Microsoft.EntityFrameworkCore;

namespace BiometricPlatform.Infrastructure.Persistence.Repositories;

public sealed class SubjectRepository
    : ISubjectRepository
{
    private readonly BiometricPlatformDbContext _dbContext;

    public SubjectRepository(
        BiometricPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Subject subject,
        CancellationToken cancellationToken)
    {
        await _dbContext.Subjects.AddAsync(
            subject,
            cancellationToken);
    }

    public Task<Subject?> GetByExternalSubjectIdAsync(
        string externalSubjectId,
        CancellationToken cancellationToken)
    {
        return _dbContext.Subjects
            .FirstOrDefaultAsync(
                x => x.ExternalSubjectId == externalSubjectId,
                cancellationToken);
    }

    public async Task<IReadOnlyCollection<Subject>> GetByGalleryIdAsync(
        Guid galleryId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Subjects
            .Where(subject => subject.GalleryId == galleryId)
            .ToListAsync(cancellationToken);
    }
}