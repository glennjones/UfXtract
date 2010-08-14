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
public class test_hCard_12
{
// http://www.ufxtract.com/testsuite/hcard/hcard12.htm
// hCard 12 - abbr (abbreviation) element test
// This page was design to comprehensively test the use of the abbreviation element.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard12.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].fn
string test = nodes.GetNameByPosition("vcard", 0).Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("John Doe"), "Should take the value from the abbr title attribute" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].n.honorific-prefix[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("honorific-prefix", 0).Value;
Assert.That(test, Is.EqualTo("Mister"), "Should take the value from the abbr title attribute" );
}
 
 
[Test]
public void Test_03()
{
// vcard[0].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("Jonathan"), "Should take the value from the abbr title attribute" );
}
 
 
[Test]
public void Test_04()
{
// vcard[0].n.additional-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("additional-name", 0).Value;
Assert.That(test, Is.EqualTo("John"), "Should take the value from the abbr title attribute" );
}
 
 
[Test]
public void Test_05()
{
// vcard[0].nickname
string test = nodes.GetNameByPosition("vcard", 0).Nodes["nickname"].Value;
Assert.That(test, Is.EqualTo("JJ"), "Should take the value from the abbr title attribute" );
}
 
 
[Test]
public void Test_06()
{
// vcard[0].adr[0].street-address[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("street-address", 0).Value;
Assert.That(test, Is.EqualTo("123 Fake Street"), "Should take the value from the abbr title attribute" );
}
 
 
[Test]
public void Test_07()
{
// vcard[0].adr[0].type[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("type", 0).Value;
Assert.That(test, Is.EqualTo("WORK").IgnoreCase, "Should take the value from the abbr title attribute" );
}
 
 
[Test]
public void Test_08()
{
// vcard[0].tel[0].value
}
 
}
}
