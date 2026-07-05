namespace BiometricPlatform.Application.Abstractions.Biometrics;

public interface IBiometricEngine
{
    Task<CreateSubjectResult> CreateSubjectAsync(
        string imagePath,
        CancellationToken cancellationToken);

    Task<SearchResult> SearchAsync(
        string imagePath,
        Guid galleryId,
        CancellationToken cancellationToken);

    Task DeleteSubjectAsync(
        string externalSubjectId,
        CancellationToken cancellationToken);
}