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
}