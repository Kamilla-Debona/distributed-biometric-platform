using BiometricPlatform.Domain.Common;

namespace BiometricPlatform.Domain.Biometrics;

public sealed class Subject : Entity
{
    private Subject()
    {
    }

    public Subject(
        Guid personId,
        Guid galleryId,
        string externalSubjectId)
    {
        PersonId = personId;
        GalleryId = galleryId;
        ExternalSubjectId = externalSubjectId;
        Status = SubjectStatus.Active;
    }

    public Guid PersonId { get; private set; }

    public Guid GalleryId { get; private set; }

    public string ExternalSubjectId { get; private set; } = string.Empty;

    public SubjectStatus Status { get; private set; }

    public void MarkAsFailed()
    {
        Status = SubjectStatus.Failed;
    }

    public void Disable()
    {
        Status = SubjectStatus.Disabled;
    }
}