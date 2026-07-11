using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Identifications;

public sealed class Identification : AggregateRoot
{
    private Identification()
    {
    }

    public Identification(
        Guid galleryId,
        Guid probeSampleId)
    {
        GalleryId = galleryId;
        ProbeSampleId = probeSampleId;
        Status = IdentificationStatus.Requested;
    }

    public Guid GalleryId { get; private set; }

    public Guid ProbeSampleId { get; private set; }

    public IdentificationStatus Status { get; private set; }

    public string? FailureReason { get; private set; }

    public DateTime? CompletedAtUtc { get; private set; }

    public void MarkAsProcessing()
    {
        Status = IdentificationStatus.Processing;
    }

    public void Complete()
    {
        Status = IdentificationStatus.Completed;
        CompletedAtUtc = DateTime.UtcNow;
    }

    public void CompleteWithNoMatch()
    {
        Status = IdentificationStatus.NoMatch;
        CompletedAtUtc = DateTime.UtcNow;
    }

    public void Fail(string reason)
    {
        Status = IdentificationStatus.Failed;
        FailureReason = reason;
        CompletedAtUtc = DateTime.UtcNow;
    }
}