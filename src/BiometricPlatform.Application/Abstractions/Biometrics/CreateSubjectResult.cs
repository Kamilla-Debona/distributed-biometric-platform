namespace BiometricPlatform.Application.Abstractions.Biometrics;

public sealed record CreateSubjectResult(
    string ExternalSubjectId,
    string VectorId,
    decimal QualityScore,
    string ModelVersion);