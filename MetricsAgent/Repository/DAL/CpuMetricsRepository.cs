
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using MetricsAgent.Repository.DAL.Helpers;

namespace MetricsAgent.Repository.DAL
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private ISqlSettings _sqlSettings;
        public CpuMetricsRepository(ISqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
         
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToString()
                    });
            }
        }


        public IList<CpuMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }

        }

        public CpuMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.QuerySingle<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE id=@id",
                    new { id = id });
            }

        }

    }
}
