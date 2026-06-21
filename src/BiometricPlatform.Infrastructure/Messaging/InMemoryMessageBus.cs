using System.Text.Json;
using BiometricPlatform.Application.Abstractions.Messaging;
using BiometricPlatform.Application.Enrollments.Messages;
using BiometricPlatform.Application.Enrollments.ProcessEnrollment;

namespace BiometricPlatform.Infrastructure.Messaging;

public sealed class InMemoryMessageBus : IMessageBus
{
    private readonly ProcessEnrollmentHandler _processEnrollmentHandler;

    public InMemoryMessageBus(
        ProcessEnrollmentHandler processEnrollmentHandler)
    {
        _processEnrollmentHandler = processEnrollmentHandler;
    }

    public async Task PublishAsync<TMessage>(
        TMessage message,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("Message published:");
        Console.WriteLine(JsonSerializer.Serialize(message));

        if (message is EnrollmentRequestedMessage enrollmentRequestedMessage)
        {
            var command = new ProcessEnrollmentCommand(
                enrollmentRequestedMessage.EnrollmentId);

            await _processEnrollmentHandler.Handle(
                command,
                cancellationToken);
        }
    }
}