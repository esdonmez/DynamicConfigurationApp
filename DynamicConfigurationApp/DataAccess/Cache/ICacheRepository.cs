namespace Lib_DynamicConfiguration.DataAccess.Cache
{
    public interface ICacheRepository
    {
        T Get<T>(string key);
        void Insert<T>(string key, T value);
        void Remove(string key);
    }
}
