using IGamer.Web.ViewModels.Administration.Suggestions;

namespace IGamer.Services.Data.Suggestions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IGamer.Web.ViewModels.Suggestions;

    public interface ISuggestionsService
    {
        Task<IEnumerable<SuggestionViewModel>> GetAllAsync();

        Task<IEnumerable<SuggestionForDropDownViewModel>> GetAllForDropDownAsync();

        Task<int> CreateSuggestionAsync<T>(T model);

        Task<bool> DoesSuggestionExist(string name);

        Task<IEnumerable<SuggestionForAdminViewModel>> GetAllForAdminAsync();

        Task<T> GetById<T>(int id);

        Task DeleteSuggestionAsync(int id);
    }
}
