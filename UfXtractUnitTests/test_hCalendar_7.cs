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
public class test_hCalendar_7
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar7.htm
// hCalendar 7 - ISO date format test - RFC 3339 profile
// This page was design to partial test the use of the RFC 3339 profile. It extends the tests on page 10. The IsEqualToISODate method normalisation and compares dates.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar7.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("200801").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO standard date format" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].dtstart
string test = nodes.GetNameByPosition("vevent", 1).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20080121").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO extended date format" );
}
 
 
[Test]
public void Test_03()
{
// vevent[2].dtstart
string test = nodes.GetNameByPosition("vevent", 2).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T1130").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO standard date format" );
}
 
 
[Test]
public void Test_04()
{
// vevent[3].dtstart
string test = nodes.GetNameByPosition("vevent", 3).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T113015").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO standard date format" );
}
 
 
[Test]
public void Test_05()
{
// vevent[4].dtstart
string test = nodes.GetNameByPosition("vevent", 4).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T113015Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - uppercase punctuation" );
}
 
 
[Test]
public void Test_06()
{
// vevent[5].dtstart
string test = nodes.GetNameByPosition("vevent", 5).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501t113015z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - lowercase punctuation" );
}
 
 
[Test]
public void Test_07()
{
// vevent[6].dtstart
string test = nodes.GetNameByPosition("vevent", 6).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T113025").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - mixed punctuation" );
}
 
 
[Test]
public void Test_08()
{
// vevent[7].dtstart
string test = nodes.GetNameByPosition("vevent", 7).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T11:30:25").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - mixed punctuation" );
}
 
}
}
