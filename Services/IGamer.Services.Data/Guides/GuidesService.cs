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

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            => await this.repository.All()
                .OrderByDescending(x => x.CreatedOn)
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
    }
}
