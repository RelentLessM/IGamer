namespace IGamer.Services.Data.ServiceModels
{
    using System;

    using AutoMapper;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Services.Mapping;

    public class CreatePostServiceModel : IMapTo<Post>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public virtual string Category { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreatePostServiceModel, Post>()
                .ForMember(x => x.Description, s => s.MapFrom(x => x.Content))
                .ForMember(x => x.Category, s => s.MapFrom(x => Enum.Parse<CategoryOfPost>(x.Category)));
        }
    }
}