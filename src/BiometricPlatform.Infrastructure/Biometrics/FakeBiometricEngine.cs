using BiometricPlatform.Application.Abstractions.Biometrics;

namespace BiometricPlatform.Infrastructure.Biometrics;

public sealed class FakeBiometricEngine : IBiometricEngine
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

    public Task<SearchResult> SearchAsync(
        string imagePath,
        Guid galleryId,
        CancellationToken cancellationToken)
    {
        var candidates = new List<SearchCandidate>
        {
            new(
                ExternalSubjectId: $"subject-{Guid.NewGuid()}",
                Score: 98.75m),
            new(
                ExternalSubjectId: $"subject-{Guid.NewGuid()}",
                Score: 93.40m),
            new(
                ExternalSubjectId: $"subject-{Guid.NewGuid()}",
                Score: 87.20m)
        };

        var result = new SearchResult(candidates);

        return Task.FromResult(result);
    }

    public Task DeleteSubjectAsync(
        string externalSubjectId,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}