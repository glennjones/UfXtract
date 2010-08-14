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
public class test_hCalendar_14
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar14.htm
// hCalendar 14 - HTML5 time element
// This page was design to test the use of the HTML5 time element for microformat datetime structures such as dtstrat and dtend
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar14.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2008-08-18").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].dtend
string test = nodes.GetNameByPosition("vevent", 1).Nodes["dtend"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2008-08-19").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtend from a HTML5 time element" );
}
 
 
[Test]
public void Test_03()
{
// vevent[2].dtstart
string test = nodes.GetNameByPosition("vevent", 2).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2008-08-18T15:00:00-01").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_04()
{
// vevent[2].dtend
string test = nodes.GetNameByPosition("vevent", 2).Nodes["dtend"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2008-08-18T16:00:00-01").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtend from a HTML5 time element" );
}
 
 
[Test]
public void Test_05()
{
// vevent[3].dtstart
string test = nodes.GetNameByPosition("vevent", 3).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_06()
{
// vevent[4].dtstart
string test = nodes.GetNameByPosition("vevent", 4).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_07()
{
// vevent[5].dtstart
string test = nodes.GetNameByPosition("vevent", 5).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_08()
{
// vevent[6].dtstart
string test = nodes.GetNameByPosition("vevent", 6).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_09()
{
// vevent[7].dtstart
string test = nodes.GetNameByPosition("vevent", 7).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_10()
{
// vevent[8].dtstart
string test = nodes.GetNameByPosition("vevent", 8).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_11()
{
// vevent[9].dtstart
string test = nodes.GetNameByPosition("vevent", 9).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00.0150").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_12()
{
// vevent[10].dtstart
string test = nodes.GetNameByPosition("vevent", 10).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00.0150+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart from a HTML5 time element" );
}
 
 
[Test]
public void Test_13()
{
// vevent[11].dtstart
string test = nodes.GetNameByPosition("vevent", 11).Nodes["dtstart"].Value;
Assert.That(test, Is.EqualTo("08:00"), "The dtstart from a HTML5 time element" );
}
 
}
}
