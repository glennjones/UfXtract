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
public class test_hCard_10
{
// http://www.ufxtract.com/testsuite/hcard/hcard10.htm
// hCard 10 - geo comprehensive data format test
// This page was design to comprehensively test the geo format. Most of these tests are based on Mike Kaply work for the Firefox. The IsEqualToGeo method canonicalise and compares geo's.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard10.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].geo.latitude
string test = nodes.GetNameByPosition("vcard", 0).Nodes["geo"].Nodes["latitude"].Value;
string testGeo = new Geo(test).ToString();
string resultGeo = new Geo("0").ToString();
Assert.That(testGeo, Is.EqualTo(resultGeo), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_02()
{
// vcard[1].geo.latitude
string test = nodes.GetNameByPosition("vcard", 1).Nodes["geo"].Nodes["latitude"].Value;
string testGeo = new Geo(test).ToString();
string resultGeo = new Geo("0").ToString();
Assert.That(testGeo, Is.EqualTo(resultGeo), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_03()
{
// vcard[2].geo.latitude
string test = nodes.GetNameByPosition("vcard", 2).Nodes["geo"].Nodes["latitude"].Value;
string testGeo = new Geo(test).ToString();
string resultGeo = new Geo("0").ToString();
Assert.That(testGeo, Is.EqualTo(resultGeo), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_04()
{
// vcard[3].geo.latitude
string test = nodes.GetNameByPosition("vcard", 3).Nodes["geo"].Nodes["latitude"].Value;
string testGeo = new Geo(test).ToString();
string resultGeo = new Geo("23.7").ToString();
Assert.That(testGeo, Is.EqualTo(resultGeo), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_05()
{
// vcard[4].geo.latitude
string test = nodes.GetNameByPosition("vcard", 4).Nodes["geo"].Nodes["latitude"].Value;
string testGeo = new Geo(test).ToString();
string resultGeo = new Geo("0").ToString();
Assert.That(testGeo, Is.EqualTo(resultGeo), "Should find latitude value from single element" );
}
 
 
[Test]
public void Test_06()
{
// vcard[5].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 5).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_07()
{
// vcard[6].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 6).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_08()
{
// vcard[7].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 7).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_09()
{
// vcard[8].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 8).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_10()
{
// vcard[9].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 9).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_11()
{
// vcard[10].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 10).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_12()
{
// vcard[11].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 11).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_13()
{
// vcard[12].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 12).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
 
[Test]
public void Test_14()
{
// vcard[13].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 13).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.False, "Is an illegal data format for geo" );
}
 
}
}
