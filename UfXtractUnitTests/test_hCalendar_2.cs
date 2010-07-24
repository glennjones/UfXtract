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
public class test_hCalendar_2
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar2.htm
// hCalendar 2 - multiple occurrence test
// This page was design to test that values of a hCalendar which are meant to have multiple occurrences are parsed correctly.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar2.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].category[0].tag
string test = nodes.GetNameByPosition("vevent", 0).Nodes.GetNameByPosition("category", 0).Nodes["tag"].Value;
Assert.That(test, Is.EqualTo("Barcamp").IgnoreCase, "The category is a multiple value" );
}
 
 
[Test]
public void Test_02()
{
// vevent[0].category[1].tag
string test = nodes.GetNameByPosition("vevent", 0).Nodes.GetNameByPosition("category", 1).Nodes["tag"].Value;
Assert.That(test, Is.EqualTo("Unconference").IgnoreCase, "The category is a multiple value" );
}
 
 
[Test]
public void Test_03()
{
// vevent[0].attendee[0]
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 0).Nodes.GetNameByPosition("attendee", 0).Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The attendee is a singular value" );
}
 
}
}
