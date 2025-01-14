namespace ArduinoServer;

public static class DependencyInjection
{
    public static void AddKestrelConfiguration(this ConfigureWebHostBuilder builder)
    {
        builder.UseKestrel()
            .UseUrls(Environment.GetEnvironmentVariable("BACKEND_URL")!);
    }
}

