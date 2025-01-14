using ArduinoServer.Entity;
using MediatR;

namespace ArduinoServer.Feature.Devices.Queries;

public class GetDeviceQuery : IRequest<List<Device>> { }
