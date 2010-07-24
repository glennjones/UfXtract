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
public class test_hResume_1
{
// http://www.ufxtract.com/testsuite/hresume/hresume1.htm
// hResume 1 - single occurrence test
// This page was design to test that values of a hResume which are meant to have only a single occurrence are parsed correctly.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hresume/hresume1.htm#uf";
webRequest.Load(url, UfFormats.HResume());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// hresume[0].contact
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The contact is a singular value" );
}
 
 
[Test]
public void Test_02()
{
// hresume[0].contact.fn
string test = nodes.GetNameByPosition("hresume", 0).Nodes["contact"].Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("John Doe"), "The contact is a hcard and has a fn value" );
}
 
 
[Test]
public void Test_03()
{
// hresume[0].summary
string test = nodes.GetNameByPosition("hresume", 0).Nodes["summary"].Value;
Assert.That(test, Is.EqualTo("Interactive designer looking for a job"), "The summary is a singular value" );
}
 
}
}
