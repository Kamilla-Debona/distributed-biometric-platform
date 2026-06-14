using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Enrollments;

public sealed class Enrollment : AggregateRoot
{
    private Enrollment()
    {
    }

    public Enrollment(Guid personId, Guid galleryId)
    {
        PersonId = personId;
        GalleryId = galleryId;
        Status = EnrollmentStatus.Requested;
    }

    public Guid PersonId { get; private set; }

    public Guid GalleryId { get; private set; }

    public EnrollmentStatus Status { get; private set; }

    public string? FailureReason { get; private set; }

    public DateTime? CompletedAtUtc { get; private set; }

    public void MarkAsProcessing()
    {
        Status = EnrollmentStatus.Processing;
    }

    public void Complete()
    {
        Status = EnrollmentStatus.Completed;
        CompletedAtUtc = DateTime.UtcNow;
    }

    public void Fail(string reason)
    {
        Status = EnrollmentStatus.Failed;
        FailureReason = reason;
        CompletedAtUtc = DateTime.UtcNow;
    }
}