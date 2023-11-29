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
            //return BaseDatabase.GetData("call SP_SubCountByLOB('1075', 'Jhon@gmail.com', '01/01/2023', '11/28/2023');");
            //return BaseDatabase.GetData("call SP_SubCountByBroker('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            //return BaseDatabase.GetData("call SP_SubCountByCity('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            //return BaseDatabase.GetData("call SP_SubCountByState('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            return BaseDatabase.GetData("call SP_SubCountByIndustries('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
            //return BaseDatabase.GetData("select 1", parameters);
        }
        public DataSet DashboardGraph_CountByLOB(int TopNumber,string clientId,string UserEmailId,DateTime startDate,DateTime EndDate)
        {
            return BaseDatabase.GetData("call SP_SubCountByLOB('1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
        }
        public DataSet DashboardGraph_CountByByBroker(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            return BaseDatabase.GetData("call SP_SubCountByByBroker('2','1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
        }
        public DataSet DashboardGraph_CountByCity(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            return BaseDatabase.GetData("call SP_SubCountByCity('2','1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
        }
        public DataSet DashboardGraph_CountByState(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            return BaseDatabase.GetData("call SP_SubCountByState('2','1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
        }
        public DataSet DashboardGraph_CountByIndustries(int TopNumber, string clientId, string UserEmailId, DateTime startDate, DateTime EndDate)
        {
            return BaseDatabase.GetData("call SP_SubCountByIndustries('2','1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
        }
    }
}
