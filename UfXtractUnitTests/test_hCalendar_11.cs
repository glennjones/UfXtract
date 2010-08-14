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
public class test_hCalendar_11
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar11.htm
// hCalendar 11 - extracting contact test
// This page was design to test the extracting organizer and attendee information from hCalendar. Mainly that these properties are parsed as hCards.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar11.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].contact.n.given-name[0]
string test = nodes.GetNameByPosition("vevent", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("John"), "The contact given-name value" );
}
 
 
[Test]
public void Test_02()
{
// vevent[0].contact.n.family-name[0]
string test = nodes.GetNameByPosition("vevent", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("Doe"), "The contact family-name value" );
}
 
 
[Test]
public void Test_03()
{
// vevent[0].attendee[1].fn
string test = nodes.GetNameByPosition("vevent", 0).Nodes.GetNameByPosition("attendee", 1).Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("Jane Doe"), "The attendee fn value" );
}
 
}
}
