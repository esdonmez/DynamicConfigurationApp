using Lib_DynamicConfiguration.DataAccess.DTOs;
using MongoDB.Driver;

namespace Lib_DynamicConfiguration.DataAccess.Database
{
    interface IMongoDbContext
    {
        IMongoCollection<ConfigDTO> Configurations { get; }
    }
}
