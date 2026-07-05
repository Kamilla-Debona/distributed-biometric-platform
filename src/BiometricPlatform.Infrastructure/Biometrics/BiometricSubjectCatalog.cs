using BiometricPlatform.Application.Abstractions.Biometrics;
using BiometricPlatform.Application.Abstractions.Persistence;

namespace BiometricPlatform.Infrastructure.Biometrics;

public sealed class BiometricSubjectCatalog(
    ISubjectRepository subjectRepository)
    : IBiometricSubjectCatalog
{
    public async Task<IReadOnlyCollection<BiometricSubjectCatalogItem>> GetSubjectsByGalleryIdAsync(
        Guid galleryId,
        CancellationToken cancellationToken)
    {
        var subjects = await subjectRepository.GetByGalleryIdAsync(
            galleryId,
            cancellationToken);

        return subjects
            .Select(subject => new BiometricSubjectCatalogItem(
                subject.Id,
                subject.ExternalSubjectId))
            .ToList();
    }
}