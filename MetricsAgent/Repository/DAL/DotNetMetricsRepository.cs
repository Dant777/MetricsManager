using System.Data.SQLite;
using System;
using System.Linq;
using Dapper;
using System.Collections.Generic;
using MetricsAgent.Repository.DAL.Helpers;

namespace MetricsAgent.Repository.DAL
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private ISqlSettings _sqlSettings;
        public DotNetMetricsRepository(ISqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
      
        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {

                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }
        }


        public IList<DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {

                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics").ToList();
            }

        }

        public DotNetMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.QuerySingle<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE id=@id",
                    new { id = id });
            }

        }

    }
}
