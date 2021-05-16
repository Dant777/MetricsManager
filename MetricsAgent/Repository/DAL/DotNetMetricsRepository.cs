﻿using System.Data.SQLite;
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
                        time = item.Time.ToString()
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

        public IList<DotNetMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics")
                    .Where(x =>
                            fromTime.DateTime <= DateTimeOffset.FromUnixTimeSeconds(x.Time).DateTime
                            && DateTimeOffset.FromUnixTimeSeconds(x.Time).DateTime <= toTime.DateTime)
                    .ToList();
            }
        }
    }
}
