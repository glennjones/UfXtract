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
    //P9MP1Y2M
    //P1Y2M10D
    //P1Y2M10DT20H
    //P1Y2M10DT20H30M
    //P1Y2M10DT20H30M30S
    //P1Y2M10DT20H30M30.5S
    //P1Y2M10DT20.5H
    //P110D
    //PT30M
    //P0001-02-10
    //P0001-02-10T14:30:30

    [TestFixture]
    public class test_ISODurations
    {


        [Test]
        public void Test_ISODurations()
        {
            Test_ISODurationConvertion("P0001", "P1Y");
            Test_ISODurationConvertion("P0001-02", "P1Y2M");
            Test_ISODurationConvertion("P0001-02-10", "P1Y2M10D");
            Test_ISODurationConvertion("P0001-02-10T19", "P1Y2M10DT19H");
            Test_ISODurationConvertion("P0001-02-10T19:30", "P1Y2M10DT19H30M");
            Test_ISODurationConvertion("P0001-02-10T19:30:30", "P1Y2M10DT19H30M30S");

            Test_ISODurationConvertion("P1Y", "P1Y");
            Test_ISODurationConvertion("P1Y2M", "P1Y2M");
            Test_ISODurationConvertion("P1Y2M10D", "P1Y2M10D");
            Test_ISODurationConvertion("P1Y2M10DT19H", "P1Y2M10DT19H");
            Test_ISODurationConvertion("P1Y2M10DT19H30M", "P1Y2M10DT19H30M");
            Test_ISODurationConvertion("P1Y2M10DT19H30M30S", "P1Y2M10DT19H30M30S");
            Test_ISODurationConvertion("P110D", "P110D");
            Test_ISODurationConvertion("PT30M", "PT30M");

            Test_ISODurationConvertion("P1Y2M10DT19.5H", "P1Y2M10DT19.5H");
            Test_ISODurationConvertion("P1Y2M10DT19H30M30.0145S", "P1Y2M10DT19H30M30.0145S");

        }


        public void Test_ISODurationConvertion(string input, string iso)
        {
            ISODuration isoDuration = new ISODuration();
            isoDuration.Parse(input);
            Assert.That(isoDuration.ToString(), Is.EqualTo(iso), "Format - " + input);
        }

        [Test]
        public void Test_ISODurationsAddToDate()
        {
            ISODuration isoDuration = new ISODuration();
            isoDuration.Parse("P1Y2M10D");
            DateTime dt1 = isoDuration.AddToDate(new DateTime(2000, 1, 1));
            DateTime dt2 = dt1;
            dt2.AddYears(1);
            dt2.AddMonths(2);
            dt2.AddDays(10);

            Assert.That(dt1, Is.EqualTo(dt2));
        }

    }
}
