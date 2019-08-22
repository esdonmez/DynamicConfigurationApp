using Newtonsoft.Json;

namespace Lib_DynamicConfiguration.DataAccess.Cache
{
    public class CacheRepository : ICacheRepository
    {
        RedisConnection con = new RedisConnection();

        public CacheRepository()
        {

        }

        public T Get<T>(string key)
        {
            var cacheDb = con.Connection.GetDatabase();

            var value = cacheDb.StringGet(key);
            if (value.IsNullOrEmpty)
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        public void Insert<T>(string key, T value)
        {
            var cacheDb = con.Connection.GetDatabase();

            var obj = JsonConvert.SerializeObject(value);
            cacheDb.StringSet(key, obj);
        }

        public void Remove(string key)
        {
            var cacheDb = con.Connection.GetDatabase();

            cacheDb.KeyDelete(key);
        }
    }
}
