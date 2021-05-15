using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class RamAgentController_Tests
    {
        private RamAgentController _controller;
        private Mock<IRamMetricsRepository> _mock;
        private Mock<ILogger<RamAgentController>> _mockLogger;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<IRamMetricsRepository>();
            _mockLogger = new Mock<ILogger<RamAgentController>>();
            _controller = new RamAgentController(_mock.Object, _mockLogger.Object);
        }

        [TestMethod()]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            var result = _controller.Create(new RamMetricCreateRequest { Time = DateTime.Now, Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
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