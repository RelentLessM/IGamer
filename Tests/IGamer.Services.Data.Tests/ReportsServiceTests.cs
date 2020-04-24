namespace IGamer.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using IGamer.Data;
    using IGamer.Data.Models;
    using IGamer.Data.Repositories;
    using IGamer.Services.Data.Guides;
    using IGamer.Services.Data.Reports;
    using IGamer.Services.Data.Suggestions;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Administration.Reports;
    using IGamer.Web.ViewModels.Guides;
    using IGamer.Web.ViewModels.Reports;
    using IGamer.Web.ViewModels.Suggestions;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ReportsServiceTests
    {
        public ReportsServiceTests()
        {
            AutoMapperConfig
                .RegisterMappings(typeof(AddReportToGuideInputModel).GetTypeInfo().Assembly);
        }

        [Fact]
        public async Task AddOnReportShouldAdd()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Report>(context);
            var service = new ReportsService(repository);
            var model = new AddReportToGuideInputModel()
            {
                UserId = "1",
                GuideId = "1",
                Reason = "tupooooo",
            };

            var actual = await service.AddReportToGuideAsync(model);

            Assert.True(actual.HasValue);
        }

        [Fact]
        public async Task AddOnReportTwiceShouldAddOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Report>(context);
            var service = new ReportsService(repository);
            var model = new AddReportToGuideInputModel()
            {
                UserId = "1",
                GuideId = "1",
                Reason = "tupooooo",
            };
            await service.AddReportToGuideAsync(model);
            var actual = await service.AddReportToGuideAsync(model);

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetReportsShouldGetOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ApplicationDbContext(options.Options);

            var repository = new EfRepository<Report>(context);
            var guideRepository = new EfDeletableEntityRepository<Guide>(context);
            var service = new ReportsService(repository);
            var guideService = new GuidesService(guideRepository);
            var guideModel = new CreateGuideInputModel()
            {
                Category = "Action",
                Description = "someDesc",
                GameId = "1",
                ImageUrl = "google",
                Title = "test",
            };
            var guideId = await guideService.CreateAsync(guideModel, "1");

            var model = new AddReportToGuideInputModel()
            {
                UserId = "1",
                GuideId = guideId,
                Reason = "tupooooo",
            };
            await service.AddReportToGuideAsync(model);
            var actual = await service.GetByGuideAsync<ReportForGuideViewModel>(model.GuideId);

            Assert.Single(actual);
        }
    }
}
