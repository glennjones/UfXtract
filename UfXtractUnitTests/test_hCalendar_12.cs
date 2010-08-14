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
public class test_hCalendar_12
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar12.htm
// hCalendar 12 - table header and axis include pattern
// This page was design to test the table header and axis include pattern for hCalendar. This test uses a real life example from Hackday London 07.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar12.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-06-16T09:00:00+00:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The included dtstart value" );
}
 
 
[Test]
public void Test_02()
{
// vevent[0].summary
string test = nodes.GetNameByPosition("vevent", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("Registration"), "The summary value" );
}
 
 
[Test]
public void Test_03()
{
// vevent[1].location
string test = nodes.GetNameByPosition("vevent", 1).Nodes["location"].Value;
Assert.That(test, Is.EqualTo("Main Stage (West Hall)"), "The included location value" );
}
 
 
[Test]
public void Test_04()
{
// vevent[1].summary
string test = nodes.GetNameByPosition("vevent", 1).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("Build a BBC News Search App in Under an Hour!"), "The summary value" );
}
 
}
}
