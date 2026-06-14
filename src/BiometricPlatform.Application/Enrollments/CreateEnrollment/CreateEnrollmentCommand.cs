namespace BiometricPlatform.Application.Enrollments.CreateEnrollment;

public sealed record CreateEnrollmentCommand(
    Guid ClientId,
    Guid GalleryId,
    string FullName,
    string Document,
    Stream Image);