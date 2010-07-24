using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using UfXtract;
using UfXtract.Utilities;
 
namespace UfXtract.UnitTests.hCalendar
{
 
[TestFixture]
public class test_value_dt_test_abbr_YYYY_MM_DD_abbr_HH_MM
{
// http://microformats.org/wiki/value-dt-test-abbr-YYYY-MM-DD-abbr-HH-MM
// value-dt-test-abbr-YYYY-MM-DD-abbr-HH-MM
// The value-dt-test-abbr-YYYY-MM-DD-abbr-HH-MM test demonstrate the concatenation of two abbr title attributes to create one datetime value:
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://microformats.org/wiki/value-dt-test-abbr-YYYY-MM-DD-abbr-HH-MM#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2009-06-05T20:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "With the value class pattern the results should contain a date and time" );
}
 
}
}
