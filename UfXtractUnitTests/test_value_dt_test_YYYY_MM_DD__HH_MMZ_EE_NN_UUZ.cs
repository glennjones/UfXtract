using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using UfXtract;
using UfXtract.Utilities;
 
namespace UfXtract.UnitTests.hCalendar
{
 
[TestFixture]
public class test_value_dt_test_YYYY_MM_DD__HH_MMZ_EE_NN_UUZ
{
// http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HH-MMZ-EE-NN-UUZ
// value-dt-test-YYYY-MM-DD--HH-MMZ-EE-NN-UUZ
// The value-dt-test-YYYY-MM-DD--HH-MMZ-EE-NN-UUZ test demonstrate the concatenation of two html elements to create one datetime value. The date for dtend is implied from the dtstart date:
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://microformats.org/wiki/value-dt-test-YYYY-MM-DD--HH-MMZ-EE-NN-UUZ#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2009-07-26T16:04Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "With the value class pattern the results should contain a time" );
}
 
 
[Test]
public void Test_02()
{
// vevent[0].dtend
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtend"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2009-07-26T17:18:19Z").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "With the value class pattern the results should contain a time" );
}
 
}
}
