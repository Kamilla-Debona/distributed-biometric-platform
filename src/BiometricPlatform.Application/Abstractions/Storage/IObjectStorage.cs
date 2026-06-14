namespace BiometricPlatform.Application.Abstractions.Storage;

//Esse contrato vai permitir salvar imagem em blob depois,
//sem a Application saber se é local, S3, Azure Blob ou GCS.
public interface IObjectStorage
{
    Task<string> UploadAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken);
}