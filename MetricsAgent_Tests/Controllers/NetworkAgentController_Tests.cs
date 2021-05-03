using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetricsAgent.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class NetworkAgentController_Tests
    {
        [TestMethod()]
        public void GetMetricsFromAgent_Test()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }
    }
}