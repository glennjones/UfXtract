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
public class test_hCard_9
{
// http://www.ufxtract.com/testsuite/hcard/hcard9.htm
// hCard 9 - extracting geo singular and paired values test
// This page was design to comprehensively test the geo format. Most of these tests are based on Mike Kaply work for the Firefox. The IsEqualToGeo method canonicalise and compares geo's.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard9.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].geo.latitude
string test = nodes.GetNameByPosition("vcard", 0).Nodes["geo"].Nodes["latitude"].Value;
string testGeo = new Geo(test).ToString();
string resultGeo = new Geo("37.77").ToString();
Assert.That(testGeo, Is.EqualTo(resultGeo), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].geo.latitude
string test = nodes.GetNameByPosition("vcard", 1).Nodes["geo"].Nodes["latitude"].Value;
string testGeo = new Geo(test).ToString();
string resultGeo = new Geo("37.77").ToString();
Assert.That(testGeo, Is.EqualTo(resultGeo), "Should extract latitude value from paired value" );
}
 
}
}
