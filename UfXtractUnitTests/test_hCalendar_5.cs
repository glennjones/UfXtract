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
public class test_hCalendar_5
{
// http://www.ufxtract.com/testsuite/hcalendar/hcalendar5.htm
// hCalendar 5 - extracting rdate repeated event test
// This page test that parsers can extract rdate from different structures.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcalendar/hcalendar5.htm#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].rdate
string test = nodes.GetNameByPosition("vevent", 0).Nodes["rdate"].Value;
Assert.That(test, Is.EqualTo("2001-12-07, 2002-12-19"), "The rdate value" );
}
 
 
[Test]
public void Test_02()
{
// vevent[1].rdate
string test = nodes.GetNameByPosition("vevent", 1).Nodes["rdate"].Value;
Assert.That(test, Is.EqualTo("2001-06-01/2001-08-29, 2002-06-05/2002-08-30"), "The rdate value" );
}
 
}
}
