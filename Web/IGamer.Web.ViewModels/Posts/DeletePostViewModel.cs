namespace IGamer.Web.ViewModels.Posts
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class DeletePostViewModel : IMapFrom<Post>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public virtual string Category { get; set; }
    }
}
