using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Abstractions.Storage;
using BiometricPlatform.Application.Enrollments.CreateEnrollment;
using BiometricPlatform.Application.Enrollments.ProcessEnrollment;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Enrollments;
using BiometricPlatform.Domain.Identity;
using NSubstitute;
using Wolverine;

namespace BiometricPlatform.Application.Tests.Enrollments;

public class CreateEnrollmentHandlerTests
{
    private readonly IBiographicDataRepository _biographicRepo = Substitute.For<IBiographicDataRepository>();
    private readonly IPersonRepository _personRepo = Substitute.For<IPersonRepository>();
    private readonly IEnrollmentRepository _enrollmentRepo = Substitute.For<IEnrollmentRepository>();
    private readonly IBiometricSampleRepository _sampleRepo = Substitute.For<IBiometricSampleRepository>();
    private readonly IObjectStorage _storage = Substitute.For<IObjectStorage>();
    private readonly IMessageBus _bus = Substitute.For<IMessageBus>();
    private readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();

    private CreateEnrollmentHandler CreateHandler() =>
        new(_biographicRepo, _personRepo, _enrollmentRepo, _sampleRepo, _storage, _bus, _uow);

    [Fact]
    public async Task Handle_ShouldPersistAggregatesAndDispatchProcessCommand()
    {
        _storage
            .UploadAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns("/tmp/uploads/img.jpg");

        var command = new CreateEnrollmentCommand(
            ClientId: Guid.NewGuid(),
            GalleryId: Guid.NewGuid(),
            FullName: "Ada Lovelace",
            Document: "12345678901",
            Image: new MemoryStream(new byte[] { 1, 2, 3 }));

        var response = await CreateHandler().Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, response.EnrollmentId);
        Assert.NotEqual(Guid.Empty, response.PersonId);

        await _storage.Received(1).UploadAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
        await _biographicRepo.Received(1).AddAsync(Arg.Any<BiographicData>(), Arg.Any<CancellationToken>());
        await _personRepo.Received(1).AddAsync(
            Arg.Is<Person>(p => p.ClientId == command.ClientId && p.Status == PersonStatus.PendingEnrollment),
            Arg.Any<CancellationToken>());
        await _enrollmentRepo.Received(1).AddAsync(
            Arg.Is<Enrollment>(e => e.GalleryId == command.GalleryId && e.Status == EnrollmentStatus.Requested),
            Arg.Any<CancellationToken>());
        await _sampleRepo.Received(1).AddAsync(
            Arg.Is<BiometricSample>(s => s.StoragePath == "/tmp/uploads/img.jpg" && s.Type == BiometricSampleType.Face),
            Arg.Any<CancellationToken>());
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _bus.Received(1).InvokeAsync(
            Arg.Is<ProcessEnrollmentCommand>(c => c.EnrollmentId == response.EnrollmentId),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldPersistBeforeDispatchingProcessCommand()
    {
        _storage
            .UploadAsync(Arg.Any<Stream>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns("/tmp/uploads/img.jpg");

        var command = new CreateEnrollmentCommand(
            Guid.NewGuid(), Guid.NewGuid(), "Grace Hopper", "999", new MemoryStream());

        await CreateHandler().Handle(command, CancellationToken.None);

        Received.InOrder(async () =>
        {
            await _uow.SaveChangesAsync(Arg.Any<CancellationToken>());
            await _bus.InvokeAsync(Arg.Any<ProcessEnrollmentCommand>(), Arg.Any<CancellationToken>());
        });
    }
}
