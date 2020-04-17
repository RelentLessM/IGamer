using System.Threading.Tasks;
using IGamer.Services.Data.Suggestions;
using IGamer.Web.ViewModels.Suggestions;
using Microsoft.AspNetCore.Mvc;

namespace IGamer.Web.Controllers
{
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsService suggestionsService;

        public SuggestionsController(ISuggestionsService suggestionsService)
        {
            this.suggestionsService = suggestionsService;
        }

        public async Task<IActionResult> All()
        {
            var suggestions = await this.suggestionsService.GetAllAsync();
            var model = new AllSuggestionsViewModel() { Suggestions = suggestions };
            return this.View(model);
        }
    }
}
