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
public class test_hResume_7
{
// http://www.ufxtract.com/testsuite/hresume/hresume7.htm
// hResume 7 - simple contact with limited properties
// This page was design to test a simple contact with only a fn and adr label structure
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hresume/hresume7.htm#uf";
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
// hresume[0].contact.label
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["label"].Value;
Assert.That(test, Is.EqualTo("Brighton, United Kingdom"), "The address label" );
}
 
 
[Test]
public void Test_03()
{
// hresume[0].contact.email[0].value
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("email", 0).Nodes["value"].Value;
Assert.That(test, Is.EqualTo("john@example.com"), "Should collect the email address from href attribute" );
}
 
 
[Test]
public void Test_04()
{
// hresume[0].contact.url[0]
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes.GetNameByPosition("url", 0).Value;
Assert.That(test, Is.EqualTo("http://example.com/johndoe/"), "Should collect the URL from href attribute" );
}
 
 
[Test]
public void Test_05()
{
// hresume[0].summary
string test = nodes.GetNameByPosition("hresume", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("Interactive designer looking for a job"), "Should collect the inner text of the first element with a summary class" );
}
 
}
}
