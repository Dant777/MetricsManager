﻿using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using MetricsAgent.Repository.DAL.Helpers;
using System;

namespace MetricsAgent.Repository.DAL
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private ISqlSettings _sqlSettings;
        public HddMetricsRepository(ISqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {

                connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.ToString()
                    });
            }
        }


        public IList<HddMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {

                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics").ToList();
            }

        }

        public HddMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.QuerySingle<HddMetric>("SELECT Id, Time, Value FROM hddmetrics WHERE id=@id",
                    new { id = id });
            }

        }

        public IList<HddMetric> GetByTimePeriod(DateTime fromTime, DateTime toTime)
        {
            using (var connection = new SQLiteConnection(_sqlSettings.GetConnestionString()))
            {
                return connection.Query<HddMetric>("SELECT Id, Time, Value FROM hddmetrics")
                    .Where(x => fromTime <= x.Time && x.Time <= toTime)
                    .ToList();
            }
        }
    }
}
