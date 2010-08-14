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
public class test_hCard_5
{
// http://www.ufxtract.com/testsuite/hcard/hcard5.htm
// hCard 5 - multiple values in class attribute test
// This page test that parsers can find a class name where a author has used multiple values in the class attributes. The IsEqualToISODate method normalisation and compares dates.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard5.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].n.given-name[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Nodes.GetNameByPosition("given-name", 0).Value;
Assert.That(test, Is.EqualTo("John"), "Should find given-name value even if class attribute has multiple values" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].category[1].tag
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("category", 1).Nodes["tag"].Value;
Assert.That(test, Is.EqualTo("development"), "Should find category value even if class and rel attribute have multiple values" );
}
 
 
[Test]
public void Test_03()
{
// vcard[0].rev
string test = nodes.GetNameByPosition("vcard", 0).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2008-01-01T13:45:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "Should find rev value even if class attribute has multiple values" );
}
 
}
}
