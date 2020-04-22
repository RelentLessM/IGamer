namespace IGamer.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;

    public class EditPostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The title must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Content must be more than 20 characters.")]
        public string Content { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, EditPostViewModel>()
                .ForMember(x => x.Content, s => s.MapFrom(x => x.Description));
        }
    }
}
