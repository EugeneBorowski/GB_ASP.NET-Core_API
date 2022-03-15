using System;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTests
{
    public class AgentRamMetricsControllerUnitTests
    {
        private AgentRamMetricsController controller;

        public AgentRamMetricsControllerUnitTests()
        {
            controller = new AgentRamMetricsController();
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange

            //Act
            var result = controller.GetRamMetrics();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
