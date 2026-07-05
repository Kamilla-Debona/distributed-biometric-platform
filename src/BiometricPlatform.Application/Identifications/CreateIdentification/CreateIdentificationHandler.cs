using BiometricPlatform.Application.Abstractions.Persistence;
using BiometricPlatform.Application.Abstractions.Storage;
using BiometricPlatform.Application.Identifications.ProcessIdentification;
using BiometricPlatform.Domain.Biometrics;
using BiometricPlatform.Domain.Identifications;
using Wolverine;

namespace BiometricPlatform.Application.Identifications.CreateIdentification;

public sealed class CreateIdentificationHandler(
    IIdentificationRepository identificationRepository,
    IBiometricSampleRepository biometricSampleRepository,
    IObjectStorage objectStorage,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork)
{
    public async Task<CreateIdentificationResponse> Handle(
        CreateIdentificationCommand command,
        CancellationToken cancellationToken)
    {
        var storagePath = await objectStorage.UploadAsync(
            command.Image,
            $"{Guid.NewGuid()}.jpg",
            cancellationToken);

        var probeSample = BiometricSample.CreateProbeSample(
            BiometricSampleType.Face,
            storagePath);

        await biometricSampleRepository.AddAsync(
            probeSample,
            cancellationToken);

        var identification = new Identification(
            command.GalleryId,
            probeSample.Id);

        await identificationRepository.AddAsync(
            identification,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await messageBus.InvokeAsync(
            new ProcessIdentificationCommand(identification.Id),
            cancellationToken);

        return new CreateIdentificationResponse(
            identification.Id);
    }
}