namespace IGamer.Services.Data.Reports
{
    using System.Threading.Tasks;

    public interface IReportsService
    {
        Task<int?> AddReportToGuideAsync<T>(T model);
    }
}
