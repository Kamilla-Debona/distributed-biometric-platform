namespace BiometricPlatform.Application.Identifications.GetIdentification;

public sealed record GetIdentificationResponse(
    Guid Id,
    Guid GalleryId,
    Guid ProbeSampleId,
    string Status,
    string? FailureReason,
    DateTime CreatedAtUtc,
    DateTime? CompletedAtUtc,
    IReadOnlyCollection<GetIdentificationCandidateResponse> Candidates);