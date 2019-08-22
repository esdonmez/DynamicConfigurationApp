using Lib_DynamicConfiguration.DataAccess.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib_DynamicConfiguration.DataAccess.Database
{
    public interface IConfigurationRepository : IRepository<ConfigDTO>
    {
        Task<IEnumerable<ConfigDTO>> GetAllByApplicationNameAsync(string applicationName);
        Task<IEnumerable<ConfigDTO>> GetAllByApplicationNameAndIsActiveAsync(string applicationName, bool isActive);
        Task<IEnumerable<ConfigDTO>> FilterByNameAsync(string searchedName, string applicationName);
        Task<bool> IsExistsAsync(string key, string applicationName);
    }
}
