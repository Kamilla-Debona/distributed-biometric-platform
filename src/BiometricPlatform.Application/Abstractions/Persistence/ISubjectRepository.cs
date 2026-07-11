using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Application.Abstractions.Persistence;

public interface ISubjectRepository
{
    Task AddAsync(Subject subject, CancellationToken cancellationToken);

    Task<Subject?> GetByExternalSubjectIdAsync(
        string externalSubjectId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Subject>> GetByGalleryIdAsync(
        Guid galleryId,
        CancellationToken cancellationToken);
}