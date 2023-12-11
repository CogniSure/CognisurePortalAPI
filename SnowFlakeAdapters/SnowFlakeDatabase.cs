using Models.DTO;
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
        public DataSet SampleTest()
        {

            //return BaseDatabase.GetData("call sp_gettestdata();");
            return BaseDatabase.GetData("call SP_SubCountByLOB('1075', 'Jhon@gmail.com', '01/01/2023', '11/28/2023');");
            //return BaseDatabase.GetData("call SP_SubCountByBroker('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            //return BaseDatabase.GetData("call SP_SubCountByCity('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            //return BaseDatabase.GetData("call SP_SubCountByState('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            //return BaseDatabase.GetData("call SP_SubCountByIndustries('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            //return BaseDatabase.GetData("select 1", parameters);
        }
        public DataSet DashboardGraph_CountByTurnaroundTime(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByLOB", parameters);
        }
        public DataSet DashboardGraph_CountByLOB(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            if(startDate!= DateTime.MinValue)
            parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByLOB", parameters);
        }
        public DataSet DashboardGraph_CountByByBroker(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByBroker", parameters);
        }
        public DataSet DashboardGraph_CountByCity(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByCity", parameters);
        }
        public DataSet DashboardGraph_CountByState(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByState", parameters);
        }
        public DataSet DashboardGraph_CountByIndustries(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add(BaseDatabase.Param("TopN", TopNumber));
            parameters.Add(BaseDatabase.Param("CLIENTID", clientId));
            parameters.Add(BaseDatabase.Param("USERID ", UserEmailId));
            if (startDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_STARTDATE ", startDate));
            if (EndDate != DateTime.MinValue)
                parameters.Add(BaseDatabase.Param("ADDEDON_ENDDATE ", EndDate));
            return BaseDatabase.GetData("call SP_SubCountByIndustries", parameters);
        }
    }
}
