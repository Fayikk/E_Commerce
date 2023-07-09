using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace E_CommerceForUdemy_API.Extension
{
    public static class ElasticSearchExtension
    {
        public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
        {

            //var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));

            //var settings = new ConnectionSettings(pool);

            //var client = new ElasticClient(settings);
            //var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!));
            var userName = configuration.GetSection("Elastic")["Username"];
            var password = configuration.GetSection("Elastic")["Password"];
            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!)).Authentication(new BasicAuthentication(userName!, password!));
            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
