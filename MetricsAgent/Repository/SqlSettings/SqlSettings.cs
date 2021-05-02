using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class SqlSettings : ISqlSettings
    {
        private const string _connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public string GetConnestionString()
        {
            return _connectionString;
        }
    }
}
