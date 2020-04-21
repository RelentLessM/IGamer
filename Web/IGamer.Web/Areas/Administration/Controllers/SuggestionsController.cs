namespace IGamer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Suggestions;
    using IGamer.Web.ViewModels.Administration.Suggestions;
    using Microsoft.AspNetCore.Mvc;

    public class SuggestionsController : AdministrationController
    {
        private readonly ISuggestionsService suggestionsService;

        public SuggestionsController(ISuggestionsService suggestionsService)
        {
            this.suggestionsService = suggestionsService;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var suggestions = await this.suggestionsService
                .GetAllForAdminAsync();
            var result = new AllSuggestionsForAdminViewModel()
            {
                Suggestions = suggestions,
            };

            return this.View(result);
        }
    }
}
