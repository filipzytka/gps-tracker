using ArduinoServer.Feature.Devices.Commands;
using ArduinoServer.Feature.Devices.Queries;
using ArduinoServer.Feature.GnssLogs.Configuration;
using ArduinoServer.Feature.GnssLogs.Queries;
using MediatR;
using Microsoft.Extensions.Options;

namespace ArduinoServer.Feature.Devices.Endpoint;

public static class DeviceEndpoints
{
    public static void MapDeviceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/device", async (string macAddress, string name, ISender sender,
         IOptions<ElasticConfiguration> options) =>
           {
               var command = new CreateDeviceCommand(macAddress, name);
               var response = await sender.Send(command);

               return response ? Results.Ok() : Results.BadRequest();
           });

        app.MapGet("api/device", async (ISender sender, IOptions<ElasticConfiguration> options) =>
        {
            var query = new GetDeviceQuery();
            var response = await sender.Send(query);

            return Results.Ok(response);
        });
    }
}