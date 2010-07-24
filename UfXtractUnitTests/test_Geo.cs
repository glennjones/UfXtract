using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using UfXtract;
using UfXtract.Utilities;

namespace UfXtract.UnitTests
{
    //Should work against all these formats
    //+23.70000;+90.30000
    //-23.70000;-90.30000
    //23.70000;90.30000
    //23.7;90.3

    [TestFixture]
    public class test_Geo
    {


        [Test]
        public void Test_Geo()
        {

            TestGeoFormat("+23.70000;+90.30000", "23.7");
            TestGeoFormat("-23.70000;-90.30000", "-23.7");
            TestGeoFormat("23.70000;90.30000", "23.7");
            TestGeoFormat("23.7;90.3", "23.7");

            TestLatitude("+23.70000", "23.7");
            TestLatitude("-23.70000", "-23.7");
            TestLatitude("23.70000", "23.7");
            TestLatitude("23.7", "23.7");
 
        }



        public void TestGeoFormat(string geo, string latitude)
        {
            Geo geo1 = new Geo();
            geo1.Parse(geo);
            Assert.That(geo1.Latitude, Is.EqualTo(latitude), "Format - " + geo);
        }

        public void TestLatitude(string lat1, string lat2)
        {
            Geo geo1 = new Geo();
            geo1.Parse(lat1, "0");
            Geo geo2 = new Geo();
            geo2.Parse(lat2, "0");
            Assert.That(geo1.Latitude, Is.EqualTo(geo2.Latitude), "Format - " + lat1 + " - " + lat2);
        }



    }
}
