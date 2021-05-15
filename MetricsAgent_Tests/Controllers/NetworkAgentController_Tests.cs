using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class NetworkAgentController_Tests
    {
        private NetworkAgentController _controller;
        private Mock<INetworkMetricsRepository> _mock;
        private Mock<ILogger<NetworkAgentController>> _mockLogger;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<INetworkMetricsRepository>();
            _mockLogger = new Mock<ILogger<NetworkAgentController>>();
            _controller = new NetworkAgentController(_mock.Object, _mockLogger.Object);
        }

        [TestMethod()]
        public void Create_Test()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            var result = _controller.Create(new NetworkMetricCreateRequest { Time = DateTime.Now, Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
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