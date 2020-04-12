namespace IGamer.Web.ViewComponents
{
    using System.Threading.Tasks;

    using IGamer.Services.Data.Posts;
    using IGamer.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "RecentPosts")]
    public class RecentPostsViewComponent : ViewComponent
    {
        private readonly IPostService postService;

        public RecentPostsViewComponent(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await this.postService.GetRecentAsync<RecentPostsViewModel>();
            var model = new RecentPostsListViewModel() { Posts = posts };

            return this.View(model);
        }
    }
}
