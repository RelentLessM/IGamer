namespace IGamer.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using IGamer.Common;
    using IGamer.Services.Data.Posts;
    using IGamer.Web.ViewModels.Administration.Posts;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : AdministrationController
    {
        private readonly IPostService postsService;

        public PostsController(IPostService postsService)
        {
            this.postsService = postsService;
        }

        public async Task<IActionResult> AllPosts(int page = 1)
        {
            var postsCount = await this.postsService.GetAllCountAsync();
            var pagesCount = (int)Math.Ceiling((double)postsCount / GlobalConstants.ItemsPerPage);
            if (page > pagesCount)
            {
                page = pagesCount;
            }

            if (page <= 0)
            {
                page = 1;
            }

            var posts = await this.postsService
                .GetAllAsync<PostForAdminViewModel>(GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var result = new AllPostsForAdminViewModel()
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
    }
}
