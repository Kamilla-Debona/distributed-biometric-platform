using BiometricPlatform.Application.Abstractions.Messaging;
using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Abstractions.Storage;
using BiometricPlatform.Application.Enrollments.ProcessEnrollment;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Enrollments;
using BiometricPlatform.Domain.Identity;
using Wolverine;

namespace BiometricPlatform.Application.Enrollments.CreateEnrollment;

public sealed class CreateEnrollmentHandler(
    IBiographicDataRepository biographicDataRepository,
    IPersonRepository personRepository,
    IEnrollmentRepository enrollmentRepository,
    IBiometricSampleRepository biometricSampleRepository,
    IObjectStorage objectStorage,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateEnrollmentCommand, CreateEnrollmentResponse>
{
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

        var storagePath = await objectStorage.UploadAsync(
            command.Image,
            $"{Guid.NewGuid()}.jpg",
            cancellationToken);

        var biometricSample = new BiometricSample(
            person.Id,
            enrollment.Id,
            BiometricSampleType.Face,
            storagePath);

        await biographicDataRepository.AddAsync(
            biographicData,
            cancellationToken);

        await personRepository.AddAsync(
            person,
            cancellationToken);

        await enrollmentRepository.AddAsync(
            enrollment,
            cancellationToken);

        await biometricSampleRepository.AddAsync(
            biometricSample,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var processEnrollmentCommand = new ProcessEnrollmentCommand(
            enrollment.Id);

        await messageBus.InvokeAsync(
            processEnrollmentCommand,
            cancellationToken);

        return new CreateEnrollmentResponse(
            enrollment.Id,
            person.Id);
    }
}