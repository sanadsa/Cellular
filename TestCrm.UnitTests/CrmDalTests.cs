using Common;
using CRM.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPIService.Controllers.Api;

namespace TestCrm.UnitTests
{

    [TestClass]
    public class CrmDalTestss
    {
        Mock<ICrmRepository> mockCrmRepository = new Mock<ICrmRepository>();
        CRMController controller;

        [TestMethod]
        public void CreateServiceAgent_LineAvailable_ReturnLine()
        {
            // ARRANGE
            var service = new ServiceAgent("sirage", "123");
            mockCrmRepository.Setup(x => x.AddServiceAgent(It.IsAny<ServiceAgent>()))
                .Returns(service);
            controller = new CRMController(mockCrmRepository.Object);
            // ACT
            var result = controller.CreateServiceAgent(service);
            // ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual("sirage", result.AgentName);
        }

        [TestMethod]
        public void CreateClient_ClientAvailable_ClientResponse()
        {
            // ARRANGE
            var client = new Client("sanad", "fff", 1231, 3, "das", "1231", 1);
            mockCrmRepository.Setup(x => x.AddClient(It.IsAny<Client>())).Returns(client);
            controller = new CRMController(mockCrmRepository.Object)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // ACT
            var response = controller.CreateClient(client);
            // ASSERT
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void CreateLine_LineAvailable_LineResponse()
        {
            // ARRANGE
            var line = new Line(1, "123", eStatus.available);
            mockCrmRepository.Setup(x => x.AddLine(It.IsAny<Line>())).Returns(line);
            controller = new CRMController(mockCrmRepository.Object)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // ACT
            var response = controller.CreateLine(line);
            // ASSERT
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetLine_GetLineById_LineResponse()
        {
            // ARRANGE
            var line = new Line(1, "123", eStatus.available);
            mockCrmRepository.Setup(x => x.GetLine(It.IsAny<int>()))
                .Returns(line);
            controller = new CRMController(mockCrmRepository.Object)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // ACT
            var response = controller.GetLine(1);
            // ASSERT
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetLines_GetClientLines_LinesResponse()
        {
            // ARRANGE
            var line = new Line(1, "123", eStatus.available);
            List<Line> lines = new List<Line>
            {
                new Line(123, "123", eStatus.available),
                new Line(123, "05426", eStatus.available),
            };
            var client = new Client
            {
                ClientName = "ddd",
                LastName = "sds",
                ClientID = 123
            };
            mockCrmRepository.Setup(x => x.AddClient(It.IsAny<Client>()))
                .Returns(client);
            mockCrmRepository.Setup(x => x.GetLines(It.IsAny<int>()))
                .Returns(lines);
            controller = new CRMController(mockCrmRepository.Object)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // ACT
            var response = controller.GetLines(123);
            // ASSERT
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.AreEqual(2, JsonConvert.DeserializeObject<List<Line>>(response.Content.ReadAsStringAsync().Result).Count);
        }
    }
}
