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
public class test_hCalendar_6
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar6.htm
// hCalendar 6 - ISO date format test - W3C Note DateTime profile
// This page was design to comprehensively test the W3C Note DateTime profile. The IsEqualToISODate method normalisation and compares dates.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar6.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].dtstart
string test = nodes.GetNameByPosition("vevent", 1).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year and month" );
}
 
 
[Test]
public void Test_03()
{
// vevent[2].dtstart
string test = nodes.GetNameByPosition("vevent", 2).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month and day" );
}
 
 
[Test]
public void Test_04()
{
// vevent[3].dtstart
string test = nodes.GetNameByPosition("vevent", 3).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time" );
}
 
 
[Test]
public void Test_05()
{
// vevent[4].dtstart
string test = nodes.GetNameByPosition("vevent", 4).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - UTC Year, month, day and time" );
}
 
 
[Test]
public void Test_06()
{
// vevent[5].dtstart
string test = nodes.GetNameByPosition("vevent", 5).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - UTC Year, month, day and time" );
}
 
 
[Test]
public void Test_07()
{
// vevent[6].dtstart
string test = nodes.GetNameByPosition("vevent", 6).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time with time zone offset" );
}
 
 
[Test]
public void Test_08()
{
// vevent[7].dtstart
string test = nodes.GetNameByPosition("vevent", 7).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time with time zone offset" );
}
 
 
[Test]
public void Test_09()
{
// vevent[8].dtstart
string test = nodes.GetNameByPosition("vevent", 8).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00.0150").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time with decimal fraction of a second" );
}
 
}
}
