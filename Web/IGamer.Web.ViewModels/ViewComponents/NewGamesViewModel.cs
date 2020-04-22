namespace IGamer.Web.ViewModels.ViewComponents
{
    using AutoMapper;
    using IGamer.Common;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class NewGamesViewModel : IMapFrom<Game>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, NewGamesViewModel>()
                .ForMember(x => x.ImageUrl, s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl));
        }
    }
}
