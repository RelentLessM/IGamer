namespace IGamer.Services.Data.Games
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGamesService
    {
        Task<IEnumerable<T>> GetAll<T>();
    }
}
