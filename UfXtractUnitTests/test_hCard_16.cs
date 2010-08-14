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
public class test_hCard_16
{
// http://www.ufxtract.com/testsuite/hcard/hcard16.htm
// hCard 16 - ISO date format test - RFC 3339 profile
// This page was design to partial test the use of the RFC 3339 profile. It extends the tests on page 10. The IsEqualToISODate method normalisation and compares dates.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard16.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].rev
string test = nodes.GetNameByPosition("vcard", 0).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("200801").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO standard date format" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].rev
string test = nodes.GetNameByPosition("vcard", 1).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20080121").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO extended date format" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].rev
string test = nodes.GetNameByPosition("vcard", 2).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T1130").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO standard date format" );
}
 
 
[Test]
public void Test_04()
{
// vcard[3].rev
string test = nodes.GetNameByPosition("vcard", 3).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T113015").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - ISO standard date format" );
}
 
 
[Test]
public void Test_05()
{
// vcard[4].rev
string test = nodes.GetNameByPosition("vcard", 4).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T113015Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - uppercase punctuation" );
}
 
 
[Test]
public void Test_06()
{
// vcard[5].rev
string test = nodes.GetNameByPosition("vcard", 5).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501t113025z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - lowercase punctuation" );
}
 
 
[Test]
public void Test_07()
{
// vcard[6].rev
string test = nodes.GetNameByPosition("vcard", 6).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T113025").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - mixed punctuation" );
}
 
 
[Test]
public void Test_08()
{
// vcard[7].rev
string test = nodes.GetNameByPosition("vcard", 7).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("20070501T11:30:25").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find a date from text node - mixed punctuation" );
}
 
}
}
