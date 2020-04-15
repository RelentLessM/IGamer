namespace IGamer.Web.ViewModels.ViewComponents
{
    using System;

    using AutoMapper;
    using IGamer.Common;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class RecentGuidesViewModel : IMapFrom<Guide>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ShortTitle => this.Title.Length > 10
            ? this.Title.Substring(0, 10) + "..."
            : this.Title;

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Guide, RecentGuidesViewModel>()
                .ForMember(x => x.ImageUrl, s => s.MapFrom(x => GlobalConstants.DefaultCloudinary + x.ImageUrl));
        }
    }
}
