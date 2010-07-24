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
public class test_value_dt_test_YYYY_DDD__HH_MM_SS
{
// http://microformats.org/wiki/value-dt-test-YYYY-DDD--HH-MM-SS
// value-dt-test-YYYY-DDD--HH-MM-SS
// The value-dt-test-YYYY-DDD--HH-MM-SS test demonstrate the concatenation of two html elements to create one datetime value. The date contains a year number and the number of days.
// Built: 21 July 2010
 
UfWebRequest webRequest;
UfDataNodes nodes;
 
[SetUp]
public void Test_Settup()
{
webRequest = new UfWebRequest();
string url = "http://microformats.org/wiki/value-dt-test-YYYY-DDD--HH-MM-SS#uf";
webRequest.Load(url, UfFormats.HCalendar());
nodes = webRequest.Data.Nodes;
}
 
 
[Test]
public void Test_01()
{
// vevent[0].dtstart
string test = nodes.GetNameByPosition("vevent", 0).Nodes["dtstart"].Value;
string testDateTime = new Rfc3389DateTime(test).ToString();
string resultDateTime = new Rfc3389DateTime("2009-10-03T17:09:34").ToString();
Assert.That(testDateTime, Is.EqualTo(resultDateTime), "With the value class pattern the results should contain a time" );
}
 
}
}
