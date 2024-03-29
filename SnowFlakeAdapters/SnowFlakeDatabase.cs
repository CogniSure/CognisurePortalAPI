﻿using Models.DTO;
using SnowFlakeAdapter.Interface;
using System.Data;

namespace SnowFlakeAdapter
{
    public class SnowFlakeDatabase: ISnowFlakeDatabase
    {
        public ISnowFlakeBaseDatabase BaseDatabase { get; }

        public SnowFlakeDatabase(ISnowFlakeBaseDatabase baseDatabase) : base()
        {
            BaseDatabase = baseDatabase;
        }

        public DataSet Sub_Submission_Files(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_DocumentVault", parameters);
        }
        public DataSet DashboardGraph_CountByTurnaroundTime(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubTAT", parameters);
        }
        public DataSet DashboardGraph_CountByLOB(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if(startDate!= DateTime.MinValue)
            parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByLOB", parameters);
        }
        public DataSet DashboardGraph_CountByByBroker(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByBroker", parameters);
        }
        public DataSet DashboardGraph_CountByCity(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByCity", parameters);
        }
        public DataSet DashboardGraph_CountByState(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByState", parameters);
        }
        public DataSet DashboardGraph_CountByIndustries(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByIndustries", parameters);
        }
        public DataSet DashboardGraph_CountOfSubmissionId(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_CountOfSubID", parameters);
        }
        public DataSet DashboardGraph_CountOfDocType(int TopNumber, string clientId, string UserEmailId, string startDate, string EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            //if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            //if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_CountOfDocType", parameters);
        }

        public DataSet Sub_Exposure_TIV(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", userEmailId));

            return BaseDatabase.GetData("call SP_SubTiv", parameters);
        }

        public DataSet Sub_Exposure_LocationsCount(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SubCountOfLocations", parameters);
        }

        public DataSet Sub_Exposure_BuildingsCount(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));
            
            return BaseDatabase.GetData("call SP_SubCountOfBuildings", parameters);
        }

        public DataSet Sub_Exposure_ConstructionType(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SubCountOfConstructionType", parameters);
        }

        public DataSet Sub_Exposure_OccupancyType(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SubCountOfOccupanyType", parameters);
        }

        public DataSet Sub_Exposure_YearBuild(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));
            
            return BaseDatabase.GetData("call SP_SubBuildingAge", parameters);
        }

        public DataSet Sub_Exposure_ProtectionClass(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));
            
            return BaseDatabase.GetData("call SP_SubCountOfProtectionClassCode", parameters);
        }

        public DataSet Sub_Loss_ClaimByLobByYear(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_ClaimsByLOBByYear", parameters);
        }

        public DataSet Sub_Loss_IncurredByLobByYear(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_IncurredByLOBByYear", parameters);
        }

        public DataSet Sub_Loss_IncurredRangeCount(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_IncurredRange", parameters);
        }

        public DataSet Sub_Loss_ClaimByClaimTypeByYear(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_ClaimsByClaimTypeByYear", parameters);
        }

        public DataSet Sub_Loss_IncurredByClaimTypeByYear(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_IncurredByClaimTypeByYear", parameters);
        }

        public DataSet Sub_Loss_ClaimByClaimType(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_ClaimsByClaimType", parameters);
        }

        public DataSet Sub_Loss_ClaimStatus(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_PercentageOfClaimStatus", parameters);
        }

        public DataSet Sub_Loss_TotalIncurred(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_TotalIncurred", parameters);
        }

        public DataSet Sub_Loss_TopLocations(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SubCountOfProtectionClassCode", parameters);
        }

        public DataSet GetSubmissionHeader(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SummaryHeader", parameters);
        }

        public DataSet Sub_Summary_Agency(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SummaryAgency", parameters);
        }

        public DataSet Sub_Summary_BusinessOperations(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SummaryBusinessOperations", parameters);
        }

        public DataSet Sub_Summary_TotalLosses(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_SummaryTotalLossess", parameters);
        }

        public DataSet Sub_Summary_Property_Exposure(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_PropertyExposure", parameters);
        }

        public DataSet Sub_Summary_Property_Coverages(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_PropertyCoverage", parameters);
        }

        public DataSet Sub_Summary_Property_Losses(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_PropertyTotalLossess", parameters);
        }

        public DataSet Sub_Summary_Auto_Exposure(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_AutomobileExposure", parameters);
        }

        public DataSet Sub_Summary_Auto_Coverages(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_AutoVehicles", parameters);
        }

        public DataSet Sub_Summary_Auto_Losses(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_AutoTotalLossess", parameters);
        }

        public DataSet Sub_Summary_WC_Exposure(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_WorkersCompensationExposure", parameters);
        }

        public DataSet Sub_Summary_WC_PayRollEmployee(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_WorkersCompensationPayrollandEmployeeCount", parameters);
        }

        public DataSet Sub_Summary_WC_Losses(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_WorkersCompensationTotalLossess", parameters);
        }

        public DataSet Sub_Summary_GL_ScheduleOfHazards(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_GeneralLiabilityScheduleOfHazards", parameters);
        }

        public DataSet Sub_Summary_GL_Coverage(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_GENERALLIABILITYCOVERAGE", parameters);
        }

        public DataSet Sub_Summary_GL_Losses(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_GeneralLiabilityTotalLossess", parameters);
        }

        public DataSet Sub_Summary_Umbrella_Exposure(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_UmbrellaExposure", parameters);
        }

        public DataSet Sub_Summary_Umbrella_Coverages(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_UmbrellaUnderlyingCovCarrierName", parameters);
        }

        public DataSet Sub_Summary_Umbrella_Losses(string clientId, string userEmailId, string submissionId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("SUBMISSIONGUID", submissionId));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID", userEmailId));

            return BaseDatabase.GetData("call SP_UmbrellaTotalLossess", parameters);
        }
    }
}
