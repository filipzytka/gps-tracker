using System.Globalization;
using ArduinoServer.Model;
using Elastic.Clients.Elasticsearch;
using MediatR;

namespace ArduinoServer.Feature.GnssLogs.Queries;

public class SearchPaginatedGNSSLogsQueryHandler : IRequestHandler<SearchPaginatedGNSSLogsQuery, PaginatedList<GNSSData>>
{
    private readonly ElasticsearchClient _client;

    public SearchPaginatedGNSSLogsQueryHandler(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<PaginatedList<GNSSData>> Handle(SearchPaginatedGNSSLogsQuery request, CancellationToken cancellationToken)
    {
        int from = request.PageNumber * request.PageSize;
        var response = string.IsNullOrEmpty(request.Search) ? await _client.SearchAsync<GNSSData>(s => s
              .Index(request.Index)
              .From(from)
              .Size(request.PageSize)
           )
       :
      await _client.SearchAsync<GNSSData>(s => s
              .Index(request.Index)
              .From(from)
              .Size(request.PageSize)
            .Query(q => q
                .MultiMatch(c => c
                .Query(request.Search)
                )
            )
    );
        if (!response.IsValidResponse)
        {
            return [];
        }

        var source = response.Documents.ToList();
        var totalCount = response.Total;
        PaginatedList<GNSSData> list = new( source.OrderByDescending(list => DateTime.Parse(list.Date)).ToList(), request.PageNumber, request.PageSize, totalCount);
        return list;
    }
}