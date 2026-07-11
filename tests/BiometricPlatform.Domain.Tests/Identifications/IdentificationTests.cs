using BiometricPlatform.Domain.Identifications;

namespace BiometricPlatform.Domain.Tests.Identifications;

public class IdentificationTests
{
    [Fact]
    public void Ctor_ShouldInitializeAsRequested()
    {
        var galleryId = Guid.NewGuid();
        var probeSampleId = Guid.NewGuid();

        var identification = new Identification(galleryId, probeSampleId);

        Assert.Equal(galleryId, identification.GalleryId);
        Assert.Equal(probeSampleId, identification.ProbeSampleId);
        Assert.Equal(IdentificationStatus.Requested, identification.Status);
        Assert.Null(identification.FailureReason);
        Assert.Null(identification.CompletedAtUtc);
    }

    [Fact]
    public void MarkAsProcessing_ShouldMoveStatusToProcessing()
    {
        var identification = new Identification(Guid.NewGuid(), Guid.NewGuid());

        identification.MarkAsProcessing();

        Assert.Equal(IdentificationStatus.Processing, identification.Status);
    }

    [Fact]
    public void Complete_ShouldMoveToCompletedAndStampCompletion()
    {
        var identification = new Identification(Guid.NewGuid(), Guid.NewGuid());

        identification.Complete();

        Assert.Equal(IdentificationStatus.Completed, identification.Status);
        Assert.NotNull(identification.CompletedAtUtc);
    }

    [Fact]
    public void CompleteWithNoMatch_ShouldMoveToNoMatchAndStampCompletion()
    {
        var identification = new Identification(Guid.NewGuid(), Guid.NewGuid());

        identification.CompleteWithNoMatch();

        Assert.Equal(IdentificationStatus.NoMatch, identification.Status);
        Assert.NotNull(identification.CompletedAtUtc);
        Assert.Null(identification.FailureReason);
    }

    [Fact]
    public void Fail_ShouldMoveToFailedAndRecordReason()
    {
        var identification = new Identification(Guid.NewGuid(), Guid.NewGuid());

        identification.Fail("search engine timeout");

        Assert.Equal(IdentificationStatus.Failed, identification.Status);
        Assert.Equal("search engine timeout", identification.FailureReason);
        Assert.NotNull(identification.CompletedAtUtc);
    }
}
