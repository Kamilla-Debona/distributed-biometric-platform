namespace BiometricPlatform.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> Handle(
        TCommand command,
        CancellationToken cancellationToken);
}