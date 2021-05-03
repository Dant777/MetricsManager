using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MetricsAgent.Repository.DAL.Helpers
{
    public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object value)
        {
            TimeSpan second = TimeSpan.FromSeconds((long)value);
            return second;
        }


        public override void SetValue(IDbDataParameter parameter, TimeSpan value) 
        {
            parameter.Value = value;
            
        }
            

    }
}
