using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using UfXtract;
using UfXtract.Utilities;
 
namespace UfXtract.UnitTests.hResume
{
 
[TestFixture]
public class test_hResume_3
{
// http://www.ufxtract.com/testsuite/hresume/hresume3.htm
// hResume 3 - extracting contact test
// This page was design to test the extracting contact information from hResume. The test includes those elements of hCard most likely to be used as contact information in a CV.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hresume/hresume3.htm#uf";
webRequest.Load(url, UfFormats.HResume());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// hresume[0].contact.n.honorific-prefix[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("honorific-prefix", 0).Value;
Assert.That(test, Is.EqualTo("Dr"), "The honorific-prefix is a optional multiple value" );
}
 
 
[Test]
public void Test_02()
{
// hresume[0].contact.n.given-name[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("John"), "The given-name is a optional multiple value" );
}
 
 
[Test]
public void Test_03()
{
// hresume[0].contact.n.additional-name[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("additional-name", 0).Value;
Assert.That(test, Is.EqualTo("Peter"), "The additional-name is a optional multiple value" );
}
 
 
[Test]
public void Test_04()
{
// hresume[0].contact.n.family-name[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("Doe"), "The family-name is a optional multiple value" );
}
 
 
[Test]
public void Test_05()
{
// hresume[0].contact.n.honorific-suffix[1]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("honorific-suffix", 1).Value;
Assert.That(test, Is.EqualTo("PHD"), "The honorific-suffix is a optional multiple value" );
}
 
 
[Test]
public void Test_06()
{
// hresume[0].contact.adr[0].type[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("type", 0).Value;
Assert.That(test, Is.EqualTo("home").IgnoreCase, "The type is a optional multiple value. Types are case insensitive" );
}
 
 
[Test]
public void Test_07()
{
// hresume[0].contact.adr[0].post-office-box
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes["post-office-box"].Value;
Assert.That(test, Is.EqualTo("PO Box 46"), "The post-office-box is a optional singular value" );
}
 
 
[Test]
public void Test_08()
{
// hresume[0].contact.adr[0].street-address[1]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("street-address", 1).Value;
Assert.That(test, Is.EqualTo("West Street"), "The street-address is a optional multiple value" );
}
 
 
[Test]
public void Test_09()
{
// hresume[0].contact.adr[0].extended-address
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes["extended-address"].Value;
Assert.That(test, Is.EqualTo("Flat 2"), "The extended-address is a optional singular value" );
}
 
 
[Test]
public void Test_10()
{
// hresume[0].contact.adr[0].region
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes["region"].Value;
Assert.That(test, Is.EqualTo("East Sussex"), "The region is a optional singular value" );
}
 
 
[Test]
public void Test_11()
{
// hresume[0].contact.adr[0].locality
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes["locality"].Value;
Assert.That(test, Is.EqualTo("Brighton"), "The locality is a optional singular value" );
}
 
 
[Test]
public void Test_12()
{
// hresume[0].contact.adr[0].postal-code
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes["postal-code"].Value;
Assert.That(test, Is.EqualTo("BN1 3DF"), "The postal-code is a optional singular value" );
}
 
 
[Test]
public void Test_13()
{
// hresume[0].contact.adr[0].country-name
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("adr", 0).Nodes["country-name"].Value;
Assert.That(test, Is.EqualTo("United Kingdom"), "The country-name is a optional singular value" );
}
 
 
[Test]
public void Test_14()
{
// hresume[0].contact.email[0].value
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Should collect the email address from href attribute" );
}
 
 
[Test]
public void Test_15()
{
// hresume[0].contact.tel[0].value
}
 
 
[Test]
public void Test_16()
{
// hresume[0].contact.url[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("url", 0).Value;
Assert.That(test, Is.EqualTo("http://www.example.com/"), "Should collect the url value" );
}
 
}
}
