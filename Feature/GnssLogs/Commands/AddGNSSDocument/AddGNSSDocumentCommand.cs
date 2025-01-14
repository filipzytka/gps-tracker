using ArduinoServer.Model;
using MediatR;

namespace ArduinoServer.Feature.GnssLogs.Commands.AddGNSSDocument;

public class AddGNSSDocumentCommand : IRequest<bool>
{
    public string Index { get; } = string.Empty;
    public GNSSData Data { get; set; } = null!;

    public AddGNSSDocumentCommand(string index, GNSSData data)
    {
        Index = index;
        Data = data;
    }
}