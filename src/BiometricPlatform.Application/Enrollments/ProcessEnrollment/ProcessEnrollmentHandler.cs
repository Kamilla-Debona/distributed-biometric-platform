using BiometricPlatform.Application.Abstractions.Biometrics;
using BiometricPlatform.Application.Abstractions.Messaging;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Domain.Biometrics;

namespace BiometricPlatform.Application.Enrollments.ProcessEnrollment;

public sealed class ProcessEnrollmentHandler
    : ICommandHandler<ProcessEnrollmentCommand, bool>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IBiometricSampleRepository _biometricSampleRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IBiometricTemplateRepository _biometricTemplateRepository;
    private readonly IBiometricEngine _biometricEngine;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessEnrollmentHandler(
        IEnrollmentRepository enrollmentRepository,
        IPersonRepository personRepository,
        IBiometricSampleRepository biometricSampleRepository,
        ISubjectRepository subjectRepository,
        IBiometricTemplateRepository biometricTemplateRepository,
        IBiometricEngine biometricEngine,
        IUnitOfWork unitOfWork)
    {
        _enrollmentRepository = enrollmentRepository;
        _personRepository = personRepository;
        _biometricSampleRepository = biometricSampleRepository;
        _subjectRepository = subjectRepository;
        _biometricTemplateRepository = biometricTemplateRepository;
        _biometricEngine = biometricEngine;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        ProcessEnrollmentCommand command,
        CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(
            command.EnrollmentId,
            cancellationToken);

        if (enrollment is null)
            throw new InvalidOperationException(
                $"Enrollment {command.EnrollmentId} not found.");

        var person = await _personRepository.GetByIdAsync(
            enrollment.PersonId,
            cancellationToken);

        if (person is null)
            throw new InvalidOperationException(
                $"Person {enrollment.PersonId} not found.");

        var biometricSample =
            await _biometricSampleRepository.GetByEnrollmentIdAsync(
                enrollment.Id,
                cancellationToken);

        if (biometricSample is null)
            throw new InvalidOperationException(
                $"BiometricSample for Enrollment {enrollment.Id} not found.");

        enrollment.MarkAsProcessing();

        var result = await _biometricEngine.CreateSubjectAsync(
            biometricSample.StoragePath,
            cancellationToken);

        var subject = new Subject(
            person.Id,
            enrollment.GalleryId,
            result.ExternalSubjectId);

        await _subjectRepository.AddAsync(
            subject,
            cancellationToken);

        var biometricTemplate = new BiometricTemplate(
            subject.Id,
            biometricSample.Id,
            result.VectorId,
            result.ModelVersion);

        await _biometricTemplateRepository.AddAsync(
            biometricTemplate,
            cancellationToken);

        biometricSample.SetQualityScore(result.QualityScore);

        person.MarkAsEnrolled();

        enrollment.Complete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}