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
public class test_hCard_18
{
// http://www.ufxtract.com/testsuite/hcard/hcard18.htm
// hCard 18 - table header include test
// This page was design to test the table header include pattern.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard18.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].url[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("url", 0).Value;
Assert.That(test, Is.EqualTo("http://example.org/"), "The url is added using the header include pattern" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("example.org"), "The organization-name is added using the header include pattern" );
}
 
 
[Test]
public void Test_03()
{
// vcard[1].url[0]
string test = nodes.GetNameByPosition("vcard", 1).Nodes.GetNameByPosition("url", 0).Value;
Assert.That(test, Is.EqualTo("http://example.org/"), "The url is added using the header include pattern" );
}
 
 
[Test]
public void Test_04()
{
// vcard[1].org[0].organization-name
string test = nodes.GetNameByPosition("vcard", 1).Nodes.GetNameByPosition("org", 0).Nodes["organization-name"].Value;
Assert.That(test, Is.EqualTo("example.org"), "The organization-name is added using the header include pattern" );
}
 
}
}
