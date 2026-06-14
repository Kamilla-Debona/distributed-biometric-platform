using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Biometrics;

public sealed class Gallery : AggregateRoot
{
    private Gallery()
    {
        Id = Guid.NewGuid();
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Gallery(Guid clientId, string name, string? description = null)
    {
        ClientId = clientId;
        Name = name;
        Description = description;
    }

    public Guid ClientId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }
}