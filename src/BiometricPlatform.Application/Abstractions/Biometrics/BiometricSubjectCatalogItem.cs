namespace BiometricPlatform.Application.Abstractions.Biometrics;

public sealed record BiometricSubjectCatalogItem(
    Guid SubjectId,
    string ExternalSubjectId);