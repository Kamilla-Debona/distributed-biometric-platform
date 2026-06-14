namespace BiometricPlatform.Application.Abstractions.Biometrics;

public interface IBiometricEngine
{
    Task<CreateSubjectResult> CreateSubjectAsync(
        string imagePath,
        CancellationToken cancellationToken);
}   