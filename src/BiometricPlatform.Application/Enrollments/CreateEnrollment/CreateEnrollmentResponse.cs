namespace BiometricPlatform.Application.Enrollments.CreateEnrollment;

public sealed record CreateEnrollmentResponse(
    Guid EnrollmentId,
    Guid PersonId);