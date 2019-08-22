using System.Collections.Generic;
using System.Linq;
using Lib_DynamicConfiguration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_DynamicConfiguration.Models;

namespace Web_DynamicConfiguration.Pages
{
    public class IndexModel : PageModel
    {
        private IConfigurationReader _configurationReader;
        public List<Config> ConfigurationList { get; set; }
        public string SearchValue { get; set; }


        public IndexModel(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }


        public async void OnGet(string value)
        {
            SearchValue = value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                var list = await _configurationReader.SearchByNameAsync(value);
                ConfigurationList = list.Select(a => new Config
                {
                    Type = a.Type,
                    Value = a.Value,
                    IsActive = a.IsActive,
                    Name = a.Name,
                    Id = a.Id
                }).ToList();
            }
            else
            {
                var list = await _configurationReader.GetAllAsync();
                ConfigurationList = list.Select(a => new Config
                {
                    Type = a.Type,
                    Value = a.Value,
                    IsActive = a.IsActive,
                    Name = a.Name,
                    Id = a.Id
                }).ToList();
            }
        }
    }
}
