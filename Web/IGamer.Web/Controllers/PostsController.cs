using Microsoft.EntityFrameworkCore;

namespace IGamer.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Services.Data.Posts;
    using IGamer.Services.Data.ServiceModels;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        public IActionResult ByCategory(string name)
        {
            if (!Enum.TryParse(typeof(CategoryOfPost), name, out _))
            {
                return this.RedirectToAction("All");
            }

            var enumResult = Enum.Parse<CategoryOfPost>(name);
            var posts = this.postService.GetByCategory<PostViewModel>(enumResult);
            var result = new PostsAllViewModel() { Posts = posts };
            return this.View(result);
        }

        public async Task<IActionResult> ByUser(string username)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                return this.RedirectToAction("All");
            }

            var posts = this.postService.GetByUser<PostViewModel>(user.Id);
            var result = new PostsAllViewModel() { Posts = posts };
            return this.View(result);
        }

        public IActionResult All()
        {
            var posts = this.postService.GetAll<PostViewModel>();
            var result = new PostsAllViewModel() { Posts = posts };
            return this.View(result);
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!Enum.TryParse(typeof(CategoryOfPost), model.Category, out _))
            {
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);

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
    }
}
