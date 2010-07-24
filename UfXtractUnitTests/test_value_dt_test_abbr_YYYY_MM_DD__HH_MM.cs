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
public class test_value_dt_test_abbr_YYYY_MM_DD__HH_MM
{
// http://microformats.org/wiki/value-dt-test-abbr-YYYY-MM-DD--HH-MM
// value-dt-test-abbr-YYYY-MM-DD--HH-MM
// The value-dt-test-abbr-YYYY-MM-DD--HH-MM test demonstrate the concatenation of abbr title attribute and the text from a span element to create one datetime value:
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://microformats.org/wiki/value-dt-test-abbr-YYYY-MM-DD--HH-MM#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2008-06-24T18:30").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "With the value class pattern the results should contain a time" );
}
 
}
}
