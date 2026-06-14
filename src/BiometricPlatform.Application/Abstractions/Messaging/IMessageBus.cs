namespace BiometricPlatform.Application.Abstractions.Messaging;

public interface IMessageBus
{
    Task PublishAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken);
}