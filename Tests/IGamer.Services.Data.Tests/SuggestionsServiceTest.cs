using System.Linq;
using System.Reflection;
using IGamer.Services.Data.Suggestions;
using IGamer.Services.Mapping;
using IGamer.Web.ViewModels.Suggestions;
using Moq;

namespace IGamer.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using IGamer.Data;
    using IGamer.Data.Models;
    using IGamer.Data.Repositories;
    using IGamer.Services.Data.Votes;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class SuggestionsServiceTest
    {
        public SuggestionsServiceTest()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(CreateSuggestionInputModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task AddOnSuggestion()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var suggestionGameRepository = new EfDeletableEntityRepository<SuggestionGame>(context);
            var service = new SuggestionsService(suggestionGameRepository);
            var model = new CreateSuggestionInputModel()
            {
                Description = "SomeDescSomeDescSomeDescSomeDescSomeDescSomeDescSomeDescSomeDescSomeDescSomeDesc",
                ImageUrl = "google.com",
                Title = "New SuggestedGame",
                UserId = "1",
            };
            await service.CreateSuggestionAsync(model);
            var actual = await service.GetAllAsync();

            Assert.Single(actual);
        }

        [Fact]
        public async Task GetAllTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var suggestionGameRepository = new EfDeletableEntityRepository<SuggestionGame>(context);
            var service = new SuggestionsService(suggestionGameRepository);
            var actual = await service.GetAllAsync();

            Assert.Empty(actual);
        }
    }
}
