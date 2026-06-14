using BiometricPlatform.Application.Abstractions.Storage;

namespace BiometricPlatform.Infrastructure.Storage;

public sealed class LocalObjectStorage : IObjectStorage
{
    private readonly string _basePath = Path.Combine(
        Directory.GetCurrentDirectory(),
        "uploads");

    public async Task<string> UploadAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_basePath);

        var filePath = Path.Combine(_basePath, fileName);

        await using var fileStream = File.Create(filePath);

        await stream.CopyToAsync(fileStream, cancellationToken);

        return filePath;
    }
}