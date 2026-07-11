using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Identifications.GetIdentification;
using BiometricPlatform.Domain.Identifications;
using NSubstitute;

namespace BiometricPlatform.Application.Tests.Identifications;

public class GetIdentificationHandlerTests
{
    private readonly IIdentificationRepository _identificationRepo = Substitute.For<IIdentificationRepository>();
    private readonly IIdentificationCandidateRepository _candidateRepo = Substitute.For<IIdentificationCandidateRepository>();

    private GetIdentificationHandler CreateHandler() => new(_identificationRepo, _candidateRepo);

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenIdentificationDoesNotExist()
    {
        var id = Guid.NewGuid();
        _identificationRepo.GetByIdAsync(id, Arg.Any<CancellationToken>()).Returns((Identification?)null);

        var response = await CreateHandler().Handle(new GetIdentificationQuery(id), CancellationToken.None);

        Assert.Null(response);
        await _candidateRepo.DidNotReceive().GetByIdentificationIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnResponseWithCandidates()
    {
        var identification = new Identification(Guid.NewGuid(), Guid.NewGuid());
        identification.Complete();

        var candidateA = new IdentificationCandidate(identification.Id, Guid.NewGuid(), Guid.NewGuid(), 98.5m, 1);
        var candidateB = new IdentificationCandidate(identification.Id, Guid.NewGuid(), Guid.NewGuid(), 90.0m, 2);

        _identificationRepo.GetByIdAsync(identification.Id, Arg.Any<CancellationToken>()).Returns(identification);
        _candidateRepo.GetByIdentificationIdAsync(identification.Id, Arg.Any<CancellationToken>())
            .Returns(new[] { candidateA, candidateB });

        var response = await CreateHandler().Handle(new GetIdentificationQuery(identification.Id), CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(identification.Id, response!.Id);
        Assert.Equal(nameof(IdentificationStatus.Completed), response.Status);
        Assert.Equal(2, response.Candidates.Count);
        Assert.Contains(response.Candidates, c => c.Score == 98.5m && c.Rank == 1);
        Assert.Contains(response.Candidates, c => c.Score == 90.0m && c.Rank == 2);
    }
}
