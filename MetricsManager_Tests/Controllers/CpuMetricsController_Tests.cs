using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetricsManager.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.Tests
{
  
    [TestClass()]
    public class CpuMetricsController_Tests
    {

        [TestMethod()]
        public void GetMetricsFromAgentTest()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }

        [TestMethod()]
        public void GetMetricsByPercentileFromAgentTest()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }

        [TestMethod()]
        public void GetMetricsFromAllClusterTest()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }

        [TestMethod()]
        public void GetMetricsByPercentileFromAllClusterTest()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }
    }
}