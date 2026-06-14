using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Identity;

public sealed class Client : Entity
{
    private Client()
    {
    }

    public Client(string name)
    {
        Name = name;
    }

    public string Name { get; private set; } = string.Empty;
}