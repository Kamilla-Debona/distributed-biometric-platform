using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Identity;

public sealed class BiographicData : Entity
{
    private BiographicData()
    {
    }

    public BiographicData(
        string fullName,
        string document,
        DateOnly? dateOfBirth = null)
    {
        FullName = fullName;
        Document = document;
        DateOfBirth = dateOfBirth;
    }

    public string FullName { get; private set; } = string.Empty;

    public string Document { get; private set; } = string.Empty;

    public DateOnly? DateOfBirth { get; private set; }
}