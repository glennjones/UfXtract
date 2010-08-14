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
public class test_hCalendar_9
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar9.htm
// hCalendar 9 - geo comprehensive data format test
// This page was design to comprehensively test the geo format. Most of these tests are based on  Mike Kaply work for the Firefox.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar9.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].geo.latitude
string test = nodes.GetNameByPosition("vevent", 0).Nodes["geo"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("0"), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].geo.latitude
string test = nodes.GetNameByPosition("vevent", 1).Nodes["geo"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("0"), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_03()
{
// vevent[2].geo.latitude
string test = nodes.GetNameByPosition("vevent", 2).Nodes["geo"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("0"), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_04()
{
// vevent[3].geo.latitude
string test = nodes.GetNameByPosition("vevent", 3).Nodes["geo"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("23.7"), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_05()
{
// vevent[4].geo.latitude
string test = nodes.GetNameByPosition("vevent", 4).Nodes["geo"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("0"), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_06()
{
// vevent[5].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 5).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_07()
{
// vevent[6].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 6).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_08()
{
// vevent[7].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 7).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_09()
{
// vevent[8].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 8).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_10()
{
// vevent[9].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 9).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_11()
{
// vevent[10].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 10).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_12()
{
// vevent[11].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 11).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_13()
{
// vevent[12].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 12).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_14()
{
// vevent[13].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vevent", 13).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
}
}
