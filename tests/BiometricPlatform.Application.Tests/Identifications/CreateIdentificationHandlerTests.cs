using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Abstractions.Storage;
using BiometricPlatform.Application.Identifications.CreateIdentification;
using BiometricPlatform.Application.Identifications.ProcessIdentification;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Identifications;
using NSubstitute;
using Wolverine;

namespace BiometricPlatform.Application.Tests.Identifications;

public class CreateIdentificationHandlerTests
{
    private readonly IIdentificationRepository _identificationRepo = Substitute.For<IIdentificationRepository>();
    private readonly IBiometricSampleRepository _sampleRepo = Substitute.For<IBiometricSampleRepository>();
    private readonly IObjectStorage _storage = Substitute.For<IObjectStorage>();
    private readonly IMessageBus _bus = Substitute.For<IMessageBus>();
    private readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();

    private CreateIdentificationHandler CreateHandler() =>
        new(_identificationRepo, _sampleRepo, _storage, _bus, _uow);

    [Fact]
    public async Task Handle_ShouldPersistProbeAndIdentificationAndDispatchCommand()
    {
        _storage
            .UploadAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns("/tmp/uploads/probe.jpg");

        var galleryId = Guid.NewGuid();
        var command = new CreateIdentificationCommand(galleryId, new MemoryStream());

        var response = await CreateHandler().Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, response.IdentificationId);

        await _sampleRepo.Received(1).AddAsync(
            Arg.Is<BiometricSample>(s =>
                s.PersonId == null &&
                s.EnrollmentId == null &&
                s.StoragePath == "/tmp/uploads/probe.jpg"),
            Arg.Any<CancellationToken>());

        await _identificationRepo.Received(1).AddAsync(
            Arg.Is<Identification>(i =>
                i.GalleryId == galleryId &&
                i.Status == IdentificationStatus.Requested),
            Arg.Any<CancellationToken>());

        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _bus.Received(1).InvokeAsync(
            Arg.Is<ProcessIdentificationCommand>(c => c.IdentificationId == response.IdentificationId),
            Arg.Any<CancellationToken>());
    }
}
