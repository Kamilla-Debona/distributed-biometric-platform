namespace BiometricPlatform.Application.Enrollments.Messages;

public sealed record EnrollmentRequestedMessage(
    Guid EnrollmentId,
    Guid PersonId,
    Guid GalleryId,
    Guid BiometricSampleId,
    string StoragePath);