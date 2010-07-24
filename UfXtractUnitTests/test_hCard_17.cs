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
public class test_hCard_17
{
// http://www.ufxtract.com/testsuite/hcard/hcard17.htm
// hCard 17 - implied organization-name optimization test
// This page was design to test the implied "org" optimization which is explained on the wiki http://microformats.org/wiki/hcard#Implied_.22organization-name.22_Optimization. Examples copied from the original test suite.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard17.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("World Wide Web Consortium"), "The organization-name value is implied from the org value" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 1).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("World Wide Web Consortium"), "The organization-name value is implied from the org value" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 2).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("World Wide Web Consortium"), "The organization-name value is implied from the org value" );
}
 
 
[Test]
public void Test_04()
{
// vcard[3].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 3).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("World Wide Web Consortium"), "The organization-name value is implied from the org value" );
}
 
 
[Test]
public void Test_05()
{
// vcard[4].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 4).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("World Wide Web Consortium"), "The organization-name value is implied from the org value" );
}
 
 
[Test]
public void Test_06()
{
// vcard[5].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 5).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("World Wide Web Consortium"), "The organization-name value" );
}
 
 
[Test]
public void Test_07()
{
// vcard[6].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 6).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("World Wide Web Consortium"), "The organization-name value" );
}
 
}
}
