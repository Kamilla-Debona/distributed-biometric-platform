using BiometricPlatform.Application.Abstractions.Biometrics;

namespace BiometricPlatform.Infrastructure.Biometrics;

public sealed class FakeBiometricEngine(
    IBiometricSubjectCatalog subjectCatalog)
    : IBiometricEngine
{
    public Task<CreateSubjectResult> CreateSubjectAsync(
        string imagePath,
        CancellationToken cancellationToken)
    {
        var result = new CreateSubjectResult(
            ExternalSubjectId: $"subject-{Guid.NewGuid()}",
            VectorId: $"vector-{Guid.NewGuid()}",
            QualityScore: 95,
            ModelVersion: "fake-engine-v1");

        return Task.FromResult(result);
    }

    public async Task<SearchResult> SearchAsync(
        string imagePath,
        Guid galleryId,
        CancellationToken cancellationToken)
    {
        var subjects = await subjectCatalog.GetSubjectsByGalleryIdAsync(
            galleryId,
            cancellationToken);

        var candidates = subjects
            .Take(3)
            .Select((subject, index) => new SearchCandidate(
                subject.ExternalSubjectId,
                Score: index switch
                {
                    0 => 98.75m,
                    1 => 93.40m,
                    _ => 87.20m
                }))
            .ToList();

        return new SearchResult(candidates);
    }

    public Task DeleteSubjectAsync(
        string externalSubjectId,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}