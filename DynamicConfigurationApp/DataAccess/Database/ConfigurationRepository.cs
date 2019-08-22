using Lib_DynamicConfiguration.DataAccess.DTOs;
using Lib_DynamicConfiguration.Infrastructure;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib_DynamicConfiguration.DataAccess.Database
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private IMongoCollection<ConfigDTO> _collection;


        public ConfigurationRepository()
        {
            _collection = IoC.Resolve<IMongoDbContext>().Configurations;
        }


        public async Task<ConfigDTO> GetByIdAsync(string id)
        {
            return await _collection
                .Find(Builders<ConfigDTO>.Filter.Eq(g => g.Id.ToString(), id))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ConfigDTO>> GetAllAsync()
        {
            return await _collection
                .Find(Builders<ConfigDTO>.Filter.Empty)
                .ToListAsync();
        }

        public async Task InsertAsync(ConfigDTO model)
        {
            await _collection.InsertOneAsync(model);
        }

        public async Task<bool> UpdateAsync(ConfigDTO config)
        {
            var result =
                await _collection
                    .ReplaceOneAsync(
                        Builders<ConfigDTO>.Filter.Eq(g => g.Id, config.Id),
                        config);
            return result.IsAcknowledged
                   && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result =
                await _collection
                    .DeleteOneAsync(Builders<ConfigDTO>.Filter.Eq(g => g.Id.ToString(), id));
            return result.IsAcknowledged
                   && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<ConfigDTO>> GetAllByApplicationNameAsync(string applicationName)
        {
            return await _collection
                .Find(Builders<ConfigDTO>.Filter.Eq(g => g.ApplicationName, applicationName))
                .ToListAsync();
        }

        public async Task<IEnumerable<ConfigDTO>> GetAllByApplicationNameAndIsActiveAsync(string applicationName, bool isActive)
        {
            return await _collection
                .Find(filter: g => g.ApplicationName == applicationName && g.IsActive == isActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<ConfigDTO>> FilterByNameAsync(string searchedName, string applicationName)
        {
            return await _collection
                .Find(filter: g => g.ApplicationName == applicationName && g.Name.ToLower().Contains(searchedName.ToLower()))
                .ToListAsync();
        }

        public async Task<bool> IsExistsAsync(string key, string applicationName)
        {
            return await _collection
                    .Find(filter: a => a.Name == key && a.ApplicationName == applicationName)
                    .CountDocumentsAsync() > 0;
        }
    }
}
