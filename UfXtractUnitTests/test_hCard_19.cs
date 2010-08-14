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
public class test_hCard_19
{
// http://www.ufxtract.com/testsuite/hcard/hcard19.htm
// hCard 19 - object include test
// This page was design to test the object include pattern. http://microformats.org/wiki/include-pattern.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard19.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[1].adr[0].street-address[0]
string test = nodes.GetNameByPosition("vcard", 1).Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("street-address", 0).Value;
Assert.That(test, Is.EqualTo("31 Gresse Street"), "The street-address is added using the object include pattern" );
}
 
 
[Test]
public void Test_02()
{
// vcard[2].adr[0].street-address[0]
string test = nodes.GetNameByPosition("vcard", 2).Nodes.GetNameByPosition("adr", 0).Nodes.GetNameByPosition("street-address", 0).Value;
Assert.That(test, Is.EqualTo("31 Gresse Street"), "The street-address is added using the object include pattern" );
}
 
}
}
