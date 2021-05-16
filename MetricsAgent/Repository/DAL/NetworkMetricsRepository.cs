﻿using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using MetricsAgent.Repository.DAL.Helpers;
using System;

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

        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics")
                    .Where(x =>
                            fromTime.DateTime <= DateTimeOffset.FromUnixTimeSeconds(x.Time).DateTime
                            && DateTimeOffset.FromUnixTimeSeconds(x.Time).DateTime <= toTime.DateTime)
                    .ToList();
            }
        }
    }
}
