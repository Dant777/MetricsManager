using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class HddAgentController_Tests
    {
        private HddAgentController _controller;
        private Mock<IHddMetricsRepository> _mock;
        private Mock<ILogger<HddAgentController>> _mockLogger;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<IHddMetricsRepository>();
            _mockLogger = new Mock<ILogger<HddAgentController>>();
            _controller = new HddAgentController(_mock.Object, _mockLogger.Object);
        }

        [TestMethod()]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            var result = _controller.Create(new HddMetricCreateRequest { Time = DateTime.Now, Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
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