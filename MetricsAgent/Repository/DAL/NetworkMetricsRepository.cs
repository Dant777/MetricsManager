using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using MetricsAgent.Repository.DAL.Helpers;

namespace MetricsAgent.Repository.DAL
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private ISqlSettings _sqlSettings;
        public NetworkMetricsRepository(ISqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

      
        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {

                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToString()
                    });
            }
        }


        public IList<NetworkMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {

                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics").ToList();
            }

        }

        public NetworkMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.QuerySingle<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE id=@id",
                    new { id = id });
            }

        }

    }
}
