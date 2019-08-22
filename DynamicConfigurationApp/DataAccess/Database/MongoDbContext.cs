using Lib_DynamicConfiguration.DataAccess.DTOs;
using Lib_DynamicConfiguration.Helpers;
using MongoDB.Driver;

namespace Lib_DynamicConfiguration.DataAccess.Database
{
    public class MongoDbContext : IMongoDbContext
    {
        private static IMongoDatabase _mongoDatabase;

        public MongoDbContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _mongoDatabase = client.GetDatabase(Constants.MongoDbName);
        }

        public IMongoCollection<ConfigDTO> Configurations => _mongoDatabase.GetCollection<ConfigDTO>(Constants.MongoCollectionName);
    }
}
