using BiometricPlatform.Domain.Enrollments;

namespace BiometricPlatform.Domain.Tests.Enrollments;

public class EnrollmentTests
{
    [Fact]
    public void Ctor_ShouldInitializeAsRequested()
    {
        var personId = Guid.NewGuid();
        var galleryId = Guid.NewGuid();

        var enrollment = new Enrollment(personId, galleryId);

        Assert.Equal(personId, enrollment.PersonId);
        Assert.Equal(galleryId, enrollment.GalleryId);
        Assert.Equal(EnrollmentStatus.Requested, enrollment.Status);
        Assert.Null(enrollment.FailureReason);
        Assert.Null(enrollment.CompletedAtUtc);
        Assert.NotEqual(Guid.Empty, enrollment.Id);
    }

    [Fact]
    public void MarkAsProcessing_ShouldMoveStatusToProcessing()
    {
        var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());

        enrollment.MarkAsProcessing();

        Assert.Equal(EnrollmentStatus.Processing, enrollment.Status);
        Assert.Null(enrollment.CompletedAtUtc);
    }

    [Fact]
    public void Complete_ShouldMoveToCompletedAndStampCompletionTime()
    {
        var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());
        enrollment.MarkAsProcessing();

        var before = DateTime.UtcNow;
        enrollment.Complete();
        var after = DateTime.UtcNow;

        Assert.Equal(EnrollmentStatus.Completed, enrollment.Status);
        Assert.NotNull(enrollment.CompletedAtUtc);
        Assert.InRange(enrollment.CompletedAtUtc!.Value, before, after);
        Assert.Null(enrollment.FailureReason);
    }

    [Fact]
    public void Fail_ShouldMoveToFailedAndRecordReason()
    {
        var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());

        enrollment.Fail("biometric engine unavailable");

        Assert.Equal(EnrollmentStatus.Failed, enrollment.Status);
        Assert.Equal("biometric engine unavailable", enrollment.FailureReason);
        Assert.NotNull(enrollment.CompletedAtUtc);
    }
}
