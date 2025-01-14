using ArduinoServer.Context;
using MediatR;

namespace ArduinoServer.Feature.Devices.Commands.AddGNSSDocument;

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, bool>
{
    private readonly AppIdentityDbContext _context;

    public CreateDeviceCommandHandler(AppIdentityDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        _context.Add(request);

        return await _context.SaveChangesAsync() > 1;
    }
}