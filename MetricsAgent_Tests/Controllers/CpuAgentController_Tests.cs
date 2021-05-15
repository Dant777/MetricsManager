using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class CpuAgentController_Tests
    {
        private CpuAgentController _controller;
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<ILogger<CpuAgentController>> _mockLogger;
        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<ICpuMetricsRepository>();
            _mockLogger = new Mock<ILogger<CpuAgentController>>();
            _controller = new CpuAgentController(_mock.Object, _mockLogger.Object);
        }

        [TestMethod()]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            var result = _controller.Create(new CpuMetricCreateRequest { Time = DateTime.Now, Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [TestMethod()]
        public void GetAll_ShouldCall_GetAll_From_Repository()
        {
            _mock.Setup(repository => repository.GetAll());
            var result = _controller.GetAll();
            _mock.Verify(repository => repository.GetAll(), Times.AtLeastOnce());
        }
    }
}