using System.Threading.Tasks;

namespace Sibintek.BeerMachine.Services
{
    public interface IReportService
    {
        Task<ReportData> GetReport();
    }
}