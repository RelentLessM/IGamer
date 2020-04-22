using System.Linq;

namespace IGamer.Services.Data.Games
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class GamesService : IGamesService
    {
        private readonly IRepository<Game> repository;

        public GamesService(IRepository<Game> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
            => await this.repository.All()
                .To<T>()
                .ToListAsync();

        public async Task AddAsync<T>(T model)
        {
            var game = AutoMapperConfig.MapperInstance.Map<Game>(model);

            await this.repository.AddAsync(game);
            await this.repository.SaveChangesAsync();
        }

        public Task<bool> DoesGameExist(string name)
            => this.repository.All()
                .AnyAsync(x => x.Title == name);

        public async Task<IEnumerable<T>> TakeNewAsync<T>()
            => await this.repository.All()
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();
    }
}
