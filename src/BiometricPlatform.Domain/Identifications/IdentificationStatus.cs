namespace BiometricPlatform.Domain.Identifications;

public enum IdentificationStatus
{
    Requested = 1,
    Processing = 2,
    Completed = 3,
    NoMatch = 4,
    Failed = 5
}

