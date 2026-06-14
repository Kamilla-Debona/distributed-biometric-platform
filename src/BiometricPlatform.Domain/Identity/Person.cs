using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Identity;

public sealed class Person : AggregateRoot
{
    private Person()
    {
    }

    public Person(Guid clientId, Guid biographicDataId)
    {
        ClientId = clientId;
        BiographicDataId = biographicDataId;
        Status = PersonStatus.PendingEnrollment;
    }

    public Guid ClientId { get; private set; }

    public Guid BiographicDataId { get; private set; }

    public PersonStatus Status { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    public void MarkAsEnrolled()
    {
        Status = PersonStatus.Enrolled;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = PersonStatus.Failed;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Disable()
    {
        Status = PersonStatus.Disabled;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}