using ArduinoServer.Context;
using ArduinoServer.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArduinoServer.Feature.Devices.Queries;

    public class GetDeviceQueryHandler : IRequestHandler<GetDeviceQuery, List<Device>>
    {
        private readonly AppIdentityDbContext _context;

        public GetDeviceQueryHandler(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Device>> Handle(GetDeviceQuery request, CancellationToken cancellationToken)
        {
            return await _context.Devices.ToListAsync(cancellationToken);
        }
    }
