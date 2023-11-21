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
            //return BaseDatabase.GetData("call sp_gettestdata();", parameters);
            return BaseDatabase.GetData("call PC_BIZ.PUBLIC.sp_gettestdata('abc');");
            //return BaseDatabase.GetData("select 1", parameters);
        }
    }
}
