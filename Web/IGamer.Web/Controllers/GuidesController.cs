using System;
using System.Linq;
using CloudinaryDotNet;
using IGamer.Data.Models;
using IGamer.Data.Models.Enums;
using IGamer.Services.CloudinaryHelper;
using IGamer.Services.Data.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IGamer.Web.Controllers
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Guides;
    using IGamer.Web.ViewModels.Guides;
    using Microsoft.AspNetCore.Mvc;

    public class GuidesController : Controller
    {
        private readonly IGuidesService guidesService;
        private readonly IGamesService gamesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly Cloudinary cloudinary;

        public GuidesController(
            IGuidesService guidesService,
            IGamesService gamesService,
            UserManager<ApplicationUser> userManager,
            ICloudinaryHelper cloudinaryHelper,
            Cloudinary cloudinary)
        {
            this.guidesService = guidesService;
            this.gamesService = gamesService;
            this.userManager = userManager;
            this.cloudinaryHelper = cloudinaryHelper;
            this.cloudinary = cloudinary;
        }

        public async Task<IActionResult> All()
        {
            var guides = await this.guidesService.GetAllAsync<GuideViewModel>();
            var model = new AllGuidesViewModel() { Guides = guides };

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var games = await this.gamesService.GetAll<GamesDropDownViewModel>();
            var model = new CreateGuideInputModel() { Games = games };
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateGuideInputModel model, IFormFile file)
        {
            var games = await this.gamesService.GetAll<GamesDropDownViewModel>();
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            var imageUrl = await this.cloudinaryHelper.UploadGuideImageAsync(this.cloudinary, file);

            model.ImageUrl = imageUrl;
            model.UserId = userId;
            model.Games = games;
            if (!this.ModelState.IsValid || string.IsNullOrWhiteSpace(userId) || file == null)
            {
                return this.View(model);
            }

            if (!Enum.TryParse(typeof(CategoryOfGuide), model.Category, out _))
            {
                return this.View(model);
            }

            if (!games.Select(x => x.Id).Contains(model.GameId))
            {
                return this.View(model);
            }
            var guideId = await this.guidesService.CreateAsync(model, userId);

            return this.RedirectToAction("Details", new { id = guideId });
        }

        public IActionResult Details(string id)
        {
            return this.Ok("post created");
        }
    }
}
