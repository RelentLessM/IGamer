using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IGamer.Data;
using IGamer.Data.Models;
using IGamer.Data.Repositories;
using IGamer.Services.Data.Games;
using IGamer.Services.Data.Guides;
using IGamer.Services.Mapping;
using IGamer.Web.ViewModels.Administration.Games;
using IGamer.Web.ViewModels.Guides;
using IGamer.Web.ViewModels.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IGamer.Services.Data.Tests
{
    public class GamesServiceTests
    {
        public GamesServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(SuggestionToGameViewModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task AddOnGameShouldAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Game>(context);
            var service = new GamesService(repository);
            var model = new SuggestionToGameViewModel()
            {
                Title = "test",
                Description = "test",
                ImageUrl = "test",
            };

            await service.AddAsync(model);
            var actual = await service.GetAll<GamesDropDownViewModel>();
            Assert.Single(actual);
        }

        [Fact]
        public async Task DoesGameExistShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Game>(context);
            var service = new GamesService(repository);
            var model = new SuggestionToGameViewModel()
            {
                Title = "test",
                Description = "test",
                ImageUrl = "test",
            };

            await service.AddAsync(model);
            var actual = await service.DoesGameExist(model.Title);
            Assert.True(actual);
        }

        [Fact]
        public async Task DoesGameExistShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Game>(context);
            var service = new GamesService(repository);
            var model = new SuggestionToGameViewModel()
            {
                Title = "test",
                Description = "test",
                ImageUrl = "test",
            };

            await service.AddAsync(model);
            var actual = await service.DoesGameExist("hahahaaa");
            Assert.False(actual);
        }

        [Fact]
        public async Task TakeNewShouldGetThree()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Game>(context);
            var service = new GamesService(repository);
            var models = new List<SuggestionToGameViewModel>
            {
                new SuggestionToGameViewModel()
                {
                    Title = "test",
                    Description = "test",
                    ImageUrl = "test",
                },
                new SuggestionToGameViewModel()
                {
                    Title = "test1",
                    Description = "test",
                    ImageUrl = "test",
                },
                new SuggestionToGameViewModel()
                {
                    Title = "test2",
                    Description = "test",
                    ImageUrl = "test",
                },
                new SuggestionToGameViewModel()
                {
                    Title = "test3",
                    Description = "test",
                    ImageUrl = "test",
                },
                new SuggestionToGameViewModel()
                {
                    Title = "test4",
                    Description = "test",
                    ImageUrl = "test",
                },
            };
            foreach (var model in models)
            {
                await service.AddAsync(model);
            }

            var actual = await service.TakeNewAsync<NewGamesViewModel>();
            Assert.Equal(3, actual.Count());
        }

        [Fact]
        public async Task TakeNewShouldGetTwo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Game>(context);
            var service = new GamesService(repository);
            var models = new List<SuggestionToGameViewModel>
            {
                new SuggestionToGameViewModel()
                {
                    Title = "test",
                    Description = "test",
                    ImageUrl = "test",
                },
                new SuggestionToGameViewModel()
                {
                    Title = "test1",
                    Description = "test",
                    ImageUrl = "test",
                },
            };
            foreach (var model in models)
            {
                await service.AddAsync(model);
            }

            var actual = await service.TakeNewAsync<NewGamesViewModel>();
            Assert.Equal(2, actual.Count());
        }
    }
}
