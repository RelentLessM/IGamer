using System;
using System.Threading.Tasks;
using IGamer.Data;
using IGamer.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IGamer.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Services.Data.Votes;
    using Moq;
    using Xunit;

    public class VotesServiceTest
    {
        [Fact]
        public async Task TwoVotesOnPostFromOneUserShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            var postRepository = new EfRepository<VoteOnPost>(context);
            var guideRepository = new EfRepository<VoteOnGuide>(context);
            var suggestionGameRepository = new EfRepository<VoteOnSuggestionGame>(context);
            var service = new VotesService(postRepository, guideRepository, suggestionGameRepository);

            await service.VoteOnPostAsync("1", "1", true);
            await service.VoteOnPostAsync("1", "1", true);
            var actual = await service.GetVotesOnPostAsync("1");

            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task TwoVotesOnGuideFromOneUserShouldCountOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            var postRepository = new EfRepository<VoteOnPost>(context);
            var guideRepository = new EfRepository<VoteOnGuide>(context);
            var suggestionGameRepository = new EfRepository<VoteOnSuggestionGame>(context);
            var service = new VotesService(postRepository, guideRepository, suggestionGameRepository);

            await service.VoteOnGuideAsync("1", "1", true);
            await service.VoteOnGuideAsync("1", "1", true);
            var actual = await service.GetVotesOnGuideAsync("1");

            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task SecondVoteOnSuggestionGameFromOneUserShouldNotCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            var postRepository = new EfRepository<VoteOnPost>(context);
            var guideRepository = new EfRepository<VoteOnGuide>(context);
            var suggestionGameRepository = new EfRepository<VoteOnSuggestionGame>(context);
            var service = new VotesService(postRepository, guideRepository, suggestionGameRepository);

            await service.VoteOnSuggestionGameAsync(1, "1");
            var actual = await service.VoteOnSuggestionGameAsync(1, "1");
            var expected = "You have already voted for this game!";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task VoteOnSuggestionGameAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);
            var postRepository = new EfRepository<VoteOnPost>(context);
            var guideRepository = new EfRepository<VoteOnGuide>(context);
            var suggestionGameRepository = new EfRepository<VoteOnSuggestionGame>(context);
            var service = new VotesService(postRepository, guideRepository, suggestionGameRepository);

            await service.VoteOnSuggestionGameAsync(1, "1");
            var actual = suggestionGameRepository.All().Count();
            var expected = 1;

            Assert.Equal(expected, actual);
        }
    }
}
