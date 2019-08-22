using StackExchange.Redis;
using System;

namespace Lib_DynamicConfiguration.DataAccess.Cache
{
    public class RedisConnection
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection;

        static RedisConnection()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                var configOptions = new ConfigurationOptions();
                configOptions.EndPoints.Add("localhost");
                return ConnectionMultiplexer.Connect(configOptions);
            });
        }

        public ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
