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
public class test_hResume_6
{
// http://www.ufxtract.com/testsuite/hresume/hresume6.htm
// hResume 6 - simple contact
// This page was design to test a simple contact structure
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hresume/hresume6.htm#uf";
webRequest.Load(url, UfFormats.HResume());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// hresume[0].contact.fn
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("Dr John Peter Doe MSc, PHD"), "Should have honorific prefixs and suffixs" );
}
 
 
[Test]
public void Test_02()
{
// hresume[0].contact.n.honorific-prefix[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("honorific-prefix", 0).Value;
Assert.That(test, Is.EqualTo("Dr"), "The honorific-prefix is a optional multiple value" );
}
 
 
[Test]
public void Test_03()
{
// hresume[0].contact.n.given-name[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("John"), "The given-name is a optional multiple value" );
}
 
 
[Test]
public void Test_04()
{
// hresume[0].contact.n.additional-name[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("additional-name", 0).Value;
Assert.That(test, Is.EqualTo("Peter"), "The additional-name is a optional multiple value" );
}
 
 
[Test]
public void Test_05()
{
// hresume[0].contact.n.family-name[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("family-name", 0).Value;
Assert.That(test, Is.EqualTo("Doe"), "The family-name is a optional multiple value" );
}
 
 
[Test]
public void Test_06()
{
// hresume[0].contact.n.honorific-suffix[1]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["n"].Nodes.GetNameByPosition("honorific-suffix", 1).Value;
Assert.That(test, Is.EqualTo("PHD"), "The honorific-suffix is a optional multiple value" );
}
 
 
[Test]
public void Test_07()
{
// hresume[0].contact.email[0].value
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Should collect the email address from href attribute" );
}
 
 
[Test]
public void Test_08()
{
// hresume[0].contact.url[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("url", 0).Value;
Assert.That(test, Is.EqualTo("http://example.com/johndoe/"), "Should collect the URL from href attribute" );
}
 
 
[Test]
public void Test_09()
{
// hresume[0].summary
string test = nodes.GetNameByPosition("hresume", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("Interactive designer looking for a job"), "Should collect the inner text of the first element with a summary class" );
}
 
}
}
