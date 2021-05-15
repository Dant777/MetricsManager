using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface ISqlSettings
    {
        string GetConnestionString();
    }
}
