namespace BiometricPlatform.Application.Identifications.CreateIdentification;

public sealed record CreateIdentificationCommand(
    Guid GalleryId,
    Stream Image);