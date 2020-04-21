using AutoMapper;
using IGamer.Common;
using IGamer.Web.ViewModels.Suggestions;

namespace IGamer.Web.ViewModels.Administration.Suggestions
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class SuggestionForAdminViewModel : IMapFrom<SuggestionGame>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int VotesCount { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SuggestionGame, SuggestionForAdminViewModel>()
                .ForMember(x => x.ImageUrl, s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl));
        }
    }
}
