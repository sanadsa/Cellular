using Common;
using CRM.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCrm.UnitTests
{
    [TestClass]
    public class CrmDalTestss
    {
        [TestMethod]
        public void GetPackageTest(CrmDal target, int lineId)
        {
            Package result = target.GetPackage(lineId);
            Assert.IsNull(result);
        }
    }
}
