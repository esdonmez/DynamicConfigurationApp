using AutoMapper;
using Lib_DynamicConfiguration.DataAccess.Cache;
using Lib_DynamicConfiguration.DataAccess.Database;
using Lib_DynamicConfiguration.DataAccess.DTOs;
using Lib_DynamicConfiguration.Models;
using Lib_DynamicConfiguration.Helpers;
using Lib_DynamicConfiguration.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Lib_DynamicConfiguration
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly string _applicationName;
        public static string _connectionString;
        private readonly int _refreshTimerIntervalInMs;
        public CancellationToken _cancellationToken { get; set; }
        private IMapper mapper;

        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs = 30000)
        {
            _applicationName = applicationName;
            _connectionString = connectionString;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
            
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ConfigDTO, Config>().ReverseMap(); });
            mapper = config.CreateMapper();

            Task.Run(() => this.LoadData()).Wait();
            Task.Run(() => this.StartTimer(_cancellationToken));
        }

        private List<Config> cachedConfigList = IoC.Resolve<ICacheRepository>().Get<List<Config>>(Constants.RedisKey);

        public T GetValue<T>(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return default(T);
            var cachedItem = cachedConfigList.FirstOrDefault(a => a.Name == name && a.IsActive);
            if (cachedItem == null)
                return default(T);
            TypeHelper.TryCast(cachedItem.Value, out T result);
            return result;
        }

        public async Task<bool> AddAsync(Config model)
        {
            if (model == null)
                return false;
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Value))
                return false;

            var isExist = await IoC.Resolve<IConfigurationRepository>().IsExistsAsync(model.Name, _applicationName);
            if (!isExist)
                return false;

            model.ApplicationName = _applicationName;
            await IoC.Resolve<IConfigurationRepository>().InsertAsync(mapper.Map<Config,ConfigDTO>(model));
            return true;
        }

        public async Task<bool> UpdateAsync(Config model)
        {
            if (model == null)
                return false;
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Value))
                return false;

            return await IoC.Resolve<IConfigurationRepository>().UpdateAsync(mapper.Map<Config, ConfigDTO>(model));
        }

        public async Task<List<Config>> GetAllAsync()
        {
            var list = (await IoC.Resolve<IConfigurationRepository>().GetAllByApplicationNameAsync(_applicationName)).ToList();
            List<Config> configList = new List<Config>();
            list.ForEach(i => configList.Add(mapper.Map<ConfigDTO, Config>(i)));
            return configList;
        }

        public async Task<List<Config>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Config>();
            var list = (await IoC.Resolve<IConfigurationRepository>().FilterByNameAsync(name, _applicationName)).ToList();
            List<Config> configList = new List<Config>();
            list.ForEach(i => configList.Add(mapper.Map<ConfigDTO, Config>(i)));
            return configList;
        }

        public async Task<Config> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;
            return mapper.Map<ConfigDTO, Config>(await IoC.Resolve<IConfigurationRepository>().GetByIdAsync(id));
        }
        
        private async Task LoadData()
        {
            var list = IoC.Resolve<IConfigurationRepository>().GetAllByApplicationNameAndIsActiveAsync(_applicationName, true);

            if (cachedConfigList == null)
                IoC.Resolve<ICacheRepository>().Insert(Constants.RedisKey, list);
            else
                await ReloadCacheList();
        }

        private async Task StartTimer(CancellationToken cancellationToken)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await LoadData();
                    await Task.Delay(_refreshTimerIntervalInMs, cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }, cancellationToken);
        }

        public async Task ReloadCacheList()
        {
            var newcachedConfigList = cachedConfigList != null ? new List<Config>(cachedConfigList) : new List<Config>();
            var configurationList = await IoC.Resolve<IConfigurationRepository>().GetAllByApplicationNameAndIsActiveAsync(_applicationName, true);

            if (!configurationList.Any())
                return;

            foreach (var configuration in configurationList)
            {
                var existingCacheItem = newcachedConfigList.FirstOrDefault(a => a.Id.Equals(configuration.Id));
                if (existingCacheItem != null)
                {
                    existingCacheItem.Type = configuration.Type;
                    existingCacheItem.Value = configuration.Value;
                    existingCacheItem.IsActive = configuration.IsActive;
                }
                else
                {
                    newcachedConfigList.Add(mapper.Map<ConfigDTO, Config>(configuration));
                }
            }

            IoC.Resolve<ICacheRepository>().Remove(Constants.RedisKey);
            IoC.Resolve<ICacheRepository>().Insert(Constants.RedisKey, newcachedConfigList);
        }
    }
}