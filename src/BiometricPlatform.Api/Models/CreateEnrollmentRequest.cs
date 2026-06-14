namespace BiometricPlatform.Api.Models;

public sealed class CreateEnrollmentRequest
{
    public Guid ClientId { get; set; }

    public Guid GalleryId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Document { get; set; } = string.Empty;

    public IFormFile Image { get; set; } = default!;
}