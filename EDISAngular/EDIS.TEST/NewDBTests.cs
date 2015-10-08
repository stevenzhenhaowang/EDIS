using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDISAngular.Infrastructure.DatabaseAccess;

namespace EDISTEST
{
    [TestClass]
    public class NewDBTests
    {
        [TestMethod]
        public void EnumCanProduceItems()
        {
            CommonReferenceDataRepository repo = new CommonReferenceDataRepository(null);

            var professionTypes = repo.GetAllProfessionTypes();
            Assert.AreEqual(7, professionTypes.Count);
        }

        [TestMethod]
        public void CanGenerateFilteredCommissionsAndFees()
        {
            CommonReferenceDataRepository repo = new CommonReferenceDataRepository(null);
            var fees = repo.GetCommissionAndFees_Filtered(c => c.CommissionsAndFeesId == 1);
            Assert.AreEqual(1, fees.Count);
        }


    }
}
