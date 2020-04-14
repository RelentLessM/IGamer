namespace IGamer.Services.Data.Guides
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
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

        public async Task<string> CreateAsync<T>(T model, string userId)
        {
            var guide = AutoMapperConfig.MapperInstance.Map<Guide>(model);
            guide.UserId = userId;
            await this.repository.AddAsync(guide);
            await this.repository.SaveChangesAsync();

            var guideId = guide.Id;

            return guideId;
        }
    }
}
