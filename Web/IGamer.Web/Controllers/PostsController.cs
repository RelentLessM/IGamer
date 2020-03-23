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

            // var serviceModel = AutoMapperConfig.MapperInstance.Map<CreatePostServiceModel>(model);

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
