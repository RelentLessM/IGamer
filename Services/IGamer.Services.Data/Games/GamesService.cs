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
            => await this.repository.All().To<T>().ToListAsync();
    }
}
