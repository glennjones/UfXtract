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
    //2007
    //2007-05
    //2007-05-01T11:30
    //2007-05-01T11:30Z
    //2007-05-01T11:30:00Z
    //2007-05-01T11:30+08:00
    //2007-05-01T11:30:00+08:00
    //2007-05-01T11:30:00.0135
    //200801
    //20080121
    //20070501T1130
    //20070501T113015
    //20070501T113015Z
    //20070501t113025z
    //2007-05-01T113025
    //20070501T11:30:25

    [TestFixture]
    public class test_ISODateTime
    {


        [Test]
        public void Test_ISODateTime()
        {


            ISOtoDotNet( "2007-05-01", new DateTime(2007,5,1));
            ISOtoDotNet( "2007-05-01T11:30:00", new DateTime(2007,5,1,11,30,0));

            IsotoISO("2007", "2007");
            IsotoISO("2007-05", "2007-05");
            IsotoISO("2007-05-01", "2007-05-01");
            IsotoISO("2007-05-01T11", "2007-05-01T11");
            IsotoISO("2007-05-01T11:30", "2007-05-01T11:30");
            IsotoISO("2007-05-01T11:30:45", "2007-05-01T11:30:45");
            IsotoISO("2007-05-01T11:30:45Z", "2007-05-01T11:30:45Z");
            IsotoISO("2007-05-01T11:30:45-10", "2007-05-01T11:30:45-10");
            IsotoISO("2007-05-01T11:30:45+10", "2007-05-01T11:30:45+10");
            IsotoISO("2007-05-01T11:30:45+10:30", "2007-05-01T11:30:45+10:30");
            IsotoISO("2007-05-01T11:30:45.0133", "2007-05-01T11:30:45.0133");

            IsotoISO("2007-05-01t11:30", "2007-05-01T11:30");
            IsotoISO("20070501T1130", "2007-05-01T11:30");
            IsotoISO("20070501T11:30", "2007-05-01T11:30");

        }

        public void ISOtoDotNet( string input, DateTime date )
        {
            Rfc3389DateTime dateTime = new Rfc3389DateTime(input);
            DateTime rfc = dateTime.ToDateTime();
            Assert.That(rfc, Is.EqualTo(date), "Format - " + input );
        }

        public void IsotoISO(string input, string iso)
        {
            Rfc3389DateTime dateTime = new Rfc3389DateTime(input);
            Assert.That(dateTime.ToString(), Is.EqualTo(iso), "Format - " + input);
        }



    }
}
