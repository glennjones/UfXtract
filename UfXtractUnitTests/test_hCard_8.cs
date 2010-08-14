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
public class test_hCard_8
{
// http://www.ufxtract.com/testsuite/hcard/hcard8.htm
// hCard 8 - extracting URLs test
// This page test that parsers can extract URLs from differnt structures.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard8.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].url[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("url", 0).Value;
Assert.That(test, Is.EqualTo("http://example.com/johndoe/"), "Should collect the URL from the a element" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].url[0]
string test = nodes.GetNameByPosition("vcard", 1).Nodes.GetNameByPosition("url", 0).Value;
Assert.That(test, Is.EqualTo("http://example.com/johndoe/"), "Should collect the URL from the area element" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].photo[0]
string test = nodes.GetNameByPosition("vcard", 2).Nodes.GetNameByPosition("photo", 0).Value;
Assert.That(test, Is.EqualTo("http://ufxtract.com/testsuite/images/photo.gif"), "Should collect the URL of the image element" );
}
 
}
}
