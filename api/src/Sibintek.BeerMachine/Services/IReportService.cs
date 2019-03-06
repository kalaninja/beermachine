using System.Threading.Tasks;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public interface IReportService
    {
        Task<ReportData> GetReport();
    }
}