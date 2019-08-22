using Lib_DynamicConfiguration.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib_DynamicConfiguration
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
        Task<List<Config>> SearchByNameAsync(string name);
        Task<List<Config>> GetAllAsync();
        Task<bool> AddAsync(Config model);
        Task<bool> UpdateAsync(Config model);
        Task<Config> GetByIdAsync(string id);
    }
}