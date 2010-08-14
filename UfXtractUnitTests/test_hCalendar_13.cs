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
public class test_hCalendar_13
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar13.htm
// hCalendar 13 - different location structures
// This page was design to test the different type of possilbe location structures within hCalandar. A location can be either a string, hCard, adr or geo.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar13.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].location
string test = nodes.GetNameByPosition("vevent", 0).Nodes["location"].Value;
Assert.That(test, Is.EqualTo("Brighton"), "The location property is a single string" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].location.locality
string test = nodes.GetNameByPosition("vevent", 1).Nodes["location"].Nodes["locality"].Value;
Assert.That(test, Is.EqualTo("Brighton"), "The location property is an adr" );
}
 
 
[Test]
public void Test_03()
{
// vevent[2].location.adr[0].locality
string test = nodes.GetNameByPosition("vevent", 2).Nodes["location"].Nodes.GetNameByPosition("adr", 0).Nodes["locality"].Value;
Assert.That(test, Is.EqualTo("Brighton"), "The location property is an hcard" );
}
 
 
[Test]
public void Test_04()
{
// vevent[3].location.latitude
string test = nodes.GetNameByPosition("vevent", 3).Nodes["location"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("37.77"), "The location property is an geo" );
}
 
}
}
