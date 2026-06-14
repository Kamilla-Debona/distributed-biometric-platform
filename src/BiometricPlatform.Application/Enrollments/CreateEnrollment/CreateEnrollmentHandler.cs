using BiometricPlatform.Application.Abstractions.Messaging;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Abstractions.Storage;
using BiometricPlatform.Application.Enrollments.Messages;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Enrollments;
using BiometricPlatform.Domain.Identity;

namespace BiometricPlatform.Application.Enrollments.CreateEnrollment;

public sealed class CreateEnrollmentHandler
    : ICommandHandler<CreateEnrollmentCommand, CreateEnrollmentResponse>
{
    private readonly IBiographicDataRepository _biographicDataRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IBiometricSampleRepository _biometricSampleRepository;
    private readonly IObjectStorage _objectStorage;
    private readonly IMessageBus _messageBus;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEnrollmentHandler(
        IBiographicDataRepository biographicDataRepository,
        IPersonRepository personRepository,
        IEnrollmentRepository enrollmentRepository,
        IBiometricSampleRepository biometricSampleRepository,
        IObjectStorage objectStorage,
        IMessageBus messageBus,
        IUnitOfWork unitOfWork)
    {
        _biographicDataRepository = biographicDataRepository;
        _personRepository = personRepository;
        _enrollmentRepository = enrollmentRepository;
        _biometricSampleRepository = biometricSampleRepository;
        _objectStorage = objectStorage;
        _messageBus = messageBus;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CreateEnrollmentResponse> Handle(
        CreateEnrollmentCommand command,
        CancellationToken cancellationToken)
    {
        var biographicData = new BiographicData(
            command.FullName,
            command.Document);

        var person = new Person(
            command.ClientId,
            biographicData.Id);

        var enrollment = new Enrollment(
            person.Id,
            command.GalleryId);

        var storagePath = await _objectStorage.UploadAsync(
            command.Image,
            $"{Guid.NewGuid()}.jpg",
            cancellationToken);

        var biometricSample = new BiometricSample(
            person.Id,
            enrollment.Id,
            BiometricSampleType.Face,
            storagePath);

        await _biographicDataRepository.AddAsync(
            biographicData,
            cancellationToken);

        await _personRepository.AddAsync(
            person,
            cancellationToken);

        await _enrollmentRepository.AddAsync(
            enrollment,
            cancellationToken);

        await _biometricSampleRepository.AddAsync(
            biometricSample,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var enrollmentRequestedEvent = new EnrollmentRequestedMessage(
            enrollment.Id,
            person.Id,
            command.GalleryId,
            biometricSample.Id,
            biometricSample.StoragePath);

        await _messageBus.PublishAsync(
            enrollmentRequestedEvent,
            cancellationToken);

        return new CreateEnrollmentResponse(
            enrollment.Id,
            person.Id);
    }
}