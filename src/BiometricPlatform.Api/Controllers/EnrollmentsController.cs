using BiometricPlatform.Api.Models;
using BiometricPlatform.Application.Enrollments.CreateEnrollment;
using Microsoft.AspNetCore.Mvc;

namespace BiometricPlatform.Api.Controllers;

[ApiController]
[Route("api/enrollments")]
public sealed class EnrollmentsController : ControllerBase
{
    private readonly CreateEnrollmentHandler _handler;

    public EnrollmentsController(CreateEnrollmentHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<ActionResult<CreateEnrollmentResponse>> Create(
        [FromForm] CreateEnrollmentRequest request,
        CancellationToken cancellationToken)
    {
        await using var imageStream = request.Image.OpenReadStream();

        var command = new CreateEnrollmentCommand(
            request.ClientId,
            request.GalleryId,
            request.FullName,
            request.Document,
            imageStream);

        var response = await _handler.Handle(
            command,
            cancellationToken);

        return Accepted(response);
    }
}