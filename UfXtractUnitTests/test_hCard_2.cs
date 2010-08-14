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
public class test_hCard_2
{
// http://www.ufxtract.com/testsuite/hcard/hcard2.htm
// hCard 2 - multiple occurrence test
// This page was design to test that values of a hcard which are can have multiple occurrences are parsed correctly.
// Built: 14 August 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://www.ufxtract.com/testsuite/hcard/hcard2.htm#uf";
webRequest.Load(url, UfFormats.HCard());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vcard[0].adr[1]
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("adr", 1).Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The adr (address) is a optional multiple value" );
}
 
 
[Test]
public void Test_02()
{
// vcard[0].email[1]
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("email", 1).Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The class is a optional multiple value" );
}
 
 
[Test]
public void Test_03()
{
// vcard[0].org[1]
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("org", 1).Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The org is a optional multiple value" );
}
 
 
[Test]
public void Test_04()
{
// vcard[0].tel[1]
bool hasProperty = true;
try
{
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("tel", 1).Value;
}
catch(Exception ex)
{
hasProperty = false;
}
Assert.That(hasProperty, Is.True, "The tel is a optional multiple value" );
}
 
 
[Test]
public void Test_05()
{
// vcard[0].agent[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("agent", 1).Value;
Assert.That(test, Is.EqualTo("Dave Doe"), "The agent is a optional multiple value" );
}
 
 
[Test]
public void Test_06()
{
// vcard[0].category[1].tag
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("category", 1).Nodes["tag"].Value;
Assert.That(test, Is.EqualTo("development"), "The category is a optional multiple value" );
}
 
 
[Test]
public void Test_07()
{
// vcard[0].key[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("key", 0).Value;
Assert.That(test, Is.EqualTo("hd02$Gfu*d%dh87KTa2=23934532479"), "The key is a optional multiple value" );
}
 
 
[Test]
public void Test_08()
{
// vcard[0].label[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("label", 1).Value;
Assert.That(test, Is.EqualTo("West Street, Brighton, United Kingdom"), "The label is a optional multiple value" );
}
 
 
[Test]
public void Test_09()
{
// vcard[0].logo[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("logo", 1).Value;
Assert.That(test, Is.EqualTo("http://www.ufxtract.com/testsuite/images/logo.gif"), "The logo is a optional multiple value" );
}
 
 
[Test]
public void Test_10()
{
// vcard[0].mailer[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("mailer", 1).Value;
Assert.That(test, Is.EqualTo("Outlook 2007"), "The mailer is a optional multiple value" );
}
 
 
[Test]
public void Test_11()
{
// vcard[0].nickname[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("nickname", 1).Value;
Assert.That(test, Is.EqualTo("Lost boy"), "The nickname is a optional multiple value" );
}
 
 
[Test]
public void Test_12()
{
// vcard[0].note[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("note", 1).Value;
Assert.That(test, Is.EqualTo("It can be a real problem booking a hotel room with the name John Doe."), "The note is a optional multiple value" );
}
 
 
[Test]
public void Test_13()
{
// vcard[0].photo[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("photo", 0).Value;
Assert.That(test, Is.EqualTo("http://www.ufxtract.com/testsuite/images/photo.gif"), "The photo is a optional multiple value" );
}
 
 
[Test]
public void Test_14()
{
// vcard[0].sound[0]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("sound", 0).Value;
Assert.That(test, Is.EqualTo("Pronunciation of my name"), "The sound is a optional multiple value" );
}
 
 
[Test]
public void Test_15()
{
// vcard[0].title[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("title", 1).Value;
Assert.That(test, Is.EqualTo("Owner"), "The title is a optional multiple value" );
}
 
 
[Test]
public void Test_16()
{
// vcard[0].url[1]
string test = nodes.GetNameByPosition("vcard", 0).Nodes.GetNameByPosition("url", 1).Value;
Assert.That(test, Is.EqualTo("http://www.webfeetmedia.com/"), "The url is a optional multiple value" );
}
 
}
}
