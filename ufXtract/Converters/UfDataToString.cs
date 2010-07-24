//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Web;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace UfXtract
{


    /// <summary>
    /// Converts a UfDataNode into a string with a indented tree structure.
    /// </summary>
    public class UfDataToString
    {

        private bool tree = false;
        private Urls urls = null;
        private UfErrors errors = null;


        /// <summary>
        /// Converts a UfDataNode into a string with a indented tree structure.
        /// </summary>
        public UfDataToString(){}



        /// <summary>
        /// Provides string version of data object
        /// </summary>
        /// <param name="node">Node</param>

        /// <returns>String with indented tree structure</returns>
        public string Convert( UfDataNode node)
        {

            string output = "ufxtract\n";
            output = BuildDataString(output, node, 0);


            //Find errors
            // -------------------------------------------------
            if (errors != null)
            {
                if (errors.Count != 0)
                {
                    output += "\n\nerrors" + "\n";
                    if (errors != null)
                    {
                        foreach (UfError ufError in errors)
                        {
                            output += "msg: " + ufError.Message + "\n";
                            output += "url: " + ufError.Address + "\n";
                            if (ufError.Status != 0)
                                output += "status: " + ufError.Status.ToString() + "\n";
                        }
                    }
                }
            }

            // Write report if a Urls object is provided
            // -------------------------------------------------
            if (urls != null)
            {
                output += "\n\nreport" + "\n";
                foreach (Url url in urls)
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
        /// Build a string from data
        /// </summary>
        /// <param name="output">Allows for method to call itself</param>
        /// <param name="node">The data object</param>
        /// <param name="indent">Current indent</param>
        /// <returns></returns>
        private string BuildDataString(string output, UfDataNode node, int indent)
        {
            string sIndent = string.Empty;

            if( indent == 1 )
                output += "\n";

            for (int i = 0; i < indent; i++)
                sIndent += " ";

            // Json data structure
            if (tree)
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

            //if (reporting && node.OuterHtml != string.Empty)
            //    output += sIndent + "OuterHtml: " + sIndent + HttpUtility.HtmlEncode(node.OuterHtml) + "\n";
            

            foreach (UfDataNode childNode in node.Nodes)
                output += BuildDataString("", childNode, indent + 1);

            return output;
        }



        /// <summary>
        /// Weather data tree structure uses hierarchal to display multiples
        /// </summary>
        public bool HierarchalTree
        {
            get { return tree; }
            set { tree = value; }
        }


        /// <summary>
        /// Collection of error information fro reporting
        /// </summary>
        public UfErrors Errors
        {
            get { return errors; }
            set { errors = value; }
        }

        /// <summary>
        /// Collection of Url information for reporting
        /// </summary>
        public Urls Urls
        {
            get { return urls; }
            set { urls = value; }
        }
        

    }
}
