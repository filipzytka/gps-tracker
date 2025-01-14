using ArduinoServer.Feature.GnssLogs.Configuration;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;

namespace ArduinoServer.Feature.Elastic
{
    public static class DependencyInjection
    {
        public static void AddElasticConfiguration(this IServiceCollection services)
        {
            services.Configure<ElasticConfiguration>(options =>
            {
                options.ElasticUrl = Environment.GetEnvironmentVariable("ELASTIC_URL")!;
                options.Index = Environment.GetEnvironmentVariable("GNSS_INDEX")!;
            });
        }

        public static void AddElasticClient(this IServiceCollection services)
        {
            services.AddSingleton<ElasticsearchClient>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<ElasticConfiguration>>();
                var elasticUrl = options.Value.ElasticUrl;
                var settings = new Uri(elasticUrl!);
                return new ElasticsearchClient(settings);
            });
        }
    }
}