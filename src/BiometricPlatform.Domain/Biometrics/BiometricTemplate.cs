using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Biometrics;

public sealed class BiometricTemplate : Entity
{
    private BiometricTemplate()
    {
    }

    public BiometricTemplate(
        Guid subjectId,
        Guid biometricSampleId,
        string vectorId,
        string modelVersion)
    {
        SubjectId = subjectId;
        BiometricSampleId = biometricSampleId;
        VectorId = vectorId;
        ModelVersion = modelVersion;
    }

    public Guid SubjectId { get; private set; }

    public Guid BiometricSampleId { get; private set; }

    public string VectorId { get; private set; } = string.Empty;

    public string ModelVersion { get; private set; } = string.Empty;
}