
namespace IGamer.Services.Data.Guides
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGuidesService
    {
        Task<IEnumerable<T>> GetAll<T>();
    }
}
