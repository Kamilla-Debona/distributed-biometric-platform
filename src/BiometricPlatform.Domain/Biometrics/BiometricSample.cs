using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Biometrics;

public sealed class BiometricSample : Entity
{
    private BiometricSample()
    {
    }

    private BiometricSample(
        Guid? personId,
        Guid? enrollmentId,
        BiometricSampleType type,
        string storagePath)
    {
        PersonId = personId;
        EnrollmentId = enrollmentId;
        Type = type;
        StoragePath = storagePath;
    }

    public Guid? PersonId { get; private set; }

    public Guid? EnrollmentId { get; private set; }

    public BiometricSampleType Type { get; private set; }

    public string StoragePath { get; private set; } = string.Empty;

    public decimal? QualityScore { get; private set; }

    public static BiometricSample CreateEnrollmentSample(
        Guid personId,
        Guid enrollmentId,
        BiometricSampleType type,
        string storagePath)
    {
        return new BiometricSample(
            personId,
            enrollmentId,
            type,
            storagePath);
    }

    public static BiometricSample CreateProbeSample(
        BiometricSampleType type,
        string storagePath)
    {
        return new BiometricSample(
            personId: null,
            enrollmentId: null,
            type,
            storagePath);
    }

    public void SetQualityScore(decimal qualityScore)
    {
        QualityScore = qualityScore;
    }
}