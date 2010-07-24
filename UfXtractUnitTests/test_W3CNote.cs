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
    public class test_W3CNote
    {


        [Test]
        public void Test_W3CNote()
        {

            W3CNotetoDotNet( "2007-05-01", new DateTime(2007,5,1));
            W3CNotetoDotNet( "2007-05-01T11:30:00", new DateTime(2007,5,1,11,30,0));

            W3CNotetoISO("2007", "2007");
            W3CNotetoISO("2007-05", "2007-05");
            W3CNotetoISO("2007-05-01", "2007-05-01");
            W3CNotetoISO("2007-05-01T11", "2007-05-01T11");
            W3CNotetoISO("2007-05-01T11:30", "2007-05-01T11:30");
            W3CNotetoISO("2007-05-01T11:30:45", "2007-05-01T11:30:45");
            W3CNotetoISO("2007-05-01T11:30:45Z", "2007-05-01T11:30:45Z");
            W3CNotetoISO("2007-05-01T11:30:45-10", "2007-05-01T11:30:45-10");
            W3CNotetoISO("2007-05-01T11:30:45+10", "2007-05-01T11:30:45+10");
            W3CNotetoISO("2007-05-01T11:30:45+10:30", "2007-05-01T11:30:45+10:30");
            W3CNotetoISO("2007-05-01T11:30:45.0133", "2007-05-01T11:30:45.0133");

        }

        public void W3CNotetoDotNet( string input, DateTime date )
        {
            W3CNoteDateTime dateTime = new W3CNoteDateTime(input);
            DateTime rfc = dateTime.ToDateTime();
            Assert.That(rfc, Is.EqualTo(date), "Format - " + input );
        }

        public void W3CNotetoISO(string input, string iso)
        {
            W3CNoteDateTime dateTime = new W3CNoteDateTime(input);
            Assert.That(dateTime.ToString(), Is.EqualTo(iso), "Format - " + input);
        }



    }
}
