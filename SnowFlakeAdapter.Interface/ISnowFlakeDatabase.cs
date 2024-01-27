using Models.DTO;
using System.Data;

namespace SnowFlakeAdapter.Interface
{
    public interface ISnowFlakeDatabase
    {
        /// <summary>
        /// Dashboard page
        /// </summary>
        /// <param name="TopNumber"></param>
        /// <param name="clientId"></param>
        /// <param name="UserEmailId"></param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        DataSet DashboardGraph_CountByTurnaroundTime(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByLOB(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByByBroker(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByCity(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByState(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountByIndustries(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate);
        DataSet DashboardGraph_CountOfSubmissionId(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate);
        DataSet DashboardGraph_CountOfDocType(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate);

        /// <summary>
        /// Exposure Summary page
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userEmailId"></param>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        DataSet Sub_Exposure_TIV(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_LocationsCount(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_BuildingsCount(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_ConstructionType(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_OccupancyType(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_YearBuild(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Exposure_ProtectionClass(string clientId, string userEmailId, string submissionId);

        /// <summary>
        /// Loss Summary Page
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userEmailId"></param>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        DataSet Sub_Loss_ClaimByLobByYear(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_IncurredByLobByYear(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_IncurredRangeCount(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_ClaimByClaimTypeByYear(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_IncurredByClaimTypeByYear(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_ClaimByClaimType(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_ClaimStatus(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_TotalIncurred(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Loss_TopLocations(string clientId, string userEmailId, string submissionId);
        DataSet GetSubmissionHeader(string clientId, string userEmailId, string submissionId);

        /// <summary>
        /// Stored Proc for Summary Page for all Lobs
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="userEmailId"></param>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        DataSet Sub_Summary_Agency(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Summary_BusinessOperations(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Summary_TotalLosses(string clientId, string userEmailId, string submissionId);

        DataSet Sub_Summary_Property_Exposure(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Summary_Property_Coverages(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Summary_Property_Losses(string clientId, string userEmailId, string submissionId);

        DataSet Sub_Summary_Auto_Exposure(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Summary_Auto_Coverages(string clientId, string userEmailId, string submissionId);
        DataSet Sub_Summary_Auto_Losses(string clientId, string userEmailId, string submissionId);
    }
}
