namespace ArduinoServer.Model;

public class MqttMessage 
{
    public string Topic { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
}