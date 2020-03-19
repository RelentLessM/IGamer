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
            var posts = this.postService.GetAll<PostViewServiceModel>().To<PostViewModel>().ToList();
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

            //var serviceModel = AutoMapperConfig.MapperInstance.Map<CreatePostServiceModel>(model);

            var userId = userManager.GetUserId(this.User);

            await this.postService.Create(model, userId);

            return this.Redirect("/");
        }
    }
}
