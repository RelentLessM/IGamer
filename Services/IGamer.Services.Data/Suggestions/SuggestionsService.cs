using IGamer.Web.ViewModels.Administration.Suggestions;

namespace IGamer.Services.Data.Suggestions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using IGamer.Web.ViewModels.Suggestions;
    using Microsoft.EntityFrameworkCore;

    public class SuggestionsService : ISuggestionsService
    {
        private readonly IRepository<SuggestionGame> repository;

        public SuggestionsService(IDeletableEntityRepository<SuggestionGame> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<SuggestionViewModel>> GetAllAsync()
        {
            var suggestions = await this.repository.All()
                .To<SuggestionViewModel>()
                .ToListAsync();
            var votesSum = await this.GetSuggestionsVotesSum();
            if (votesSum == 0)
            {
                votesSum = 1;
            }

            foreach (var suggestion in suggestions)
            {
                suggestion.Percentage = (suggestion.VotesCount * 100.0m) / votesSum;
            }

            return suggestions.OrderByDescending(x => x.Percentage);
        }

        public async Task<IEnumerable<SuggestionForDropDownViewModel>> GetAllForDropDownAsync()
        => await this.repository.All()
            .OrderBy(x => x.Id)
            .To<SuggestionForDropDownViewModel>()
            .ToListAsync();

        public async Task<int> CreateSuggestionAsync<T>(T model)
        {
            var suggestion = AutoMapperConfig.MapperInstance.Map<SuggestionGame>(model);

            await this.repository.AddAsync(suggestion);
            await this.repository.SaveChangesAsync();

            return suggestion.Id;
        }

        public async Task<bool> DoesSuggestionExist(string name)
        => await this.repository.All()
            .AnyAsync(x => x.Title.ToLower() == name.ToLower());

        public async Task<IEnumerable<SuggestionForAdminViewModel>> GetAllForAdminAsync()
        => await this.repository.All()
            .OrderByDescending(x => x.Votes.Count)
            .To<SuggestionForAdminViewModel>()
            .ToListAsync();

        private async Task<int> GetSuggestionsVotesSum()
            => await this.repository.All()
                .SelectMany(x => x.Votes)
                .CountAsync();
    }
}
