namespace BiometricPlatform.Application.Abstractions.Biometrics;

public sealed record CreateSubjectResult(
    Guid SubjectId,
    decimal QualityScore,
    byte[] Template);