using System;
using System.Web;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ufXtract
{
    public class ufDataToString
    {
        //Copyright (c) 2007 Glenn Jones
        private bool m_bHierarchalTree = false;
        private bool m_bReporting = false;
        private bool m_bMultiples = false;
        private Urls m_oUrls = null;
        private Errors m_oErrors = null;

        public ufDataToString()
		{
		}






        /// <summary>
        /// Provides string version of data object
        /// </summary>
        /// /// <param name="node">The data object</param>
        public string Convert( ufDateNode node, bool multiples )
        {
            m_bMultiples = multiples;

            string output = "ufxtract\n";
            output = BuildDataString(output, node, 0);


            //Find errors
            // -------------------------------------------------
            if (m_oErrors != null)
            {
                if (m_oErrors.Count != 0)
                {
                    output += "\n\nerrors" + "\n";
                    if (m_oErrors != null)
                    {
                        foreach (Error error in m_oErrors)
                        {
                            output += "msg: " + error.Message + "\n";
                            output += "url: " + error.Address + "\n";
                            if (error.Status != 0)
                                output += "status: " + error.Status.ToString() + "\n";
                        }
                    }
                }
            }

            // Write report if a Urls object is provided
            // -------------------------------------------------
            if (m_bReporting && m_oUrls != null)
            {
                output += "\n\nreport" + "\n";
                foreach (Url url in m_oUrls)
                {
                    output += "url: " + url.Address + "\n";
                    output += "status: " + url.Status.ToString() + "\n";
                    output += "millisec: " + url.LoadTime.Milliseconds.ToString() + "\n\n";

                }
                output += "found: " + node.Nodes.Count.ToString();
            }


            return output;
        }


        /// <summary>
        /// Provides string version of data object
        /// </summary>
        /// <param name="node">The data object</param>
        /// <param name="urls">The Urls object created by spider</param>
        /// <returns></returns>
        public string Convert(ufDateNode node, bool multiples, Urls urls)
        {
            m_oUrls = urls;
            return Convert(node, multiples);
        }


        /// <summary>
        /// Provides string version of data object
        /// </summary>
        /// <param name="node">The data object</param>
        /// <param name="urls">The Urls object created by spider</param>
        /// <returns></returns>
        public string Convert(ufDateNode node, bool multiples, Urls urls, Errors errors)
        {
            m_oErrors = errors;
            m_oUrls = urls;
            return Convert(node, multiples);
        }


        /// <summary>
        /// Provides string version of data object
        /// </summary>
        /// <param name="node">The data object</param>
        /// <param name="urls">The Urls object created by spider</param>
        /// <returns></returns>
        public string Convert(ufDateNode node, bool multiples, Urls urls, Errors errors, bool reporting)
        {
            m_bReporting = reporting;
            m_oErrors = errors;
            m_oUrls = urls;
            return Convert(node, multiples);
        }



        /// <summary>
        /// Build a string from data
        /// </summary>
        /// <param name="output">Allows for method to call itself</param>
        /// <param name="node">The data object</param>
        /// <param name="indent">Current indent</param>
        /// <returns></returns>
        private string BuildDataString(string output, ufDateNode node, int indent)
        {
            string sIndent = string.Empty;

            if( indent == 1 )
                output += "\n";

            for (int i = 0; i < indent; i++)
                sIndent += " ";

            // Json data structure
            if (m_bHierarchalTree)
            {
                if (node.ValueArray.Count > 0)
                {
                    output += sIndent + node.Name + ": " + "\n";
                    for (int i = 0; i < node.ValueArray.Count; i++)
                    {
                        output += sIndent + "[" + i.ToString() + "]: " + node.ValueArray[i] + "\n";
                    }
                }
                else
                {
                    output += sIndent + node.Name + ": " + node.Value + "\n";
                }
            }
            // Standard data structure
            else
            {
                if (node.Name != string.Empty)
                {

                    output += sIndent + node.Name + ": " + node.Value + "\n";

                    if( node.RepresentativeNode )
                        output += sIndent + " " + "representative-hcard: true\n";

                    if (node.SourceUrl != string.Empty)
                        output += sIndent + " " + "source-url: " + node.SourceUrl + "\n";
                    
                }


            }

            //if (m_bReporting && node.OuterHtml != string.Empty)
            //    output += sIndent + "OuterHtml: " + sIndent + HttpUtility.HtmlEncode(node.OuterHtml) + "\n";
            

            foreach (ufDateNode childNode in node.Nodes)
                output += BuildDataString("", childNode, indent + 1);

            return output;
        }



        /// <summary>
        /// If the data tree structure uses hierarchal to display multiples
        /// This is use for displaying Json type array structures
        /// </summary>
        public bool HierarchalTree
        {
            get { return m_bHierarchalTree; }
            set { m_bHierarchalTree = value; }
        }


        public bool Reporting
        {
            get { return m_bReporting; }
            set { m_bReporting = value; }
        }
        

    }
}
