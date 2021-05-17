
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using MetricsAgent.Repository.DAL.Helpers;
using System;

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

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
         
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.Query<CpuMetric>("SELECT Id, Time, Value FROM cpumetrics")
                    .Where(x => 
                            fromTime.DateTime <= DateTimeOffset.FromUnixTimeSeconds(x.Time).DateTime  
                            && DateTimeOffset.FromUnixTimeSeconds(x.Time).DateTime <= toTime.DateTime)
                    .ToList();
            }

        }
    }
}
