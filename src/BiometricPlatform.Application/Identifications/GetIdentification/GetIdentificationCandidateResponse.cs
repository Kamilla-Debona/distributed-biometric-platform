namespace BiometricPlatform.Application.Identifications.GetIdentification;

public sealed record GetIdentificationCandidateResponse(
    Guid PersonId,
    Guid SubjectId,
    decimal Score,
    int Rank);