using BiometricPlatform.Api.Models;
using BiometricPlatform.Application.Identifications.CreateIdentification;
using BiometricPlatform.Application.Identifications.GetIdentification;
using Microsoft.AspNetCore.Mvc;

namespace BiometricPlatform.Api.Controllers;

[ApiController]
[Route("api/identifications")]
public sealed class IdentificationsController : ControllerBase
{
    private readonly CreateIdentificationHandler _createHandler;
    private readonly GetIdentificationHandler _getHandler;

    public IdentificationsController(
        CreateIdentificationHandler createHandler,
        GetIdentificationHandler getHandler)
    {
        _createHandler = createHandler;
        _getHandler = getHandler;
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

        var response = await _createHandler.Handle(
            command,
            cancellationToken);

        return Accepted(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetIdentificationResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _getHandler.Handle(
            new GetIdentificationQuery(id),
            cancellationToken);

        if (response is null)
            return NotFound();

        return Ok(response);
    }
}