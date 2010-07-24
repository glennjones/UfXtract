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
public class test_hCalendar_1
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar1.htm
// hCalendar 1 - single occurrence test
// This page was design to test that values of a hCalendar which are meant to have only a single occurrence are parsed correctly. The IsEqualToISODate method uses date normalisation and compare methods.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar1.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].summary
string test = nodes.GetNameByPosition("vevent", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("Barcamp Brighton 1"), "The summary is a singular value" );
}
 
 
[Test]
public void Test_02()
{
// vevent[0].duration
string test = nodes.GetNameByPosition("vevent", 0).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P2D"), "The duration is a singular value" );
}
 
 
[Test]
public void Test_03()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-09-08").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart is a singular value" );
}
 
 
[Test]
public void Test_04()
{
// vevent[0].dtend
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtend"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-09-09").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtend is a singular value" );
}
 
 
[Test]
public void Test_05()
{
// vevent[0].location
string test = nodes.GetNameByPosition("vevent", 0).Nodes["location"].Value;
Assert.That(test, Is.EqualTo("Madgex Office, Brighton"), "The location is a singular value" );
}
 
 
[Test]
public void Test_06()
{
// vevent[0].description
string test = nodes.GetNameByPosition("vevent", 0).Nodes["description"].Value;
Assert.That(test, Is.EqualTo("Barcamp is an ad-hoc gathering born from the desire to share and learn in an open environment."), "The description is a singular value" );
}
 
 
[Test]
public void Test_07()
{
// vevent[0].url
string test = nodes.GetNameByPosition("vevent", 0).Nodes["url"].Value;
Assert.That(test, Is.EqualTo("http://www.barcampbrighton.org/"), "The url is a singular value" );
}
 
 
[Test]
public void Test_08()
{
// vevent[0].class
string test = nodes.GetNameByPosition("vevent", 0).Nodes["class"].Value;
Assert.That(test, Is.EqualTo("Public").IgnoreCase, "The class is a singular value" );
}
 
 
[Test]
public void Test_09()
{
// vevent[0].dtstamp
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstamp"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstamp is a singular value" );
}
 
 
[Test]
public void Test_10()
{
// vevent[0].last-modified
string test = nodes.GetNameByPosition("vevent", 0).Nodes["last-modified"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-02").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The last-modified is a singular value" );
}
 
 
[Test]
public void Test_11()
{
// vevent[0].uid
string test = nodes.GetNameByPosition("vevent", 0).Nodes["uid"].Value;
Assert.That(test, Is.EqualTo("guid1.example.com"), "The uid is a singular value" );
}
 
 
[Test]
public void Test_12()
{
// vevent[0].status
string test = nodes.GetNameByPosition("vevent", 0).Nodes["status"].Value;
Assert.That(test, Is.EqualTo("Confirmed").IgnoreCase, "The status is a singular value" );
}
 
 
[Test]
public void Test_13()
{
// vevent[0].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 0).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The geo is a singular value" );
}
 
 
[Test]
public void Test_14()
{
// vevent[0].contact
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 0).Nodes["contact"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The contact is a singular value" );
}
 
 
[Test]
public void Test_15()
{
// vevent[0].organizer
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 0).Nodes["organizer"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The organizer is a singular value" );
}
 
}
}
