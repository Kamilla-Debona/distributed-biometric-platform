using BiometricPlatform.Domain.Identity;

namespace BiometricPlatform.Domain.Tests.Identity;

public class PersonTests
{
    [Fact]
    public void Ctor_ShouldInitializeAsPendingEnrollment()
    {
        var clientId = Guid.NewGuid();
        var biographicDataId = Guid.NewGuid();

        var person = new Person(clientId, biographicDataId);

        Assert.Equal(clientId, person.ClientId);
        Assert.Equal(biographicDataId, person.BiographicDataId);
        Assert.Equal(PersonStatus.PendingEnrollment, person.Status);
        Assert.Null(person.UpdatedAtUtc);
    }

    [Fact]
    public void MarkAsEnrolled_ShouldTransitionAndStampUpdate()
    {
        var person = new Person(Guid.NewGuid(), Guid.NewGuid());

        person.MarkAsEnrolled();

        Assert.Equal(PersonStatus.Enrolled, person.Status);
        Assert.NotNull(person.UpdatedAtUtc);
    }

    [Fact]
    public void MarkAsFailed_ShouldTransitionAndStampUpdate()
    {
        var person = new Person(Guid.NewGuid(), Guid.NewGuid());

        person.MarkAsFailed();

        Assert.Equal(PersonStatus.Failed, person.Status);
        Assert.NotNull(person.UpdatedAtUtc);
    }

    [Fact]
    public void Disable_ShouldTransitionAndStampUpdate()
    {
        var person = new Person(Guid.NewGuid(), Guid.NewGuid());
        person.MarkAsEnrolled();

        person.Disable();

        Assert.Equal(PersonStatus.Disabled, person.Status);
    }
}
