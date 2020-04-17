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
            .OrderByDescending(x => x.Id)
            .To<SuggestionViewModel>()
            .ToListAsync();
            var votesSum = await this.GetSuggestionsVotesSum();
            foreach (var suggestion in suggestions)
            {
                suggestion.Percentage = (suggestion.Votes * 100.0m) / votesSum;
            }

            return suggestions;
        }

        public async Task CreateSuggestionAsync<T>(T model)
        {
            var suggestion = AutoMapperConfig.MapperInstance.Map<SuggestionGame>(model);
            suggestion.Votes = 1;

            await this.repository.AddAsync(suggestion);
            await this.repository.SaveChangesAsync();
        }

        private async Task<int> GetSuggestionsVotesSum()
            => await this.repository.All()
                .SumAsync(x => x.Votes);
    }
}
