using ArduinoServer.Feature.GnssLogs.Commands.AddGNSSDocument;
using ArduinoServer.Feature.GnssLogs.Configuration;
using ArduinoServer.Feature.GnssLogs.Queries;
using ArduinoServer.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ArduinoServer.Feature.GnssLogs.Endpoint;

public static class GnssLogsEndpoints
{
    public static void MapElasticEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/gnss", async ([FromBody] GNSSData data, ISender sender,
         IOptions<ElasticConfiguration> options) =>
           {
               var command = new AddGNSSDocumentCommand(options.Value.Index, data);
               var response = await sender.Send(command);

               return response ? Results.Ok() : Results.BadRequest();
           });

        app.MapGet("api/gnss", async (int pageNumber, int pageSize, string? search, ISender sender, IOptions<ElasticConfiguration> options) =>
        {
            var query = new SearchPaginatedGNSSLogsQuery(options.Value.Index, pageNumber, pageSize, search);
            var response = await sender.Send(query);

            return Results.Ok(new
            {
                logs = response,
                response.CurrentPage,
                response.PageSize,
                response.TotalCount,
                response.TotalPages,
                response.HasPreviousPage,
                response.HasNextPage
            });
        });
    }
}