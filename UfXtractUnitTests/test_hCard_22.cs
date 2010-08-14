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
public class test_hCard_22
{
// http://www.ufxtract.com/testsuite/hcard/hcard22.htm
// hCard 22 - HTML5 time element
// This page was design to test the use of the HTML5 time element for microformat datetime structures such as birthdays
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard22.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].bday
string test = nodes.GetNameByPosition("vcard", 0).Nodes["bday"].Value;
Assert.That(test, Is.EqualTo("1992-05-14"), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].bday
string test = nodes.GetNameByPosition("vcard", 1).Nodes["bday"].Value;
Assert.That(test, Is.EqualTo("1992-05-14"), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].bday
string test = nodes.GetNameByPosition("vcard", 2).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_04()
{
// vcard[3].bday
string test = nodes.GetNameByPosition("vcard", 3).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_05()
{
// vcard[4].bday
string test = nodes.GetNameByPosition("vcard", 4).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_06()
{
// vcard[5].bday
string test = nodes.GetNameByPosition("vcard", 5).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_07()
{
// vcard[6].bday
string test = nodes.GetNameByPosition("vcard", 6).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_08()
{
// vcard[7].bday
string test = nodes.GetNameByPosition("vcard", 7).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00.0150").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_09()
{
// vcard[8].bday
string test = nodes.GetNameByPosition("vcard", 8).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2007-05-01T21:30:00.0150+08:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday or birthday from a HTML5 time element" );
}
 
 
[Test]
public void Test_10()
{
// vcard[9].bday
string test = nodes.GetNameByPosition("vcard", 9).Nodes["bday"].Value;
Assert.That(test, Is.EqualTo("08:00"), "The dtstart from a HTML5 time element" );
}
 
}
}
