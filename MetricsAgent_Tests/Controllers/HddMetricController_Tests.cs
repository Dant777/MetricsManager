﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetricsAgent.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace MetricsAgent.Controllers.Tests
{
    [TestClass()]
    public class HddMetricController_Tests
    {
        private HddMetricsController _controller;
        private Mock<IHddMetricsRepository> _mock;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<IHddMetricsRepository>();
            _controller = new HddMetricsController(_mock.Object);
        }

        [TestMethod()]
        public void Create_ShouldCall_Create_From_Repository()
        {
            _mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            var result = _controller.Create(new HddMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
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