using BiometricPlatform.Application.Abstractions.Biometrics;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Identifications;

namespace BiometricPlatform.Application.Identifications.ProcessIdentification;

public sealed class ProcessIdentificationHandler
{
    public async Task<bool> Handle(
        ProcessIdentificationCommand command,
        IIdentificationRepository identificationRepository,
        IBiometricSampleRepository biometricSampleRepository,
        ISubjectRepository subjectRepository,
        IIdentificationCandidateRepository identificationCandidateRepository,
        IBiometricEngine biometricEngine,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var identification = await identificationRepository.GetByIdAsync(
            command.IdentificationId,
            cancellationToken);

        if (identification is null)
            throw new InvalidOperationException(
                $"Identification {command.IdentificationId} not found.");

        var probeSample = await biometricSampleRepository.GetByIdAsync(
            identification.ProbeSampleId,
            cancellationToken);

        if (probeSample is null)
            throw new InvalidOperationException(
                $"Probe sample {identification.ProbeSampleId} not found.");

        identification.MarkAsProcessing();

        var searchResult = await biometricEngine.SearchAsync(
            probeSample.StoragePath,
            identification.GalleryId,
            cancellationToken);

        var rank = 1;
        var matchedCandidates = 0;

        foreach (var candidate in searchResult.Candidates)
        {
            var subject = await subjectRepository.GetByExternalSubjectIdAsync(
                candidate.ExternalSubjectId,
                cancellationToken);

            if (subject is null)
                continue;

            var identificationCandidate = new IdentificationCandidate(
                identification.Id,
                subject.PersonId,
                subject.Id,
                candidate.Score,
                rank);

            await identificationCandidateRepository.AddAsync(
                identificationCandidate,
                cancellationToken);

            rank++;
            matchedCandidates++;
        }

        if (matchedCandidates == 0)
        {
            identification.CompleteWithNoMatch();
        }
        else
        {
            identification.Complete();
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}