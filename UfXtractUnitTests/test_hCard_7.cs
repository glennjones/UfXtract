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
public class test_hCard_7
{
// http://www.ufxtract.com/testsuite/hcard/hcard7.htm
// hCard 7 - extracting tel (telephone) number test
// This page test that parsers can extract telephone numbers from different structures. This includes type property and value excerpting test. The IsEqualToPhoneNumber method canonicalises and compares phone numbers.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard7.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].tel[0].value
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("tel", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("01273 700100"), "Should collect the telephone number from the node text" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].tel[0].value
}
 
 
[Test]
public void Test_03()
{
// vcard[2].tel[0].type[12]
string test = nodes.GetNameByPosition("vcard", 2).Nodes.GetNameByPosition("tel", 0).Nodes.GetNameByPosition("type", 12).Value;
Assert.That(test, Is.EqualTo("PCS").IgnoreCase, "Should find the type value. Types are case insensitive" );
}
 
 
[Test]
public void Test_04()
{
// vcard[3].tel[0].type[1]
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 3).Nodes.GetNameByPosition("tel", 0).Nodes.GetNameByPosition("type", 1).Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "The second type value \"next-door\" is incorrect" );
}
 
 
[Test]
public void Test_05()
{
// vcard[4].tel[0].value
}
 
}
}
