using System.Text.Json;
using ArduinoServer.Mqtt;
using MediatR;

namespace ArduinoServer.Feature.UbxProtocol.Commands.Restart;

public class SetIntervalCommandHandler : IRequestHandler<RestartCommand>
{
    readonly IMqttPublisher _publisher;

    public SetIntervalCommandHandler(IMqttPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(RestartCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish("command", JsonSerializer.Serialize(request));
    }
}