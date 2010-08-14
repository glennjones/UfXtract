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
public class test_hCard_4
{
// http://www.ufxtract.com/testsuite/hcard/hcard4.htm
// hCard 4 - n (name) singular and multiple occurrence test
// This page was design to test singular and multiple occurrence values in the n element of a hcard.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard4.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].n.honorific-prefix[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("honorific-prefix", 0).Value;
Assert.That(test, Is.EqualTo("Dr"), "The honorific-prefix is a optional multiple value" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("John"), "The given-name is a optional multiple value" );
}
 
 
[Test]
public void Test_03()
{
// vcard[0].n.additional-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("additional-name", 0).Value;
Assert.That(test, Is.EqualTo("Peter"), "The additional-name is a optional multiple value" );
}
 
 
[Test]
public void Test_04()
{
// vcard[0].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("Doe"), "The family-name is a optional multiple value" );
}
 
 
[Test]
public void Test_05()
{
// vcard[0].n.honorific-suffix[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("honorific-suffix", 1).Value;
Assert.That(test, Is.EqualTo("PHD"), "The honorific-suffix is a optional multiple value" );
}
 
}
}
