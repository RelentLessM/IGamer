namespace IGamer.Web.ViewModels.Guides
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Services.Mapping;

    public class CreateGuideInputModel : IMapTo<Guide>, IHaveCustomMappings
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [MinLength(400, ErrorMessage = "Content must be at least 400 symbols.")]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }


        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Category { get; set; }

        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Game is required!")]
        [Display(Name = "Game")]
        public string GameId { get; set; }

        public IEnumerable<GamesDropDownViewModel> Games { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CreateGuideInputModel, Guide>()
                .ForMember(x => x.Category, s => s.MapFrom(x => Enum.Parse<CategoryOfGuide>(x.Category)));
        }
    }
}
