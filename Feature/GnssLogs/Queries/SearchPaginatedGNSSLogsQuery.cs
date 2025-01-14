using ArduinoServer.Model;
using MediatR;

namespace ArduinoServer.Feature.GnssLogs.Queries;

public class SearchPaginatedGNSSLogsQuery : IRequest<PaginatedList<GNSSData>>
{
    public string Index { get; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Search { get; set; }
    
    public SearchPaginatedGNSSLogsQuery(string index, int pageNumber, int pageSize, string? search)
    {
        Index = index;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Search = search;
    }
}