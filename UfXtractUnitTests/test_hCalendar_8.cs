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
public class test_hCalendar_8
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar8.htm
// hCalendar 8 - extracting geo singular and paired values test
// This page was design to comprehensively test the geo format. Most of these tests are based on Mike Kaply work for the Firefox.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar8.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].geo.latitude
string test = nodes.GetNameByPosition("vevent", 0).Nodes["geo"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("37.77"), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].geo.latitude
string test = nodes.GetNameByPosition("vevent", 1).Nodes["geo"].Nodes["latitude"].Value;
Assert.That(test, Is.EqualTo("37.77"), "Should extract latitude value from paired value" );
}
 
}
}
