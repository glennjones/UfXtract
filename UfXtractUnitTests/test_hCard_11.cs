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
public class test_hCard_11
{
// http://www.ufxtract.com/testsuite/hcard/hcard11.htm
// hCard 11 - img (image) element test
// This page was design to test the use of the image element.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard11.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].fn
string test = nodes.GetNameByPosition("vcard", 0).Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("John Doe"), "The fn value should be taken from the alt attribute on a img element" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 1).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("John"), "The given-name value should implied from the alt attribute" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 2).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("Doe"), "The family-name value should implied from the alt attribute" );
}
 
}
}
