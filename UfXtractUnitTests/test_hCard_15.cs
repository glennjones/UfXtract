using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using UfXtract;
using UfXtract.Utilities;
 
namespace UfXtract.UnitTests.hCard
{
 
[TestFixture]
public class test_hCard_15
{
// http://www.ufxtract.com/testsuite/hcard/hcard15.htm
// hCard 15 - ISO date format test - W3C Note DateTime profile
// This page was design to comprehensively test the W3C Note DateTime profile. The IsEqualToISODate method normalisation and compares dates.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard15.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].rev
string test = nodes.GetNameByPosition("vcard", 0).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].rev
string test = nodes.GetNameByPosition("vcard", 1).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year and month" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].rev
string test = nodes.GetNameByPosition("vcard", 2).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month and day" );
}
 
 
[Test]
public void Test_04()
{
// vcard[3].rev
string test = nodes.GetNameByPosition("vcard", 3).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time" );
}
 
 
[Test]
public void Test_05()
{
// vcard[4].rev
string test = nodes.GetNameByPosition("vcard", 4).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - UTC Year, month, day and time" );
}
 
 
[Test]
public void Test_06()
{
// vcard[5].rev
string test = nodes.GetNameByPosition("vcard", 5).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - UTC Year, month, day and time" );
}
 
 
[Test]
public void Test_07()
{
// vcard[6].rev
string test = nodes.GetNameByPosition("vcard", 6).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time with time zone offset" );
}
 
 
[Test]
public void Test_08()
{
// vcard[7].rev
string test = nodes.GetNameByPosition("vcard", 7).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time with time zone offset" );
}
 
 
[Test]
public void Test_09()
{
// vcard[8].rev
string test = nodes.GetNameByPosition("vcard", 8).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00.0150").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - Year, month, day and time with decimal fraction of a second" );
}
 
}
}
