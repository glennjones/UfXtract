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
public class test_hCard_1
{
// http://www.ufxtract.com/testsuite/hcard/hcard1.htm
// hCard 1 - single occurrence test
// This page was design to test that values of a hcard which are meant to have only a single occurrence are parsed correctly. The IsEqualToISODate method uses date normalisation and compare methods.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard1.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].fn
string test = nodes.GetNameByPosition("vcard", 0).Nodes["fn"].Value;
Assert.That(test, Is.EqualTo("John Doe"), "The fn (formatted name) is a singular value" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].n
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 0).Nodes["n"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The n (name) is a singular value" );
}
 
 
[Test]
public void Test_03()
{
// vcard[0].bday
string test = nodes.GetNameByPosition("vcard", 0).Nodes["bday"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2000-01-01T00:00:00-0800").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The bday (birthday) is a singular value" );
}
 
 
[Test]
public void Test_04()
{
// vcard[0].class
string test = nodes.GetNameByPosition("vcard", 0).Nodes["class"].Value;
Assert.That(test, Is.EqualTo("Public"), "The class is a singular value" );
}
 
 
[Test]
public void Test_05()
{
// vcard[0].geo
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 0).Nodes["geo"].Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The geo is a singular value" );
}
 
 
[Test]
public void Test_06()
{
// vcard[0].rev
string test = nodes.GetNameByPosition("vcard", 0).Nodes["rev"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2008-01-01T13:45:00").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "The rev is a singular value" );
}
 
 
[Test]
public void Test_07()
{
// vcard[0].role
string test = nodes.GetNameByPosition("vcard", 0).Nodes["role"].Value;
Assert.That(test, Is.EqualTo("Designer"), "The role is a singular value" );
}
 
 
[Test]
public void Test_08()
{
// vcard[0].sort-string
string test = nodes.GetNameByPosition("vcard", 0).Nodes["sort-string"].Value;
Assert.That(test, Is.EqualTo("John"), "The sort-string is a singular value" );
}
 
 
[Test]
public void Test_09()
{
// vcard[0].tz
string test = nodes.GetNameByPosition("vcard", 0).Nodes["tz"].Value;
Assert.That(test, Is.EqualTo("-05:00"), "The tz is a singular value" );
}
 
 
[Test]
public void Test_10()
{
// vcard[0].uid
string test = nodes.GetNameByPosition("vcard", 0).Nodes["uid"].Value;
Assert.That(test, Is.EqualTo("com.johndoe/profiles/johndoe"), "The uid is a singular value" );
}
 
}
}
