namespace IGamer.Web.ViewModels.Suggestions
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class SuggestionForDropDownViewModel : IMapFrom<SuggestionGame>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
