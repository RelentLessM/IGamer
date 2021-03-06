﻿namespace IGamer.Services.Data.SearchBar
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class SearchBarService : ISearchBarService
    {
        private readonly IDeletableEntityRepository<Post> postRepository;
        private readonly IDeletableEntityRepository<Guide> guideRepository;

        public SearchBarService(IDeletableEntityRepository<Post> postRepository, IDeletableEntityRepository<Guide> guideRepository)
        {
            this.postRepository = postRepository;
            this.guideRepository = guideRepository;
        }

        public async Task<IEnumerable<T>> SearchPost<T>(string input, int take, int skip = 0)
            => await this.postRepository.All()
                .Where(x => x.Title.Contains(input)
                            || x.Description.Contains(input))
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> SearchGuide<T>(string input, int take, int skip = 0)
            => await this.guideRepository.All()
                .Where(x => x.Title.Contains(input)
                            || x.Description.Contains(input))
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take(take)
                .To<T>()
                .ToListAsync();
    }
}
