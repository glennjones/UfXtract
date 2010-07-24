//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;
using UfXtract;

namespace UfXtractUnitTestBuilder
{
    class Program
    {
        static void Main(string[] args)
        {

            TestFixtureLoader loader = new TestFixtureLoader();

            loader.ParseTestSuite("http://www.ufxtract.com/testsuite/hcard/");
            loader.ParseTestSuite("http://www.ufxtract.com/testsuite/hcalendar/");
            loader.ParseTestSuite("http://www.ufxtract.com/testsuite/hresume/");

            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HH-MM");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-abbr-YYYY-MM-DD--HH-MM");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-abbr-YYYY-MM-DD-abbr-HH-MM");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HHpm");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--Hpm-EEpm");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--abbr-HH-MMpm");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--12am-12pm");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--H-MMam-Epm");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--0Ham-EEam");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--H-MM-SSpm-EE-NN-UUpm");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-DDD--HH-MM-SS");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HH-MMZ-EE-NN-UUZ");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HH-MM-XX-YY--EE-NN-UU--XXYY");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HH-MM-XX--EE-NN-UU--Y");
            loader.LoadTestFixture("http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HH-MM-SS-XXYY--EE-NN--Z");

        }





    }
}
