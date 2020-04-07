namespace IGamer.Web.ViewModels.Posts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Services.Mapping;

    public class CreatePostInputModel : IMapTo<Post>, IHaveCustomMappings
    {
        [Required]
        [StringLength(20, ErrorMessage = "The title must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "Content must be more than 20 characters.")]
        public string Content { get; set; }

        [Required]
        public virtual string Category { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreatePostInputModel, Post>()
                .ForMember(x => x.Description, s => s.MapFrom(x => x.Content))
                .ForMember(x => x.Category, s => s.MapFrom(x => Enum.Parse<CategoryOfPost>(x.Category)));
        }
    }
}