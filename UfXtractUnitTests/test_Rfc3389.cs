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
    public class test_RFC3389
    {


        [Test]
        public void Test_RFC3389()
        {

            RFC3389toDotNet( "2007-05-01", new DateTime(2007,5,1));
            RFC3389toDotNet( "2007-05-01T11:30:00", new DateTime(2007,5,1,11,30,0));

            RFC3389toISO("2007", "2007");
            RFC3389toISO("2007-05", "2007-05");
            RFC3389toISO("2007-05-01", "2007-05-01");
            RFC3389toISO("2007-05-01T11", "2007-05-01T11");
            RFC3389toISO("2007-05-01T11:30", "2007-05-01T11:30");
            RFC3389toISO("2007-05-01T11:30:45", "2007-05-01T11:30:45");
            RFC3389toISO("2007-05-01T11:30:45Z", "2007-05-01T11:30:45Z");
            RFC3389toISO("2007-05-01T11:30:45-10", "2007-05-01T11:30:45-10");
            RFC3389toISO("2007-05-01T11:30:45+10", "2007-05-01T11:30:45+10");
            RFC3389toISO("2007-05-01T11:30:45+10:30", "2007-05-01T11:30:45+10:30");
            RFC3389toISO("2007-05-01T11:30:45.0133", "2007-05-01T11:30:45.0133");

            RFC3389toISO("2007-05-01t11:30", "2007-05-01T11:30");
            RFC3389toISO("20070501T1130", "2007-05-01T11:30");
            RFC3389toISO("20070501T11:30", "2007-05-01T11:30");

        }

        public void RFC3389toDotNet( string input, DateTime date )
        {
            Rfc3389DateTime dateTime = new Rfc3389DateTime(input);
            DateTime rfc = dateTime.ToDateTime();
            Assert.That(rfc, Is.EqualTo(date), "Format - " + input );
        }

        public void RFC3389toISO(string input, string iso)
        {
            Rfc3389DateTime dateTime = new Rfc3389DateTime(input);
            Assert.That(dateTime.ToString(), Is.EqualTo(iso), "Format - " + input);
        }



    }
}
