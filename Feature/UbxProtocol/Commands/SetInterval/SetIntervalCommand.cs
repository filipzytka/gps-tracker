using MediatR;

namespace ArduinoServer.Feature.UbxProtocol.Commands.SetInterval;

public class SetIntervalCommand : IRequest 
{
    public int Time { get; set; }

    public SetIntervalCommand(int time)
    {
        Time = time;
    }
}