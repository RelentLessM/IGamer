namespace IGamer.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using IGamer.Data;
    using IGamer.Data.Models;
    using IGamer.Data.Repositories;
    using IGamer.Services.Data.Suggestions;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Suggestions;
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
        public async Task AddOnSuggestionShouldAdd()
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
        public async Task GetAllShouldReturnOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var suggestionGameRepository = new EfDeletableEntityRepository<SuggestionGame>(context);
            var service = new SuggestionsService(suggestionGameRepository);
            var actual = await service.GetAllAsync();

            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetAllForDropDownShouldReturnOne()
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
            var actual = await service.GetAllForDropDownAsync();

            Assert.Single(actual);
        }

        [Fact]
        public async Task DoesSuggestionExistShouldReturnTrue()
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
            var actual = await service.DoesSuggestionExist(model.Title);

            Assert.True(actual);
        }

        [Fact]
        public async Task DoesSuggestionExistShouldReturnFalse()
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
            var actual = await service.DoesSuggestionExist("Nqma meeee");

            Assert.False(actual);
        }

        [Fact]
        public async Task GetAllForAdminShouldReturnOne()
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
                Title = "TestingMethod",
                UserId = "1",
            };
            await service.CreateSuggestionAsync(model);
            var actual = await service.GetAllForAdminAsync();

            Assert.Single(actual);
        }

        [Fact]
        public async Task GetByIdShouldReturnModel()
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
                Title = "TestingMethod",
                UserId = "1",
            };
            var id = await service.CreateSuggestionAsync(model);
            var actual = await service.GetById<SuggestionViewModel>(id);

            Assert.IsType<SuggestionViewModel>(actual);
        }

        [Fact]
        public async Task DeleteShouldMarkAsDeleted()
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
                Title = "TestingMethod",
                UserId = "1",
            };
            var id = await service.CreateSuggestionAsync(model);
            await service.DeleteSuggestionAsync(id);
            var actual = await service.GetAllAsync();

            Assert.Empty(actual);
        }
    }
}
