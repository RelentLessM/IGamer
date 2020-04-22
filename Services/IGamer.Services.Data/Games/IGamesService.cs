namespace IGamer.Services.Data.Games
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGamesService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task AddAsync<T>(T model);

        Task<bool> DoesGameExist(string name);

        Task<IEnumerable<T>> TakeNewAsync<T>();
    }
}
