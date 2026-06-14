using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Biometrics;

public sealed class BiometricSample : Entity
{
    private BiometricSample()
    {
    }

    public BiometricSample(
        Guid personId,
        Guid enrollmentId,
        BiometricSampleType type,
        string storagePath)
    {
        PersonId = personId;
        EnrollmentId = enrollmentId;
        Type = type;
        StoragePath = storagePath;
    }

    public Guid PersonId { get; private set; }

    public Guid EnrollmentId { get; private set; }

    public BiometricSampleType Type { get; private set; }

    public string StoragePath { get; private set; } = string.Empty;

    public decimal? QualityScore { get; private set; }

    public void SetQualityScore(decimal qualityScore)
    {
        QualityScore = qualityScore;
    }
}