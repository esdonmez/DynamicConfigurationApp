using System.Threading.Tasks;
using Lib_DynamicConfiguration;
using Lib_DynamicConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_DynamicConfiguration.Pages
{
    public class ConfigurationEditModel : PageModel
    {
        [BindProperty]
        public Config Config { get; set; }
        private IConfigurationReader _configurationReader;


        public ConfigurationEditModel(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }


        public async void OnGet(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var configuration = await _configurationReader.GetByIdAsync(id);
                Config = configuration;
            }
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _configurationReader.UpdateAsync(Config);
            return RedirectToPage("Index");
        }
    }
}