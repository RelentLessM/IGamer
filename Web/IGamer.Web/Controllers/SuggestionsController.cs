using CloudinaryDotNet;
using IGamer.Data.Models;
using IGamer.Services.CloudinaryHelper;
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

        public SuggestionsController(
            ISuggestionsService suggestionsService,
            UserManager<ApplicationUser> userManager,
            ICloudinaryHelper cloudinaryHelper,
            Cloudinary cloudinary)
        {
            this.suggestionsService = suggestionsService;
            this.userManager = userManager;
            this.cloudinaryHelper = cloudinaryHelper;
            this.cloudinary = cloudinary;
        }

        public async Task<IActionResult> All()
        {
            var suggestions = await this.suggestionsService.GetAllAsync();
            var model = new AllSuggestionsViewModel() { Suggestions = suggestions };
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

            var imageUrl = await this.cloudinaryHelper.UploadSuggestionImageAsync(this.cloudinary, file);
            model.ImageUrl = imageUrl;

            await this.suggestionsService.CreateSuggestionAsync(model);
            return this.RedirectToAction("All");
        }
    }
}
