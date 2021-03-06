using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface IRepository<T> where T : class 
    {
        IList<T> GetByTimePeriod(DateTime fromTime, DateTime toTime);

        IList<T> GetAll();

        void Create(T item);
    }
}
