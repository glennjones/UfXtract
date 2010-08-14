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
public class test_hCard_14
{
// http://www.ufxtract.com/testsuite/hcard/hcard14.htm
// hCard 14 - area element test
// This page was design to test the use of the area element. The IsEqualToPhoneNumber method canonicalises and compares phone numbers.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard14.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].tel[0].value
}
 
 
[Test]
public void Test_02()
{
// vcard[0].tel[0].type[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("tel", 0).Nodes.GetNameByPosition("type", 0).Value;
Assert.That(test, Is.EqualTo("Tel").IgnoreCase, "Should take the value from the object data attribute" );
}
 
}
}
