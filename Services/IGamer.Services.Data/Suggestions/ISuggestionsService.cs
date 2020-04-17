namespace IGamer.Services.Data.Suggestions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Web.ViewModels.Suggestions;

    public interface ISuggestionsService
    {
        Task<IEnumerable<SuggestionViewModel>> GetAllAsync();

        Task CreateSuggestionAsync<T>(T model);
    }
}
