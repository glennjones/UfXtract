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
public class test_value_dt_test_YYYY_MM_DD__abbr_HH_MMpm
{
// http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--abbr-HH-MMpm
// value-dt-test-YYYY-MM-DD--abbr-HH-MMpm
// The value-dt-test-YYYY-MM-DD--abbr-HH-MMpm test demonstrate the concatenation of two html elements to create one datetime value. The time element contains text demarking the use of 12 hour clock i.e. "pm". The date for dtend is implied from the dtstart date:
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--abbr-HH-MMpm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2009-06-26T19:30").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "With the value class pattern the results should contain a time" );
}
 
 
[Test]
public void Test_02()
{
// vevent[0].dtend
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtend"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2009-06-26T22:15").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "With the value class pattern the results should contain a time" );
}
 
}
}
