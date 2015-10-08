using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EDISAngular.Services;


namespace EDISTEST
{
    [TestClass]
    public class GoogleGeoTest
    {
        [TestMethod]
        public void CoordinateRetrievalTest()
        {
            var lat_australia = GoogleGeoService.GetCoordinatesLat("australia");
            var lng_australia = GoogleGeoService.GetCoordinatesLng("australia");

            Assert.AreEqual(-25.274398, lat_australia);
            Assert.AreEqual(133.775136, lng_australia);

        }
    }
}
