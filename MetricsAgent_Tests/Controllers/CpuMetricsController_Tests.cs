using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetricsAgent.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class CpuMetricsController_Tests
    {
        private CpuMetricsController _controller;
        private Mock<ICpuMetricsRepository> _mock;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void TestInitialize()
        {

            _mock = new Mock<ICpuMetricsRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CpuMetricsController(_mock.Object, _mockMapper.Object);
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