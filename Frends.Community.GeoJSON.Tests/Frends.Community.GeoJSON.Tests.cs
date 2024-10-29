using NUnit.Framework;

namespace Frends.Community.GeoJSON.Tests
{
    [TestFixture]
    class TestClass
    {
        /// <summary>
        /// lorem
        /// </summary>
        [Test]
        public void ThreeGeoJsons()
        {
            var input = new Parameters
            {
                Wkt2 = "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))"
            };

            var options = new Options
            {
                Amount = 3,
                Delimiter = ", "
            };

            var ret = GeoJson.WKTPolygonToGeoJSON(input, options, new System.Threading.CancellationToken());

            Assert.That(ret.Ret, Is.EqualTo(@"{""type"":""Feature"",""geometry"":{""type"":""Polygon"",""coordinates"":[[[30.0,10.0],[40.0,40.0],[20.0,40.0],[10.0,20.0],[30.0,10.0]]]},""properties"":{}}"));
        }
    }
}
