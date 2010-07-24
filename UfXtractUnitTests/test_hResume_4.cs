using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using UfXtract;
using UfXtract.Utilities;
 
namespace UfXtract.UnitTests.hResume
{
 
[TestFixture]
public class test_hResume_4
{
// http://www.ufxtract.com/testsuite/hresume/hresume4.htm
// hResume 4 - education hCalendar and hCard
// This page was design to test the use of both hCalendar and hCard properties to create an educational history item
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hresume/hresume4.htm#uf";
webRequest.Load(url, UfFormats.HResume());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// hresume[0].education[0].summary
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("BA (Hons) 3d Design"), "The summary value from hCalendar" );
}
 
 
[Test]
public void Test_02()
{
// hresume[0].education[0].dtstart
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("1989").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtstart value from hCalendar" );
}
 
 
[Test]
public void Test_03()
{
// hresume[0].education[0].dtend
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes["dtend"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("1992").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The dtend value from hCalendar" );
}
 
 
[Test]
public void Test_04()
{
// hresume[0].education[0].org[0].organization-name
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("University of Brighton"), "The org value from hCard" );
}
 
 
[Test]
public void Test_05()
{
// hresume[0].education[0].description
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes["description"].Value;
Assert.That(test, Is.EqualTo("A mixed art degree which used traditional craft skills, such as woodwork and blacksmithing to create works of art."), "The description value from hCalendar" );
}
 
 
[Test]
public void Test_06()
{
// hresume[0].education[0].adr[0].locality
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes.GetNameByPosition("adr", 0).Nodes["locality"].Value;
Assert.That(test, Is.EqualTo("Brighton"), "The locality value from hCard address" );
}
 
}
}
