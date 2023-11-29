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
        public DataSet DashboardGraph(DashboardFilter dashboardFilter,string Type)
        {
            return BaseDatabase.GetData("call SP_SubCountByIndustries('2', '1075', 'Jhon@gmail.com', '01/01/2023', '11/27/2023');");
        }
    }
}
