namespace BiometricPlatform.Application.Abstractions.Biometrics;

public sealed record SearchResult(
    IReadOnlyCollection<SearchCandidate> Candidates);