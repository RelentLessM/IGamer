using AutoMapper;
using IGamer.Common;

namespace IGamer.Web.ViewModels.Suggestions
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class SuggestionViewModel : IMapFrom<SuggestionGame>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int VotesCount { get; set; }

        public decimal Percentage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SuggestionGame, SuggestionViewModel>()
                .ForMember(x => x.ImageUrl, s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl));
        }
    }
}
