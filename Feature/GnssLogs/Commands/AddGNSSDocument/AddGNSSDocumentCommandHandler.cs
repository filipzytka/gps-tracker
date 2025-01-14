using Elastic.Clients.Elasticsearch;
using MediatR;

namespace ArduinoServer.Feature.GnssLogs.Commands.AddGNSSDocument;

public class AddGNSSDocumentCommandHandler : IRequestHandler<AddGNSSDocumentCommand, bool>
{
    private readonly ElasticsearchClient _client;

    public AddGNSSDocumentCommandHandler(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<bool> Handle(AddGNSSDocumentCommand request, CancellationToken cancellationToken)
    {
        var result = await _client.IndexAsync(
              request.Data,
               idx => idx.Index(request.Index).OpType(OpType.Index));

        return result.IsValidResponse;
    }
}