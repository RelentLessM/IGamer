using CloudinaryDotNet;
using IGamer.Data.Models;
using IGamer.Services.CloudinaryHelper;
using IGamer.Services.Data.Votes;
using IGamer.Web.ViewModels.Votes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IGamer.Web.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Suggestions;
    using IGamer.Web.ViewModels.Suggestions;
    using Microsoft.AspNetCore.Mvc;

    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsService suggestionsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly Cloudinary cloudinary;
        private readonly IVotesService votesService;

        public SuggestionsController(
            ISuggestionsService suggestionsService,
            UserManager<ApplicationUser> userManager,
            ICloudinaryHelper cloudinaryHelper,
            Cloudinary cloudinary,
            IVotesService votesService)
        {
            this.suggestionsService = suggestionsService;
            this.userManager = userManager;
            this.cloudinaryHelper = cloudinaryHelper;
            this.cloudinary = cloudinary;
            this.votesService = votesService;
        }

        public async Task<IActionResult> All()
        {
            var suggestions = await this.suggestionsService.GetAllAsync();
            var suggestionsForDropDown = await this.suggestionsService.GetAllForDropDownAsync();
            var model = new AllSuggestionsViewModel()
            {
                Suggestions = suggestions,
                SuggestionsForDropDown = suggestionsForDropDown,
            };
            return this.View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSuggestionInputModel model, IFormFile file)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            model.UserId = userId;

            if (!this.ModelState.IsValid || model.UserId == null || file == null)
            {
                return this.View(model);
            }

            var doesItExist = await this.suggestionsService.DoesSuggestionExist(model.Title);
            if (doesItExist)
            {
                this.TempData["SuggestionExist"] = "This game is already suggested! :)";
                return this.View(model);
            }

            var imageUrl = await this.cloudinaryHelper.UploadSuggestionImageAsync(this.cloudinary, file);
            model.ImageUrl = imageUrl;

            var suggestionId = await this.suggestionsService.CreateSuggestionAsync(model);
            var result = await this.votesService.VoteOnSuggestionGameAsync(suggestionId, userId);
            return this.RedirectToAction("All");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Vote(AddVoteToSuggestionGameInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            if (!this.ModelState.IsValid || userId == null)
            {
                this.TempData["ReturnMessage"] = "Something went wrong!";
                return this.RedirectToAction("All");
            }

            model.UserId = userId;
            var result = await this.votesService.VoteOnSuggestionGameAsync(model.SuggestionGameId, model.UserId);

            if (result != null)
            {
                this.TempData["ReturnMessage"] = result;
                return this.RedirectToAction("All");
            }

            return this.RedirectToAction("All");
        }
    }
}
