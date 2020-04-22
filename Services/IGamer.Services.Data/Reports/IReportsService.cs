namespace IGamer.Services.Data.Reports
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReportsService
    {
        Task<int?> AddReportToGuideAsync<T>(T model);

        Task<IEnumerable<T>> GetByGuideAsync<T>(string id);
    }
}
