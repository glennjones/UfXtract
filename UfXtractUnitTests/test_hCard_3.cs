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
public class test_hCard_3
{
// http://www.ufxtract.com/testsuite/hcard/hcard3.htm
// hCard 3 - adr (address) singular and multiple occurrence test
// This page was design to test singular and multiple occurrence values in the adr element of a hcard.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard3.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].adr[0].type[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("type", 0).Value;
Assert.That(test, Is.EqualTo("work").IgnoreCase, "The type is a optional multiple value. Types are case insensitive" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].adr[0].post-office-box
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes["post-office-box"].Value;
Assert.That(test, Is.EqualTo("PO Box 46"), "The post-office-box is a optional singular value" );
}
 
 
[Test]
public void Test_03()
{
// vcard[0].adr[0].street-address[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("street-address", 1).Value;
Assert.That(test, Is.EqualTo("West Street"), "The street-address is a optional multiple value" );
}
 
 
[Test]
public void Test_04()
{
// vcard[0].adr[0].extended-address
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes["extended-address"].Value;
Assert.That(test, Is.EqualTo("Suite 2"), "The extended-address is a optional singular value" );
}
 
 
[Test]
public void Test_05()
{
// vcard[0].adr[0].region
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes["region"].Value;
Assert.That(test, Is.EqualTo("East Sussex"), "The region is a optional singular value" );
}
 
 
[Test]
public void Test_06()
{
// vcard[0].adr[0].locality
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes["locality"].Value;
Assert.That(test, Is.EqualTo("Brighton"), "The locality is a optional singular value" );
}
 
 
[Test]
public void Test_07()
{
// vcard[0].adr[0].postal-code
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes["postal-code"].Value;
Assert.That(test, Is.EqualTo("BN1 3DF"), "The postal-code is a optional singular value" );
}
 
 
[Test]
public void Test_08()
{
// vcard[0].adr[0].country-name
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 0).Nodes["country-name"].Value;
Assert.That(test, Is.EqualTo("United Kingdom"), "The country-name is a optional singular value" );
}
 
}
}
