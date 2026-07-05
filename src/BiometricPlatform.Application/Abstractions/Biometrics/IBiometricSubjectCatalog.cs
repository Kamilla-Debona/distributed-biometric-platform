namespace BiometricPlatform.Application.Abstractions.Biometrics;

public interface IBiometricSubjectCatalog
{
    Task<IReadOnlyCollection<BiometricSubjectCatalogItem>> GetSubjectsByGalleryIdAsync(
        Guid galleryId,
        CancellationToken cancellationToken);
}