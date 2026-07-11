using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Domain.Tests.Biometrics;

public class BiometricSampleTests
{
    [Fact]
    public void CreateEnrollmentSample_ShouldBindOwnershipToPersonAndEnrollment()
    {
        var personId = Guid.NewGuid();
        var enrollmentId = Guid.NewGuid();

        var sample = BiometricSample.CreateEnrollmentSample(
            personId,
            enrollmentId,
            BiometricSampleType.Face,
            "/tmp/img.jpg");

        Assert.Equal(personId, sample.PersonId);
        Assert.Equal(enrollmentId, sample.EnrollmentId);
        Assert.Equal(BiometricSampleType.Face, sample.Type);
        Assert.Equal("/tmp/img.jpg", sample.StoragePath);
        Assert.Null(sample.QualityScore);
    }

    [Fact]
    public void CreateProbeSample_ShouldLeaveOwnershipUnbound()
    {
        var sample = BiometricSample.CreateProbeSample(
            BiometricSampleType.Face,
            "/tmp/probe.jpg");

        Assert.Null(sample.PersonId);
        Assert.Null(sample.EnrollmentId);
        Assert.Equal(BiometricSampleType.Face, sample.Type);
        Assert.Equal("/tmp/probe.jpg", sample.StoragePath);
    }

    [Fact]
    public void SetQualityScore_ShouldPersistScore()
    {
        var sample = BiometricSample.CreateProbeSample(BiometricSampleType.Face, "/tmp/x.jpg");

        sample.SetQualityScore(87.5m);

        Assert.Equal(87.5m, sample.QualityScore);
    }
}
