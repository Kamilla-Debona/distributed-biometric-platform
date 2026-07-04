using BiometricPlatform.Application.Abstractions.Biometrics;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Application.Enrollments.ProcessEnrollment;

public sealed class ProcessEnrollmentHandler
{
    public async Task<bool> Handle(
        ProcessEnrollmentCommand command,
        IEnrollmentRepository enrollmentRepository,
        IPersonRepository personRepository,
        IBiometricSampleRepository biometricSampleRepository,
        ISubjectRepository subjectRepository,
        IBiometricTemplateRepository biometricTemplateRepository,
        IBiometricEngine biometricEngine,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(
            command.EnrollmentId,
            cancellationToken);

        if (enrollment is null)
            throw new InvalidOperationException(
                $"Enrollment {command.EnrollmentId} not found.");

        var person = await personRepository.GetByIdAsync(
            enrollment.PersonId,
            cancellationToken);

        if (person is null)
            throw new InvalidOperationException(
                $"Person {enrollment.PersonId} not found.");

        var biometricSample = await biometricSampleRepository.GetByEnrollmentIdAsync(
            enrollment.Id,
            cancellationToken);

        if (biometricSample is null)
            throw new InvalidOperationException(
                $"BiometricSample for Enrollment {enrollment.Id} not found.");

        enrollment.MarkAsProcessing();

        var result = await biometricEngine.CreateSubjectAsync(
            biometricSample.StoragePath,
            cancellationToken);

        var subject = new Subject(
            person.Id,
            enrollment.GalleryId,
            result.ExternalSubjectId);

        await subjectRepository.AddAsync(
            subject,
            cancellationToken);

        var biometricTemplate = new BiometricTemplate(
            subject.Id,
            biometricSample.Id,
            result.VectorId,
            result.ModelVersion);

        await biometricTemplateRepository.AddAsync(
            biometricTemplate,
            cancellationToken);

        biometricSample.SetQualityScore(result.QualityScore);

        person.MarkAsEnrolled();

        enrollment.Complete();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}