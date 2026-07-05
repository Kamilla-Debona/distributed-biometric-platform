namespace BiometricPlatform.Application.Identifications.CreateIdentification;

public sealed record CreateIdentificationCommand(
    Guid ClientId,
    Guid GalleryId,
    Stream Image);