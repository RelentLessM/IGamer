using System.Collections.Generic;
using System.Linq;

namespace IGamer.Services.Data.Reports
{
    using System.Threading.Tasks;

    using IGamer.Data.Common.Repositories;
    using IGamer.Data.Models;
    using IGamer.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ReportsService : IReportsService
    {
        private readonly IRepository<Report> repository;

        public ReportsService(IRepository<Report> repository)
        {
            this.repository = repository;
        }

        public async Task<int?> AddReportToGuideAsync<T>(T model)
        {
            var report = AutoMapperConfig.MapperInstance.Map<Report>(model);
            if (await this.repository.All().AnyAsync(x => x.UserId == report.UserId))
            {
                return null;
            }

            await this.repository.AddAsync(report);
            await this.repository.SaveChangesAsync();
            return report.Id;
        }

        public async Task<IEnumerable<T>> GetByGuideAsync<T>(string id)
            => await this.repository.All().Where(x => x.GuideId == id).To<T>().ToListAsync();
    }
}
