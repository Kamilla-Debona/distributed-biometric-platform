using BiometricPlatform.Application.Abstractions.Persistence;

namespace BiometricPlatform.Application.Identifications.GetIdentification;

public sealed class GetIdentificationHandler(
    IIdentificationRepository identificationRepository,
    IIdentificationCandidateRepository candidateRepository)
{
    public async Task<GetIdentificationResponse?> Handle(
        GetIdentificationQuery query,
        CancellationToken cancellationToken)
    {
        var identification = await identificationRepository.GetByIdAsync(
            query.IdentificationId,
            cancellationToken);

        if (identification is null)
            return null;

        var candidates = await candidateRepository.GetByIdentificationIdAsync(
            identification.Id,
            cancellationToken);

        var candidateResponses = candidates
            .Select(candidate => new GetIdentificationCandidateResponse(
                candidate.PersonId,
                candidate.SubjectId,
                candidate.Score,
                candidate.Rank))
            .ToList();

        return new GetIdentificationResponse(
            identification.Id,
            identification.GalleryId,
            identification.ProbeSampleId,
            identification.Status.ToString(),
            identification.FailureReason,
            identification.CreatedAtUtc,
            identification.CompletedAtUtc,
            candidateResponses);
    }
}