using ArduinoServer.Mqtt.Configuration;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;

namespace ArduinoServer.Mqtt;

public interface IMqttPublisher 
{
    public Task Publish(string topic, string payload);
}

public class MqttPublisher : IMqttPublisher
{
    private readonly IMqttClient _client;
    private readonly MqttConfiguration _config;

    public MqttPublisher(IMqttClient client, IOptions<MqttConfiguration> config)
    {
        _client = client;
        _config = config.Value;
    }

    public async Task Publish(string topic, string payload)
    {
        if (!_client.IsConnected)
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                        .WithTcpServer(_config.Host, _config.Port)
                        .Build();

            await _client.ConnectAsync(mqttClientOptions);
        }

        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .Build();

        await _client.PublishAsync(applicationMessage);
    }
}