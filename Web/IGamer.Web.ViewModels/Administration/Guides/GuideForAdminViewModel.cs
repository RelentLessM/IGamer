namespace IGamer.Web.ViewModels.Administration.Guides
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class GuideForAdminViewModel : IMapFrom<Guide>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
