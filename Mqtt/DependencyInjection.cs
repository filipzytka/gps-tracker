using ArduinoServer.Mqtt.Configuration;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;

namespace ArduinoServer.Mqtt
{
    public static class DependencyInjection
    {
        public static void AddMqttConfiguration(this IServiceCollection services)
        {
            services.Configure<MqttConfiguration>(options =>
            {
                options.Host = Environment.GetEnvironmentVariable("MQTT_HOST")!;
                options.Port = int.Parse(Environment.GetEnvironmentVariable("MQTT_PORT")!);
            });
        }

        public static void AddMqttClient(this IServiceCollection services)
        {
            services.AddScoped<MqttFactory>();

            services.AddScoped<IMqttClient>(provider =>
            {
                var mqttFactory = provider.GetRequiredService<MqttFactory>();
                return mqttFactory.CreateMqttClient();
            });

            services.AddScoped<IMqttPublisher>(provider =>
            {
                var client = provider.GetRequiredService<IMqttClient>();
                var options = provider.GetRequiredService<IOptions<MqttConfiguration>>();
                return new MqttPublisher(client, options);
            });
        }
    }
}