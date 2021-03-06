// <copyright file="CrmDalTest.cs">Copyright ©  2018</copyright>
using System;
using CRM.DAL;
using Common;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CRM.DAL.Tests
{
    /// <summary>This class contains parameterized unit tests for CrmDal</summary>
    [PexClass(typeof(CrmDal))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CrmDalTest
    {
        /// <summary>Test stub for GetPackage(Int32)</summary>
        [PexMethod]
        public void GetPackageTest([PexAssumeUnderTest]CrmDal target, int lineId)
        {
            Package result = target.GetPackage(lineId);
            Assert.IsNotNull(result);
            //return result;
            // TODO: add assertions to method CrmDalTest.GetPackageTest(CrmDal, Int32)
        }

       
    }
}
