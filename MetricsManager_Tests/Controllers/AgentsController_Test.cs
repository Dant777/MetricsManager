using System;
using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetricsManager_Tests
{
    [TestClass]
    public class AgentsController_Test
    {
        AgentsController _agentsController;
        AgentInfo _agentInfo;

        [TestInitialize]
        public void TestInitialize()
        {
            _agentsController = new AgentsController();
            _agentInfo = new AgentInfo();
        }

        [TestMethod]
        public void RegisterAgent_ReturnOk()
        {
            
            var result = _agentsController.RegisterAgent(_agentInfo);
            Assert.AreEqual<IActionResult>(result, result);
        }
        [TestMethod]
        public void EnableAgentById_ReturnOk()
        {

            var result = _agentsController.EnableAgentById(1);
            Assert.AreEqual<IActionResult>(result, result);
        }
        [TestMethod]
        public void DisableAgentById_ReturnOk()
        {

            var result = _agentsController.DisableAgentById(1);
            Assert.AreEqual<IActionResult>(result, result);
        }
        [TestMethod]
        public void GetRegisterAgents_ReturnOk()
        {

            var result = _agentsController.GetRegisterAgents();
            Assert.AreEqual<IActionResult>(result, result);
        }
    }
}
