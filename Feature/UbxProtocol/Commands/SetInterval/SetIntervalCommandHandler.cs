using System.Text.Json;
using ArduinoServer.Feature.UbxProtocol.Commands.SetInterval;
using ArduinoServer.Mqtt;
using MediatR;

namespace ArduinoServer.Commands.SetInterval;

public class SetIntervalCommandHandler : IRequestHandler<SetIntervalCommand>
{
    readonly IMqttPublisher _publisher;

    public SetIntervalCommandHandler(IMqttPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(SetIntervalCommand request, CancellationToken cancellationToken)
    {
        await _publisher.Publish("command", JsonSerializer.Serialize(request));
    }
}