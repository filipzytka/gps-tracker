using MediatR;

namespace ArduinoServer.Feature.UbxProtocol.Commands.Restart;

public class RestartCommand : IRequest 
{
    public string Restart { get; set; } = string.Empty;
}
