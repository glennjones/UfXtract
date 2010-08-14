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
public class test_hCalendar_4
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar4.htm
// hCalendar 4 - extracting rrule repeated event test
// This page test that parsers can extract rrule from different structures.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar4.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].rrule.freq
string test = nodes.GetNameByPosition("vevent", 0).Nodes["rrule"].Nodes["freq"].Value;
Assert.That(test, Is.EqualTo("yearly"), "The rrule.freq value" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].rrule.freq
string test = nodes.GetNameByPosition("vevent", 1).Nodes["rrule"].Nodes["freq"].Value;
Assert.That(test, Is.EqualTo("weekly"), "The rrule.freq value" );
}
 
 
[Test]
public void Test_03()
{
// vevent[1].rrule.byday
string test = nodes.GetNameByPosition("vevent", 1).Nodes["rrule"].Nodes["byday"].Value;
Assert.That(test, Is.EqualTo("mo,tu,we,th,fr"), "The rrule.byday value" );
}
 
 
[Test]
public void Test_04()
{
// vevent[1].rrule.byhour
string test = nodes.GetNameByPosition("vevent", 1).Nodes["rrule"].Nodes["byhour"].Value;
Assert.That(test, Is.EqualTo("17"), "The rrule.byhour value" );
}
 
 
[Test]
public void Test_05()
{
// vevent[1].rrule.byminute
string test = nodes.GetNameByPosition("vevent", 1).Nodes["rrule"].Nodes["byminute"].Value;
Assert.That(test, Is.EqualTo("30"), "The rrule.byminute value" );
}
 
 
[Test]
public void Test_06()
{
// vevent[1].tzid
string test = nodes.GetNameByPosition("vevent", 1).Nodes["tzid"].Value;
Assert.That(test, Is.EqualTo("US-Eastern"), "The tzid value" );
}
 
}
}
