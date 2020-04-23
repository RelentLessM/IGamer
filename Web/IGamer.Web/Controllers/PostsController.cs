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
    using IGamer.Services.Data.Posts;
    using IGamer.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryHelper cloudinaryHelper;
        private readonly Cloudinary cloudinary;

        public PostsController(
            IPostService postService,
            UserManager<ApplicationUser> userManager,
            ICloudinaryHelper cloudinaryHelper,
            Cloudinary cloudinary)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.cloudinaryHelper = cloudinaryHelper;
            this.cloudinary = cloudinary;
        }

        public async Task<IActionResult> ByCategory(string name, int page = 1)
        {
            if (!Enum.TryParse(typeof(CategoryOfPost), name, out _))
            {
                return this.RedirectToAction("All");
            }

            var enumResult = Enum.Parse<CategoryOfPost>(name);
            var postsCount = await this.postService.GetCountByCategoryAsync(enumResult);
            var pagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var posts = await this.postService
                .GetByCategoryAsync<PostViewModel>(enumResult, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new PostsAllViewModel
            {
                Posts = posts,
                PagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage),
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
            var postsCount = await this.postService.GetCountByUserAsync(userId);
            var pagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var posts = await this.postService
                .GetByUserAsync<PostViewModel>(userId, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var result = new PostsAllViewModel
            {
                Posts = posts,
                PagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage),
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

        [Authorize]
        public async Task<IActionResult> MyPosts(int page = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);

            var postsCount = await this.postService.GetCountByUserAsync(userId);
            var pagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var posts = await this.postService
                .GetByUserAsync<PostViewModel>(userId, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new PostsAllViewModel
            {
                Posts = posts,
                PagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage),
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

        public async Task<IActionResult> All(int page = 1)
        {
            var postsCount = await this.postService.GetAllCountAsync();
            var pagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var posts = await this.postService
                .GetAllAsync<PostViewModel>(GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new PostsAllViewModel()
            {
                Posts = posts,
                PagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage),
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

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostInputModel model, IFormFile file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!Enum.TryParse(typeof(CategoryOfPost), model.Category, out _))
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            var imageUrl = await this.cloudinaryHelper.UploadPostImageAsync(this.cloudinary, file);

            model.ImageUrl = imageUrl;
            var postId = await this.postService.CreateAsync(model, userId);

            return this.RedirectToAction("DetailedPost", new { id = postId });
        }

        public async Task<IActionResult> DetailedPost(string id)
        {
            var post = await this.postService.DetailsAsync<DetailedPostViewModel>(id);

            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            var doesPostBelongToUser = await this.postService.DoesPostBelongToUserAsync(userId, id);
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName) && !doesPostBelongToUser)
            {
                return this.RedirectToAction("All", "Posts");
            }

            var postForDelete = await this.postService.GetPostByIdAsync<DeletePostViewModel>(id);
            if (postForDelete == null)
            {
                return this.NotFound();
            }

            return this.View(postForDelete);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(DeletePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["InfoMessage"] = "Something went wrong!";
                return this.View(model);
            }

            await this.postService.DeletePostAsync(model.Id);
            this.TempData["InfoMessage"] = "Post successfully deleted!";
            return this.RedirectToAction("All", "Posts");
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = await this.userManager.GetUserIdAsync(user);
            var doesPostBelongToUser = await this.postService.DoesPostBelongToUserAsync(userId, id);
            if (!this.User.IsInRole(GlobalConstants.ModeratorRoleName) && !doesPostBelongToUser)
            {
                return this.RedirectToAction("All", "Posts");
            }

            var postForEdit = await this.postService.GetPostByIdAsync<EditPostViewModel>(id);
            if (postForEdit == null)
            {
                return this.NotFound();
            }

            return this.View(postForEdit);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["InfoMessage"] = "Something went wrong!";
                return this.View(model);
            }

            await this.postService.EditPostAsync(model.Id, model.Title, model.Content);
            this.TempData["InfoMessage"] = "Post successfully edited!";
            return this.RedirectToAction("DetailedPost", "Posts", new { id = model.Id });
        }
    }
}
