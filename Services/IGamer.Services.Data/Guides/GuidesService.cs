namespace IGamer.Services.Data.Guides
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Data.Models.Enums;
    using IGamer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class GuidesService : IGuidesService
    {
        private readonly IDeletableEntityRepository<Guide> repository;

        public GuidesService(IDeletableEntityRepository<Guide> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int take, int skip = 0)
            => await this.repository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

        public async Task<int> GetAllCountAsync()
            => await this.repository.All()
                .CountAsync();

        public async Task<string> CreateAsync<T>(T model, string userId)
        {
            var guide = AutoMapperConfig.MapperInstance.Map<Guide>(model);
            guide.UserId = userId;
            await this.repository.AddAsync(guide);
            await this.repository.SaveChangesAsync();

            var guideId = guide.Id;

            return guideId;
        }

        public async Task<T> GetByIdAsync<T>(string id)
            => await this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetByCategoryAsync<T>(CategoryOfGuide categoryName, int take, int skip = 0)
            => await this.repository.All()
                .Where(x => x.Category == categoryName)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

        public async Task<int> GetCountByCategoryAsync(CategoryOfGuide categoryName)
            => await this.repository.All()
                .Where(x => x.Category == categoryName)
                .CountAsync();

        public async Task<IEnumerable<T>> GetRecentAsync<T>()
            => await this.repository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(5)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> GetByUserAsync<T>(string userId, int take, int skip = 0)
            => await this.repository.All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

        public async Task<int> GetCountByUserAsync(string userId)
            => await this.repository.All()
                .Where(x => x.UserId == userId)
                .CountAsync();

        public async Task<bool> DoesGuideBelongToUserAsync(string userId, string id)
            => await this.repository.All().AnyAsync(x => x.UserId == userId && x.Id == id);

        public async Task<T> GetGuideByIdAsync<T>(string id)
            => await this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefaultAsync();

        public async Task DeleteGuideAsync(string id)
        {
            var guide = await this.repository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            this.repository.Delete(guide);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllForAdminAsync<T>(int take, int skip)
            => await this.repository.All()
                .OrderByDescending(x => x.Reports.Count)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();
    }
}
