namespace BiometricPlatform.Api.Models;

public sealed class CreateIdentificationRequest
{
    public Guid GalleryId { get; set; }

    public IFormFile Image { get; set; } = default!;
}