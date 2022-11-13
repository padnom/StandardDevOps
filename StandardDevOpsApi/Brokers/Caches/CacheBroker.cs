using StackExchange.Redis;

using StandardDevOpsApi.Models.LibraryAccounts;
using StandardDevOpsApi.Models.LibraryCards;
using StandardDevOpsApi.Models.Students;

namespace StandardDevOpsApi.Brokers.Caches
{
    public partial class CacheBroker : ICacheBroker

    {
        private readonly IConfiguration configuration;
        private readonly IDatabase database;
        private ConnectionMultiplexer redis;

        public CacheBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.redis = InitRedisDatabase();
            this.database = this.redis.GetDatabase(0);
        }



        private ConnectionMultiplexer InitRedisDatabase()
        {
            string connectionString =
                            this.configuration.GetConnectionString("RedisCacheConnection");
            ConfigurationOptions configuration = ConfigurationOptions.Parse(connectionString, true);

            return ConnectionMultiplexer.Connect(configuration);

        }

        private IServer GetServer()
        {
            var endpoint = this.redis.GetEndPoints();
            return this.redis.GetServer(endpoint.First());
        }


    }
}