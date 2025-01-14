using ArduinoServer.Feature.UbxProtocol.Commands.Restart;
using ArduinoServer.Feature.UbxProtocol.Commands.SetInterval;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArduinoServer.Feature.UbxProtocol.Endpoint;

public static class UbxEndpoints
{
    public static void MapNavigationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/command/interval", async ([FromQuery] int seconds, ISender sender) =>
        {
            var command = new SetIntervalCommand(seconds);

            await sender.Send(command);

            return Results.Ok();
        });

        app.MapPost("api/command/restart", async (ISender sender) =>
        {
            var command = new RestartCommand();

            await sender.Send(command);

            return Results.Ok();
        });
    }
}