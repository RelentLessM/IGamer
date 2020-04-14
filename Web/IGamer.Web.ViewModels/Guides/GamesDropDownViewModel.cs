namespace IGamer.Web.ViewModels.Guides
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class GamesDropDownViewModel : IMapFrom<Game>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}