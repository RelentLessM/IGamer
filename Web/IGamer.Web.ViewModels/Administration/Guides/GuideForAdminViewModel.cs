using AutoMapper;
using IGamer.Common;

namespace IGamer.Web.ViewModels.Administration.Guides
{
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class GuideForAdminViewModel : IMapFrom<Guide>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int ReportsCount { get; set; }

        public string ImageUrl { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Guide, GuideForAdminViewModel>()
                .ForMember(x => x.ImageUrl, s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl));
        }
    }
}
