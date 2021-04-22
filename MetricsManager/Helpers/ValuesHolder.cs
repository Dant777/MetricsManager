using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class ValuesHolder
    {
        private List<string> _values;
        public ValuesHolder()
        {
            _values = new List<string>() { "1", "11", "2", "22", "3", "33" };
        }

        public List<string> Values { get => _values;  set { _values = value; } }
    }
}
