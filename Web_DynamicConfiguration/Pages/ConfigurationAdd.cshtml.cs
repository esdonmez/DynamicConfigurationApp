using System.Threading.Tasks;
using Lib_DynamicConfiguration;
using Lib_DynamicConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_DynamicConfiguration.Pages
{
    public class ConfigurationAddModel : PageModel
    {
        [BindProperty]
        public Config Config { get; set; }
        private IConfigurationReader _configurationReader;


        public ConfigurationAddModel(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _configurationReader.AddAsync(Config);
            return RedirectToPage("Index");
        }
    }
}