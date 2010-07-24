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
public class test_hCard_99
{
// http://www.ufxtract.com/testsuite/hcard/hcard99.htm
// hCard 99 - implied n optimization test
// This page was design to test the implied "n" optimization which is explained on the wiki http://microformats.org/wiki/hcard#Implied_.22n.22_Optimization. Examples copied from the orginal test suite.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard99.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("Ryan"), "The given-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("King"), "The family-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_03()
{
// vcard[1].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 1).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("Ryan"), "The given-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_04()
{
// vcard[1].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 1).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("King"), "The family-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_05()
{
// vcard[2].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 2).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("Ryan"), "The given-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_06()
{
// vcard[2].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 2).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("King"), "The family-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_07()
{
// vcard[3].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 3).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("Brian"), "The given-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_08()
{
// vcard[3].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 3).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("Suda"), "The family-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_09()
{
// vcard[4].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 4).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("Ryan"), "The given-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_10()
{
// vcard[4].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 4).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("King"), "The family-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_11()
{
// vcard[5].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 5).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("R"), "The given-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_12()
{
// vcard[5].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 5).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("King"), "The family-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_13()
{
// vcard[6].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 6).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("R"), "The given-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_14()
{
// vcard[6].n.family-name[0]
string test = nodes.GetNameByPosition("vcard", 6).Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("King"), "The family-name value is implied from the fn value" );
}
 
 
[Test]
public void Test_15()
{
// vcard[7].n.given-name
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 7).Nodes["n"].Nodes["given-name"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "The given-name property should be missing" );
}
 
 
[Test]
public void Test_16()
{
// vcard[7].n.family-name
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 7).Nodes["n"].Nodes["family-name"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "The family-name property should be missing" );
}
 
 
[Test]
public void Test_17()
{
// vcard[8].n.given-name
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 8).Nodes["n"].Nodes["given-name"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "The given-name property should be missing" );
}
 
 
[Test]
public void Test_18()
{
// vcard[8].n.family-name
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 8).Nodes["n"].Nodes["family-name"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "The family-name property should be missing" );
}
 
}
}
