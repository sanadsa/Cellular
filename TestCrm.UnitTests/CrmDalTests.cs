using Common;
using CRM.BL;
using CRM.Common.Interfaces;
using CRM.DAL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebAPIService.Controllers.Api;

namespace TestCrm.UnitTests
{

    [TestClass]
    public class CrmDalTestss
    {
        [TestMethod]
        public void AddNewLine_LineAvailable_ReturnLine()
        {
            // ARRANGE
            var client = new ServiceAgent("sirage", "123");
            var mockCrmRepository = new Mock<ICrmRepository>();
            mockCrmRepository.Setup(x => x.AddServiceAgent(It.IsAny<ServiceAgent>()))
                .Returns(client);
            var crmBl = new CrmDalBl(mockCrmRepository.Object);
            // ACT
            var result = crmBl.AddAgent(client);
            // ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual("sirage", result.AgentName);
        }
    }
}
