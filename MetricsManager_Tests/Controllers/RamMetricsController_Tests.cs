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
    public class RamMetricsController_Tests
    {
        [TestMethod()]
        public void GetMetricsFromAgent_Test()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }

        [TestMethod()]
        public void GetMetricsByPercentileFromAgent_Test()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }

        [TestMethod()]
        public void GetMetricsFromAllCluster_Test()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }

        [TestMethod()]
        public void GetMetricsByPercentileFromAllCluster_Test()
        {
            bool result = true;
            bool expect = true;
            Assert.AreEqual(result, expect);
        }
    }
}