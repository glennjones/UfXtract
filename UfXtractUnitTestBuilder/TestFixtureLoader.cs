//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;
using UfXtract;
using System.IO;

namespace UfXtractUnitTestBuilder
{
    public class TestFixtureLoader
    {
        // The number of found test fixtures
        private int number = 1;
        string path = "";


        public TestFixtureLoader()
        {
        }


        /// <summary>
        /// Parses a TestSuite to find all TestFixture
        /// </summary>
        /// <param name="url">The Url of a TestSuite</param>
        public void ParseTestSuite(string url)
		{
            // Get path relative to this project
            if (path == "")
            {
                String currentpath = Directory.GetCurrentDirectory();
                int startNum = currentpath.IndexOf("ufXtractUnitTestBuilder");
                if (startNum > 0)
                {
                    path = currentpath.Substring(0, startNum) + "ufXtractUnitTests\\";
                    Console.WriteLine("Writing unit text files to: " + path);
                }
                else
                {
                    Console.WriteLine("Problem finding path to write unit text files");
                }
            }

            number = 1;
            UfWebRequest webRequest = new UfWebRequest();
            webRequest.Load(url, UfFormats.TestSuite());
            foreach (UfDataNode node in webRequest.Data.Nodes)
            {
                FindTestSuite(node);
            }
		}


        /// <summary>
        /// Finds each test suite group 
        /// </summary>
        /// <param name="node"></param>
        public void FindTestSuite(UfDataNode node)
        {
            
            foreach (UfDataNode childNode in node.Nodes)
            {
                string url = childNode.Nodes["url"].Value;
                string format = childNode.Nodes["format"].Value;
                //Console.WriteLine("Found: " + url);
                
                LoadTestFixture(url);
                number++;
            }
        }


        /// <summary>
        /// Loads and parses a TestFixture
        /// </summary>
        /// <param name="url">The Url of the test fixture page</param>
        public void LoadTestFixture(string url)
        {
            UfWebRequest webRequest = new UfWebRequest();
            webRequest.Load(url, UfFormats.TestFixture());
            BuildTest(webRequest.Data, url);
        }


        public string TestName(string summary, string url)
        {
            string output = "";

            if (url.StartsWith("http://ufxtract.com/") || url.StartsWith("http://www.ufxtract.com/"))
                output = summary.Split('-')[0].Trim().Replace('/', '_').Replace(" ", "_");
            else
                output = summary.Replace('-', '_').Replace("/", "_").Replace(" ", "_");

            return output;
        }


        public void BuildTest(UfDataNode node, string url)
        {

            // Find summary, description and format
            string summary = node.Nodes[0].Nodes["summary"].Value;
            string description = node.Nodes[0].Nodes["description"].Value;
            string format = node.Nodes[0].Nodes["format"].Value;
            //Console.WriteLine(summary);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections;");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.Text;");
            stringBuilder.AppendLine("using NUnit.Framework;");
            stringBuilder.AppendLine("using NUnit.Framework.Constraints;");
            stringBuilder.AppendLine("using NUnit.Framework.SyntaxHelpers;");
            stringBuilder.AppendLine("using UfXtract;");
            stringBuilder.AppendLine("using UfXtract.Utilities;");
            stringBuilder.AppendLine(" ");
            stringBuilder.AppendLine("namespace UfXtract.UnitTests." + format);
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(" ");


            stringBuilder.AppendLine("[TestFixture]");
            stringBuilder.AppendLine("public class test_" + TestName(summary, url));
            stringBuilder.AppendLine("{");

            stringBuilder.AppendLine("// " + url );
            stringBuilder.AppendLine("// " + summary);
            stringBuilder.AppendLine("// " + description);
            stringBuilder.AppendLine("// Built: " + DateTime.Now.ToLongDateString() );

            stringBuilder.AppendLine(" ");
            stringBuilder.AppendLine("UfWebRequest webRequest;");
            stringBuilder.AppendLine("UfDataNodes nodes;");
            stringBuilder.AppendLine(" ");

            stringBuilder.AppendLine("[SetUp]");
            stringBuilder.AppendLine("public void Test_Settup()");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("webRequest = new UfWebRequest();");
            stringBuilder.AppendLine("string url = \"" + url + "#uf\";");

            if( format.ToLower() == "hcard")
                stringBuilder.AppendLine("webRequest.Load(url, UfFormats.HCard());");
            if (format.ToLower() == "hcalendar")
                stringBuilder.AppendLine("webRequest.Load(url, UfFormats.HCalendar());");
            if( format.ToLower() == "hresume")
                stringBuilder.AppendLine("webRequest.Load(url, UfFormats.HResume());");


            stringBuilder.AppendLine("nodes = webRequest.Data.Nodes;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine(" ");

            int assertNumber = 1;
            foreach (UfDataNode childNode in node.Nodes[0].Nodes)
            {
                if (childNode.Name == "assert")
                {
                    BuildAsset(childNode, stringBuilder, assertNumber);
                    assertNumber ++;
                }
            }

            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("}");

            string filename = "test_" +  TestName(summary, url) + ".cs";

            string filepath = path + filename;
            WriteFile(stringBuilder.ToString(), filepath);
        

        }


        public void BuildAsset(UfDataNode node, StringBuilder stringBuilder, int assertNumber)
        {
            string test = node.Nodes["test"].Value;
            string result = node.Nodes["result"].Value;
            string comment = node.Nodes["comment"].Value;

            stringBuilder.AppendLine(" ");
            stringBuilder.AppendLine("[Test]");
            if( assertNumber > 9 )
                stringBuilder.AppendLine("public void Test_" + assertNumber.ToString() + "()");
            else
                stringBuilder.AppendLine("public void Test_0" + assertNumber.ToString() + "()");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("// " + test);

            BuildAssertResult(stringBuilder, test, result, comment);

            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine(" ");
        }


        /// <summary>
        /// Converts to JavaScript object refenerce to c# object refenerce
        /// </summary>
        /// <param name="test">JavaScript object refenerce</param>
        /// <returns></returns>
        public string BuildObjectRefPath( string test )
        {
            //vcard[0].n.given-name[0]
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("nodes");
            string[] parts = test.Split('.');
            for (int i = 0; i < parts.Length; i++)
            {
                if( parts[i].Contains("[") )
                {
                    string[] subParts = parts[i].Split('[');
                    if (i > 0)
                        stringBuilder.Append(".Nodes");
                    stringBuilder.Append(".GetNameByPosition(\"" + subParts[0] + "\", " + subParts[1].Substring(0,subParts[1].Length-1) + ")" );
                }
                else
                {
                    stringBuilder.Append( ".Nodes[\"" + parts[i] + "\"]" );
                }

            }
            stringBuilder.Append(".Value");
            return stringBuilder.ToString();
        }


        public void BuildAssertResult(StringBuilder stringBuilder, string test, string result, string comment)
        {

            // Need to add each assert type below
            // IsEqualTo()
            // IsEqualToCaseInsensitive()
            // IsEqualToISODate()
            // IsEqualToGeo()
            // IsEqualToPhoneNumbers()
            // IsTrue()
            // IsFalse()
            // HasProperty()


            if (result.Contains("IsEqualTo("))
            {
                stringBuilder.AppendLine("string test = " + BuildObjectRefPath(test) + ";");
                result = result.Replace("IsEqualTo","Is.EqualTo");
                stringBuilder.AppendLine("Assert.That(test, " + result + ", \"" + comment.Replace("\"", "\\\"") + "\" );");
            }

            if (result.Contains("IsEqualToCaseInsensitive("))
            {
                stringBuilder.AppendLine("string test = " + BuildObjectRefPath(test) + ";");
                result = result.Replace("IsEqualToCaseInsensitive", "Is.EqualTo");
                stringBuilder.AppendLine("Assert.That(test, " + result + ".IgnoreCase, \"" + comment.Replace("\"", "\\\"") + "\" );");
            }

            if (result.Contains("IsEqualToISODate("))
            {
                result = result.Replace("IsEqualToISODate(", "").Replace(")", "");

                stringBuilder.AppendLine("string test = " + BuildObjectRefPath(test) + ";");
                stringBuilder.AppendLine("string testDateTime = new Rfc3389DateTime(test).ToString();");
                stringBuilder.AppendLine("string resultDateTime = new Rfc3389DateTime(" + result + ").ToString();");
                stringBuilder.AppendLine("Assert.That(testDateTime, Is.EqualTo(resultDateTime), \"" + comment.Replace("\"", "\\\"") + "\" );");
            }

            if (result.Contains("IsEqualToGeo("))
            {
                result = result.Replace("IsEqualToGeo(", "").Replace(")", "");
                stringBuilder.AppendLine("string test = " + BuildObjectRefPath(test) + ";");
                stringBuilder.AppendLine("string testGeo = new Geo(test).ToString();");
                stringBuilder.AppendLine("string resultGeo = new Geo(" + result + ").ToString();");
                stringBuilder.AppendLine("Assert.That(testGeo, Is.EqualTo(resultGeo), \"" + comment.Replace("\"", "\\\"") + "\" );");
            }

            if (result.Contains("IsEqualToPhoneNumbers("))
            {
                result = result.Replace("IsEqualToPhoneNumbers(", "").Replace(")", "");
                stringBuilder.AppendLine("string test = " + BuildObjectRefPath(test) + ";");
                stringBuilder.AppendLine("string testPhoneNumber = new PhoneNumber(test).Canonicalised;");
                stringBuilder.AppendLine("string resultPhoneNumber = new PhoneNumber(" + result + ").Canonicalised;");
                stringBuilder.AppendLine("Assert.That(testPhoneNumber, Is.EqualTo(resultPhoneNumber), \"" + comment.Replace("\"", "\\\"") + "\" );");
            }

            if (result.Contains("IsTrue("))
            {
                result = result.Replace("IsTrue", "Is.True");
                stringBuilder.AppendLine("bool test = Convert.ToBoolean(" + BuildObjectRefPath(test) + ");");
                stringBuilder.AppendLine("Assert.That(test, " + result + ", \"" + comment.Replace("\"", "\\\"") + "\" );");
            }

            if (result.Contains("IsFalse("))
            {
                result = result.Replace("IsFalse", "Is.False");
                stringBuilder.AppendLine("bool test = Convert.ToBoolean(" + BuildObjectRefPath(test) + ");");
                stringBuilder.AppendLine("Assert.That(test, " + result + ", \"" + comment.Replace("\"", "\\\"") + "\" );");
            }

            if (result.Contains("HasProperty("))
            {
                stringBuilder.AppendLine("bool hasProperty = true;");
                stringBuilder.AppendLine("try");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("string test = " + BuildObjectRefPath(test) + ";");
                stringBuilder.AppendLine("}");
                stringBuilder.AppendLine("catch(Exception ex)");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("hasProperty = false;");
                stringBuilder.AppendLine("}");
                if(result.ToLower().Contains("true"))
                    stringBuilder.AppendLine("Assert.That(hasProperty, Is.True, \"" + comment.Replace("\"", "\\\"") + "\" );");
                else if (result.ToLower().Contains("false"))
                    stringBuilder.AppendLine("Assert.That(hasProperty, Is.False, \"" + comment.Replace("\"", "\\\"") + "\" );");
            }



        }


        public void WriteFile(string contents, string path)
        {
            Console.WriteLine(path);
            // Creates or updates a  file
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(contents);
                sw.Close();
            }
        }


        public String Path
        {
            get { return path; }
            set { path = value; }
        }


    }
}
