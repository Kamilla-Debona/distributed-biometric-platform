using BiometricPlatform.Application.Abstractions.Biometrics;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Enrollments.ProcessEnrollment;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Enrollments;
using BiometricPlatform.Domain.Identity;
using NSubstitute;

namespace BiometricPlatform.Application.Tests.Enrollments;

public class ProcessEnrollmentHandlerTests
{
    private readonly IEnrollmentRepository _enrollmentRepo = Substitute.For<IEnrollmentRepository>();
    private readonly IPersonRepository _personRepo = Substitute.For<IPersonRepository>();
    private readonly IBiometricSampleRepository _sampleRepo = Substitute.For<IBiometricSampleRepository>();
    private readonly ISubjectRepository _subjectRepo = Substitute.For<ISubjectRepository>();
    private readonly IBiometricTemplateRepository _templateRepo = Substitute.For<IBiometricTemplateRepository>();
    private readonly IBiometricEngine _engine = Substitute.For<IBiometricEngine>();
    private readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();

    private readonly ProcessEnrollmentHandler _handler = new();

    private Task<bool> Invoke(ProcessEnrollmentCommand cmd) =>
        _handler.Handle(cmd, _enrollmentRepo, _personRepo, _sampleRepo, _subjectRepo, _templateRepo, _engine, _uow, CancellationToken.None);

    [Fact]
    public async Task Handle_ShouldCompleteEnrollmentAndEnrollPerson()
    {
        var clientId = Guid.NewGuid();
        var galleryId = Guid.NewGuid();
        var person = new Person(clientId, Guid.NewGuid());
        var enrollment = new Enrollment(person.Id, galleryId);
        var sample = BiometricSample.CreateEnrollmentSample(person.Id, enrollment.Id, BiometricSampleType.Face, "/tmp/img.jpg");

        _enrollmentRepo.GetByIdAsync(enrollment.Id, Arg.Any<CancellationToken>()).Returns(enrollment);
        _personRepo.GetByIdAsync(person.Id, Arg.Any<CancellationToken>()).Returns(person);
        _sampleRepo.GetByEnrollmentIdAsync(enrollment.Id, Arg.Any<CancellationToken>()).Returns(sample);
        _engine.CreateSubjectAsync(sample.StoragePath, Arg.Any<CancellationToken>())
            .Returns(new CreateSubjectResult("ext-1", "vec-1", 91.5m, "v1"));

        var result = await Invoke(new ProcessEnrollmentCommand(enrollment.Id));

        Assert.True(result);
        Assert.Equal(EnrollmentStatus.Completed, enrollment.Status);
        Assert.NotNull(enrollment.CompletedAtUtc);
        Assert.Equal(PersonStatus.Enrolled, person.Status);
        Assert.Equal(91.5m, sample.QualityScore);

        await _subjectRepo.Received(1).AddAsync(
            Arg.Is<Subject>(s => s.PersonId == person.Id && s.GalleryId == galleryId && s.ExternalSubjectId == "ext-1"),
            Arg.Any<CancellationToken>());
        await _templateRepo.Received(1).AddAsync(
            Arg.Is<BiometricTemplate>(t => t.VectorId == "vec-1" && t.ModelVersion == "v1"),
            Arg.Any<CancellationToken>());
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenEnrollmentMissing()
    {
        var enrollmentId = Guid.NewGuid();
        _enrollmentRepo.GetByIdAsync(enrollmentId, Arg.Any<CancellationToken>()).Returns((Enrollment?)null);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => Invoke(new ProcessEnrollmentCommand(enrollmentId)));

        Assert.Contains(enrollmentId.ToString(), ex.Message);
        await _uow.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenPersonMissing()
    {
        var enrollment = new Enrollment(Guid.NewGuid(), Guid.NewGuid());
        _enrollmentRepo.GetByIdAsync(enrollment.Id, Arg.Any<CancellationToken>()).Returns(enrollment);
        _personRepo.GetByIdAsync(enrollment.PersonId, Arg.Any<CancellationToken>()).Returns((Person?)null);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => Invoke(new ProcessEnrollmentCommand(enrollment.Id)));

        await _uow.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenSampleMissing()
    {
        var person = new Person(Guid.NewGuid(), Guid.NewGuid());
        var enrollment = new Enrollment(person.Id, Guid.NewGuid());
        _enrollmentRepo.GetByIdAsync(enrollment.Id, Arg.Any<CancellationToken>()).Returns(enrollment);
        _personRepo.GetByIdAsync(person.Id, Arg.Any<CancellationToken>()).Returns(person);
        _sampleRepo.GetByEnrollmentIdAsync(enrollment.Id, Arg.Any<CancellationToken>()).Returns((BiometricSample?)null);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => Invoke(new ProcessEnrollmentCommand(enrollment.Id)));

        await _engine.DidNotReceive().CreateSubjectAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    }
}
