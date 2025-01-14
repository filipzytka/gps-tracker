using MediatR;

namespace ArduinoServer.Feature.Devices.Commands;

public class CreateDeviceCommand : IRequest<bool>
{
    public string MacAddress { get; } = string.Empty;
    public string Name { get; } = string.Empty;

    public CreateDeviceCommand(string macAddress, string name)
    {
        MacAddress = macAddress;
        Name = name;
    }
}