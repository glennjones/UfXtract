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
public class test_hCard_6
{
// http://www.ufxtract.com/testsuite/hcard/hcard6.htm
// hCard 6 - extracting email addresses test
// This page test that parsers can extract email addresses from differnt structures. This includes type property and value excerpting test.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard6.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].email[0].value
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Should collect the email address from href attribute" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].email[0].value
string test = nodes.GetNameByPosition("vcard", 1).Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Where a type is specified, but the value is not then the node text is the value" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].email[0].value
string test = nodes.GetNameByPosition("vcard", 2).Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Should collect the email address from the node text" );
}
 
 
[Test]
public void Test_04()
{
// vcard[3].email[0].type[0]
string test = nodes.GetNameByPosition("vcard", 3).Nodes.GetNameByPosition("email", 0).Nodes.GetNameByPosition("type", 0).Value;
Assert.That(test, Is.EqualTo("Internet").IgnoreCase, "Should find the type value. Types are case insensitive" );
}
 
 
[Test]
public void Test_05()
{
// vcard[4].email[0].type[3]
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 4).Nodes.GetNameByPosition("email", 0).Nodes.GetNameByPosition("type", 3).Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "The thrid type value \"lotus-notes\" is incorrect" );
}
 
 
[Test]
public void Test_06()
{
// vcard[5].email[0].value
string test = nodes.GetNameByPosition("vcard", 5).Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Should not conatin quertystring \"?subject=parser-test\"" );
}
 
 
[Test]
public void Test_07()
{
// vcard[6].email[0].value
string test = nodes.GetNameByPosition("vcard", 6).Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Where a type is specified, but the value is not then the node text is the value" );
}
 
}
}
