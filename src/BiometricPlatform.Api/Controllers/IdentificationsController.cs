using BiometricPlatform.Api.Models;
using BiometricPlatform.Application.Identifications.CreateIdentification;
using Microsoft.AspNetCore.Mvc;

namespace BiometricPlatform.Api.Controllers;

[ApiController]
[Route("api/identifications")]
public sealed class IdentificationsController : ControllerBase
{
    private readonly CreateIdentificationHandler _handler;

    public IdentificationsController(CreateIdentificationHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<ActionResult<CreateIdentificationResponse>> Create(
        [FromForm] CreateIdentificationRequest request,
        CancellationToken cancellationToken)
    {
        await using var imageStream = request.Image.OpenReadStream();

        var command = new CreateIdentificationCommand(
            request.GalleryId,
            imageStream);

        var response = await _handler.Handle(
            command,
            cancellationToken);

        return Accepted(response);
    }
}