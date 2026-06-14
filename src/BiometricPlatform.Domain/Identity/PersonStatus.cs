namespace BiometricPlatform.Domain.Identity;

public enum PersonStatus
{
    PendingEnrollment = 1,
    Enrolled = 2,
    Failed = 3,
    Disabled = 4
}