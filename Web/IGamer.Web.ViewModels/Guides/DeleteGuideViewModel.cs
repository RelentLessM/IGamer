namespace IGamer.Web.ViewModels.Guides
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class DeleteGuideViewModel : IMapFrom<Guide>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public virtual string Category { get; set; }
    }
}
