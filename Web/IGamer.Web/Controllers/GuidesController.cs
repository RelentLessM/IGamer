namespace IGamer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using IGamer.Common;
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
    using Microsoft.EntityFrameworkCore;

    [Authorize]
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
            var guidesCount = await this.guidesService.GetAllCountAsync();
            var pagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var guides = await this.guidesService.GetAllAsync<GuideViewModel>(GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var model = new AllGuidesViewModel()
            {
                Guides = guides,
                PagesCount = pagesCount,
            };

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
            var guidesCount = await this.guidesService.GetCountByCategoryAsync(enumResult);
            var pagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var guides = await this.guidesService
                .GetByCategoryAsync<GuideViewModel>(enumResult, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new AllGuidesViewModel
            {
                Guides = guides,
                PagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage),
            };

            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            if (page > result.PagesCount)
            {
                page = result.PagesCount;
            }

            result.CurrentPage = page;
            return this.View(result);
        }

        public async Task<IActionResult> ByUser(string username, int page = 1)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
            {
                return this.RedirectToAction("All");
            }

            var userId = await this.userManager.GetUserIdAsync(user);
            var guidesCount = await this.guidesService.GetCountByUserAsync(userId);
            var pagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var guides = await this.guidesService
                .GetByUserAsync<GuideViewModel>(userId, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var result = new AllGuidesViewModel()
            {
                Guides = guides,
                PagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage),
            };

            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            if (page > result.PagesCount)
            {
                page = result.PagesCount;
            }

            result.CurrentPage = page;
            return this.View(result);
        }

        public async Task<IActionResult> MyGuides(int page = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);

            var guidesCount = await this.guidesService.GetCountByUserAsync(userId);
            var pagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var guides = await this.guidesService
                .GetByUserAsync<GuideViewModel>(userId, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new AllGuidesViewModel()
            {
                Guides = guides,
                PagesCount = (int)Math.Ceiling((double)guidesCount / GlobalConstants.ItemsPerPage),
            };

            if (result.PagesCount == 0)
            {
                result.PagesCount = 1;
            }

            if (page > result.PagesCount)
            {
                page = result.PagesCount;
            }

            result.CurrentPage = page;
            return this.View(result);
        }

        public async Task<IActionResult> Create()
        {
            var games = await this.gamesService.GetAll<GamesDropDownViewModel>();
            var model = new CreateGuideInputModel() { Games = games };
            return this.View(model);
        }

        [HttpPost]
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

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            var doesGuideBelongToUser = await this.guidesService.DoesGuideBelongToUserAsync(userId, id);
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && !doesGuideBelongToUser)
            {
                return this.RedirectToAction("All", "Guides");
            }

            var guideForDelete = await this.guidesService.GetByIdAsync<DeleteGuideViewModel>(id);
            if (guideForDelete == null)
            {
                return this.NotFound();
            }

            return this.View(guideForDelete);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteGuideViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["InfoMessage"] = "Something went wrong!";
                return this.View(model);
            }

            await this.guidesService.DeleteGuideAsync(model.Id);
            this.TempData["InfoMessage"] = "Guide successfully deleted!";
            return this.RedirectToAction("All", "Guides");
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            var doesGuideBelongToUser = await this.guidesService.DoesGuideBelongToUserAsync(userId, id);
            if (!this.User.IsInRole(GlobalConstants.ModeratorRoleName) && !doesGuideBelongToUser)
            {
                return this.RedirectToAction("All", "Guides");
            }

            var guideToEdit = await this.guidesService.GetByIdAsync<EditGuideViewModel>(id);
            if (guideToEdit == null)
            {
                return this.NotFound();
            }

            return this.View(guideToEdit);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditGuideViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["InfoMessage"] = "Something went wrong!";
                return this.View(model);
            }

            await this.guidesService.EditGuideAsync(model.Id, model.Title, model.Content);
            this.TempData["InfoMessage"] = "Guide successfully edited!";
            return this.RedirectToAction("Details", "Guides", new { id = model.Id });
        }
    }
}
