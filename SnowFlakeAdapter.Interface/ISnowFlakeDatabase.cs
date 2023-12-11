using Models.DTO;
using System.Data;

namespace SnowFlakeAdapter.Interface
{
    public interface ISnowFlakeDatabase
    {
        DataSet SampleTest();
        DataSet DashboardGraph_CountByTurnaroundTime(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByLOB(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByByBroker(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByCity(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByState(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByIndustries(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
    }
}
