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

        DataSet Sub_Exposure_TIV(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_LocationsCount(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_BuildingsCount(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_ConstructionType(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_OccupancyType(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_YearBuild(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_ProtectionClass(string clientId, string userEmailId, string submissionId);
    }
}
