namespace IGamer.Web.ViewModels.Guides
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class EditGuideViewModel : IMapFrom<Guide>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [MinLength(400, ErrorMessage = "Content must be at least 400 symbols.")]
        public string Content { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Guide, EditGuideViewModel>()
                .ForMember(x => x.Content, s => s.MapFrom(x => x.Description));
        }
    }
}
