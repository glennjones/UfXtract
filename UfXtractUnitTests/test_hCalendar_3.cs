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
public class test_hCalendar_3
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar3.htm
// hCalendar 3 - ISO duration format test
// This page was design to comprehensively test the ISO duration format. More information on duration representations in microformats
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar3.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].duration
string test = nodes.GetNameByPosition("vevent", 0).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P9M"), "The duration value" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].duration
string test = nodes.GetNameByPosition("vevent", 1).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P1Y2M"), "The duration value" );
}
 
 
[Test]
public void Test_03()
{
// vevent[2].duration
string test = nodes.GetNameByPosition("vevent", 2).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P1Y2M10D"), "The duration value" );
}
 
 
[Test]
public void Test_04()
{
// vevent[3].duration
string test = nodes.GetNameByPosition("vevent", 3).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P1Y2M10DT20H"), "The duration value" );
}
 
 
[Test]
public void Test_05()
{
// vevent[4].duration
string test = nodes.GetNameByPosition("vevent", 4).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P1Y2M10DT20H30M"), "The duration value" );
}
 
 
[Test]
public void Test_06()
{
// vevent[5].duration
string test = nodes.GetNameByPosition("vevent", 5).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P1Y2M10DT20H30M30S"), "The duration value" );
}
 
 
[Test]
public void Test_07()
{
// vevent[6].duration
string test = nodes.GetNameByPosition("vevent", 6).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P1Y2M10DT20H30M30.5S"), "The duration value" );
}
 
 
[Test]
public void Test_08()
{
// vevent[7].duration
string test = nodes.GetNameByPosition("vevent", 7).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P1Y2M10DT20.5H"), "The duration value" );
}
 
 
[Test]
public void Test_09()
{
// vevent[8].duration
string test = nodes.GetNameByPosition("vevent", 8).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P110D"), "The duration value" );
}
 
 
[Test]
public void Test_10()
{
// vevent[9].duration
string test = nodes.GetNameByPosition("vevent", 9).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("PT30M"), "The duration value" );
}
 
 
[Test]
public void Test_11()
{
// vevent[10].duration
string test = nodes.GetNameByPosition("vevent", 10).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P0001-02-10"), "The duration value" );
}
 
 
[Test]
public void Test_12()
{
// vevent[11].duration
string test = nodes.GetNameByPosition("vevent", 11).Nodes["duration"].Value;
Assert.That(test, Is.EqualTo("P0001-02-10T14:30:30"), "The duration value" );
}
 
}
}
