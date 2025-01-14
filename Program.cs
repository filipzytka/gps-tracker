using ArduinoServer;
using ArduinoServer.Context;
using ArduinoServer.Entity;
using ArduinoServer.Feature.Devices.Endpoint;
using ArduinoServer.Feature.Elastic;
using ArduinoServer.Feature.GnssLogs.Endpoint;
using ArduinoServer.Feature.UbxProtocol.Endpoint;
using ArduinoServer.Mqtt;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.AddKestrelConfiguration();
builder.Services.AddMqttConfiguration();
builder.Services.AddMqttClient();
builder.Services.AddElasticConfiguration();
builder.Services.AddElasticClient();
builder.Services.AddIdentityCore<User>();
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppIdentityDbContext>();
builder.Services.AddMediatR(configuration => {
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("cors-policy", (pb) =>
    {
        pb.WithOrigins(Environment.GetEnvironmentVariable("REACT_CLIENT_URL")!)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("cors-policy");
app.MapNavigationEndpoints();
app.MapIdentityApi<User>();
app.MapElasticEndpoints();
app.MapDeviceEndpoints();
app.ApplyMigrations();
app.Run();
