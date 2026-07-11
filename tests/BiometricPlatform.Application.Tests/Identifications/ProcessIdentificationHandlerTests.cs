using BiometricPlatform.Application.Abstractions.Biometrics;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Identifications.ProcessIdentification;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Identifications;
using NSubstitute;

namespace BiometricPlatform.Application.Tests.Identifications;

public class ProcessIdentificationHandlerTests
{
    private readonly IIdentificationRepository _identificationRepo = Substitute.For<IIdentificationRepository>();
    private readonly IBiometricSampleRepository _sampleRepo = Substitute.For<IBiometricSampleRepository>();
    private readonly ISubjectRepository _subjectRepo = Substitute.For<ISubjectRepository>();
    private readonly IIdentificationCandidateRepository _candidateRepo = Substitute.For<IIdentificationCandidateRepository>();
    private readonly IBiometricEngine _engine = Substitute.For<IBiometricEngine>();
    private readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();

    private readonly ProcessIdentificationHandler _handler = new();

    private Task<bool> Invoke(Guid identificationId) =>
        _handler.Handle(
            new ProcessIdentificationCommand(identificationId),
            _identificationRepo, _sampleRepo, _subjectRepo, _candidateRepo, _engine, _uow,
            CancellationToken.None);

    private static BiometricSample Probe(string path) =>
        BiometricSample.CreateProbeSample(BiometricSampleType.Face, path);

    [Fact]
    public async Task Handle_ShouldPersistRankedCandidates_WhenMatchesFound()
    {
        var galleryId = Guid.NewGuid();
        var probe = Probe("/tmp/probe.jpg");
        var identification = new Identification(galleryId, probe.Id);

        var subjectA = new Subject(Guid.NewGuid(), galleryId, "ext-A");
        var subjectB = new Subject(Guid.NewGuid(), galleryId, "ext-B");

        _identificationRepo.GetByIdAsync(identification.Id, Arg.Any<CancellationToken>()).Returns(identification);
        _sampleRepo.GetByIdAsync(probe.Id, Arg.Any<CancellationToken>()).Returns(probe);
        _engine.SearchAsync(probe.StoragePath, galleryId, Arg.Any<CancellationToken>())
            .Returns(new SearchResult(new[]
            {
                new SearchCandidate("ext-A", 98.5m),
                new SearchCandidate("ext-B", 90.0m),
                new SearchCandidate("ext-UNKNOWN", 80.0m)
            }));
        _subjectRepo.GetByExternalSubjectIdAsync("ext-A", Arg.Any<CancellationToken>()).Returns(subjectA);
        _subjectRepo.GetByExternalSubjectIdAsync("ext-B", Arg.Any<CancellationToken>()).Returns(subjectB);
        _subjectRepo.GetByExternalSubjectIdAsync("ext-UNKNOWN", Arg.Any<CancellationToken>()).Returns((Subject?)null);

        var result = await Invoke(identification.Id);

        Assert.True(result);
        Assert.Equal(IdentificationStatus.Completed, identification.Status);
        Assert.NotNull(identification.CompletedAtUtc);

        await _candidateRepo.Received(1).AddAsync(
            Arg.Is<IdentificationCandidate>(c => c.SubjectId == subjectA.Id && c.Score == 98.5m && c.Rank == 1),
            Arg.Any<CancellationToken>());
        await _candidateRepo.Received(1).AddAsync(
            Arg.Is<IdentificationCandidate>(c => c.SubjectId == subjectB.Id && c.Score == 90.0m && c.Rank == 2),
            Arg.Any<CancellationToken>());
        await _candidateRepo.Received(2).AddAsync(Arg.Any<IdentificationCandidate>(), Arg.Any<CancellationToken>());
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCompleteWithNoMatch_WhenNoSubjectResolved()
    {
        var galleryId = Guid.NewGuid();
        var probe = Probe("/tmp/probe.jpg");
        var identification = new Identification(galleryId, probe.Id);

        _identificationRepo.GetByIdAsync(identification.Id, Arg.Any<CancellationToken>()).Returns(identification);
        _sampleRepo.GetByIdAsync(probe.Id, Arg.Any<CancellationToken>()).Returns(probe);
        _engine.SearchAsync(probe.StoragePath, galleryId, Arg.Any<CancellationToken>())
            .Returns(new SearchResult(Array.Empty<SearchCandidate>()));

        await Invoke(identification.Id);

        Assert.Equal(IdentificationStatus.NoMatch, identification.Status);
        await _candidateRepo.DidNotReceive().AddAsync(Arg.Any<IdentificationCandidate>(), Arg.Any<CancellationToken>());
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCompleteWithNoMatch_WhenAllSubjectsUnresolved()
    {
        var galleryId = Guid.NewGuid();
        var probe = Probe("/tmp/probe.jpg");
        var identification = new Identification(galleryId, probe.Id);

        _identificationRepo.GetByIdAsync(identification.Id, Arg.Any<CancellationToken>()).Returns(identification);
        _sampleRepo.GetByIdAsync(probe.Id, Arg.Any<CancellationToken>()).Returns(probe);
        _engine.SearchAsync(probe.StoragePath, galleryId, Arg.Any<CancellationToken>())
            .Returns(new SearchResult(new[] { new SearchCandidate("ext-ghost", 70m) }));
        _subjectRepo.GetByExternalSubjectIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns((Subject?)null);

        await Invoke(identification.Id);

        Assert.Equal(IdentificationStatus.NoMatch, identification.Status);
        await _candidateRepo.DidNotReceive().AddAsync(Arg.Any<IdentificationCandidate>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenIdentificationMissing()
    {
        var id = Guid.NewGuid();
        _identificationRepo.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns((Identification?)null);

        await Assert.ThrowsAsync<InvalidOperationException>(() => Invoke(id));
        await _engine.DidNotReceive().SearchAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenProbeSampleMissing()
    {
        var identification = new Identification(Guid.NewGuid(), Guid.NewGuid());
        _identificationRepo.GetByIdAsync(identification.Id, Arg.Any<CancellationToken>()).Returns(identification);
        _sampleRepo.GetByIdAsync(identification.ProbeSampleId, Arg.Any<CancellationToken>()).Returns((BiometricSample?)null);

        await Assert.ThrowsAsync<InvalidOperationException>(() => Invoke(identification.Id));
        await _engine.DidNotReceive().SearchAsync(Arg.Any<string>(), Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }
}
