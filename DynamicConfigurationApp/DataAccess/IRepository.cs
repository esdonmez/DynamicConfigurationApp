using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib_DynamicConfiguration.DataAccess
{
    public interface IRepository<TModel>
    {
        Task<TModel> GetByIdAsync(string id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task InsertAsync(TModel model);
        Task<bool> UpdateAsync(TModel model);
        Task<bool> DeleteAsync(string id);
    }
}
