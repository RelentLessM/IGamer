namespace IGamer.Web.ViewModels.Administration.Posts
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class PostForAdminViewModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}