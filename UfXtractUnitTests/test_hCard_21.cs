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
public class test_hCard_21
{
// http://www.ufxtract.com/testsuite/hcard/hcard21.htm
// hCard 21 - opaque/embedded hCards
// This page was design to test that embedded hCards are opaque to the parent hCard. This test was originally created by Toby Inkster.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard21.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].fn
string test = nodes.GetNameByPosition("vcard", 0).Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("Queen Elizabeth II"), "The fomatted name of the contain hcard" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].agent[0].fn
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("agent", 0).Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("Michael Jeffery"), "The fomatted name of the agent" );
}
 
 
[Test]
public void Test_03()
{
// vcard[0].agent[0].agent[0].fn
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("agent", 0).Nodes.GetNameByPosition("agent", 0).Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("Malcolm Hazell"), "The fomatted name of the agents agent" );
}
 
 
[Test]
public void Test_04()
{
// vcard[0].agent[0].agent[0].role
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("agent", 0).Nodes.GetNameByPosition("agent", 0).Nodes["role"].Value;
Assert.That(test, Is.EqualTo("secretary"), "The role of the agents agent" );
}
 
 
[Test]
public void Test_05()
{
// vcard[0].agent[0].role
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("agent", 0).Nodes["role"].Value;
Assert.That(test, Is.EqualTo("representative in Australia"), "The role of the agent" );
}
 
 
[Test]
public void Test_06()
{
// vcard[0].role
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 0).Nodes["role"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "The hCard should have no role only the agents" );
}
 
}
}
