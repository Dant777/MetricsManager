using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class DotNetAgentController_Tests
    {
        private DotNetAgentController _controller;
        private Mock<IDotNetMetricsRepository> _mock;
        private Mock<ILogger<DotNetAgentController>> _mockLogger;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<IDotNetMetricsRepository>();
            _mockLogger = new Mock<ILogger<DotNetAgentController>>();
            _controller = new DotNetAgentController(_mock.Object, _mockLogger.Object);
        }

        [TestMethod()]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            var result = _controller.Create(new DotNetMetricCreateRequest { Time = DateTime.Now, Value = 50 });
            _mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
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