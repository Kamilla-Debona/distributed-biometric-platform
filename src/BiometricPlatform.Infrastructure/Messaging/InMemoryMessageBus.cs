using System.Text.Json;
using BiometricPlatform.Application.Abstractions.Messaging;

namespace BiometricPlatform.Infrastructure.Messaging;

public sealed class InMemoryMessageBus : IMessageBus
{
    public Task PublishAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("Message published:");
        Console.WriteLine(JsonSerializer.Serialize(message));

        return Task.CompletedTask;
    }
}