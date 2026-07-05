namespace BiometricPlatform.Application.Abstractions.Biometrics;

public sealed record SearchCandidate(
    string ExternalSubjectId,
    decimal Score);