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
public class test_hResume_2
{
// http://www.ufxtract.com/testsuite/hresume/hresume2.htm
// hResume 2 - multiple occurrence test
// This page was design to test that values of a hResume which are can have multiple occurrences are parsed correctly.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hresume/hresume2.htm#uf";
webRequest.Load(url, UfFormats.HResume());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// hresume[0].affiliation[1].org[0].organization-name
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("affiliation", 1).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("BritPack"), "The affiliation is a multiple occurrence value" );
}
 
 
[Test]
public void Test_02()
{
// hresume[0].education[0].location
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes["location"].Value;
Assert.That(test, Is.EqualTo("Brighton University"), "The education is a multiple occurrence value" );
}
 
 
[Test]
public void Test_03()
{
// hresume[0].education[0].summary
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("education", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("BA Graphic Design"), "The education is a multiple occurrence value" );
}
 
 
[Test]
public void Test_04()
{
// hresume[0].experience[0].location
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("experience", 0).Nodes["location"].Value;
Assert.That(test, Is.EqualTo("Brighton"), "The experience is a multiple occurrence value" );
}
 
 
[Test]
public void Test_05()
{
// hresume[0].experience[0].summary
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("experience", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("Web Design Ltd"), "The experience is a multiple occurrence value" );
}
 
 
[Test]
public void Test_06()
{
// hresume[0].publication[1]
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("publication", 1).Value;
Assert.That(test, Is.EqualTo("http://2.example.com"), "The publication is a multiple occurrence value" );
}
 
 
[Test]
public void Test_07()
{
// hresume[0].skill[3].tag
string test = nodes.GetNameByPosition("hresume", 0).Nodes.GetNameByPosition("skill", 3).Nodes["tag"].Value;
Assert.That(test, Is.EqualTo("C++").IgnoreCase, "The skill is a multiple occurrence value" );
}
 
}
}
