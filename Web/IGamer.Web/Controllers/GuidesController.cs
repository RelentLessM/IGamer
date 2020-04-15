using IGamer.Common;

namespace IGamer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Services.CloudinaryHelper;
    using IGamer.Services.Data.Games;
    using IGamer.Services.Data.Guides;
    using IGamer.Web.ViewModels.Guides;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
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

        public async Task<IActionResult> All(int page = 1)
        {
            var guides = await this.guidesService.GetAllAsync<GuideViewModel>();
            var model = new AllGuidesViewModel() { Guides = guides };

            var guidesCount = await this.guidesService.GetAllCountAsync();
            model.PagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage);
            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = page;

            return this.View(model);
        }

        public async Task<IActionResult> ByCategory(string name, int page = 1)
        {
            if (!Enum.TryParse(typeof(CategoryOfGuide), name, out _))
            {
                return this.RedirectToAction("All");
            }

            var enumResult = Enum.Parse<CategoryOfGuide>(name);
            var guides = await this.guidesService
                .GetByCategoryAsync<GuideViewModel>(enumResult, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new AllGuidesViewModel() { Guides = guides };

            var postsCount = await this.guidesService.GetCountByCategoryAsync(enumResult);
            result.PagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage);
            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            result.CurrentPage = page;
            return this.View(result);
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

        public async Task<IActionResult> Details(string id)
        {
            var guide = await this.guidesService.GetByIdAsync<DetailedGuideViewModel>(id);
            if (guide == null)
            {
                return this.RedirectToAction("All");
            }

            return this.View(guide);
        }
    }
}
