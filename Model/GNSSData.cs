namespace ArduinoServer.Model;

public class GNSSData
{
    public string MacAddress {get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public float Lat { get; set; }
    public float Lon { get; set; }
    public float Set { get; set; }
    public float Prec { get; set; }
    public float Alt { get; set; }
    public float Course { get; set; }
    public float Speed_kmph { get; set; }
}
