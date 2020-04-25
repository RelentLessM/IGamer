namespace IGamer.Services.Data.SearchBar
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISearchBarService
    {
        Task<IEnumerable<T>> SearchPost<T>(string input, int take, int skip = 0);

        Task<IEnumerable<T>> SearchGuide<T>(string input, int take, int skip = 0);
    }
}
