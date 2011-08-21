//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UfXtract.Utilities;

namespace UfXtract
{
    /// <summary>
    /// Converts a UfDataNode structure into JSON
    /// </summary>
    public class UfDataToJson
    {
    
        private UfDataNode tree = new UfDataNode();
        private string callBack = string.Empty;
        private Urls urls = null;
        private UfErrors errors = null;


        /// <summary>
        /// Converts a UfDataNode structure into JSON
        /// </summary>
        public UfDataToJson(){}





        /// <summary>
        /// Converts a UfDataNode structure into JSON
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="formatDescriber">Microformat format describer object</param>
        /// <param name="callBack">JSONP callback function name to wrap JSON object</param>
        /// <returns>JSON string</returns>
        public string Convert(UfDataNode node, UfFormatDescriber formatDescriber, string callBack)
        {
            this.callBack = callBack;
            this.callBack = this.callBack.Replace("(", "").Replace(")", "").Trim();
            return Convert(node, formatDescriber);
        }


        /// <summary>
        /// Converts a UfDataNode structure into JSON
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="formatArray">Array of microformat format describer to describer data in node</param>
        /// <returns>JSON string</returns>
        public string Convert(UfDataNode node, ArrayList formatArray)
        {
            return Convert(node, formatArray, "");
        }


      

        /// <summary>
        /// Converts a UfDataNode structure into JSON
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="formatArray">Array of microformat format describer to describer data in node</param>
        /// <param name="callBack">JSONP callback function name to wrap JSON object</param>
        /// <returns>JSON string</returns>
        public string Convert(UfDataNode node, ArrayList formatArray, string callBack)
        {
            
            this.callBack = callBack;
            this.callBack = this.callBack.Replace("(", "").Replace(")", "").Trim();
            this.urls = urls;
            this.errors = errors;

            foreach (UfFormatDescriber formatDescriber in formatArray)
            {
                foreach (UfDataNode childNode in node.Nodes)
                {
                    foreach (UfDataNode grandChildNode in childNode.Nodes)
                    {
                        if (grandChildNode.Name == formatDescriber.BaseElement.Name)
                        {
                            UfDataNode xChild = tree.Nodes.Append(grandChildNode.Name, grandChildNode.Value, grandChildNode.SourceUrl, grandChildNode.RepresentativeNode);
                            if (grandChildNode.Nodes.Count > 0)
                                AddChildNodes(xChild, grandChildNode, formatDescriber.BaseElement);

                        }
                    }
                }
            }

            //string output = "// UfXtract \n";
            string output = "";
            if (callBack != string.Empty)
                output += callBack + "( ";

            output += "{";

            foreach (UfDataNode childNode in tree.Nodes)
                output += BuildDataString(childNode, true, false);

            if (tree.Nodes.Count > 0)
                output = output.Substring(0, output.Length - 2);

            output += AddUfErrors();
            output += AddReporting(node);

            // End whole block
            output += "}";

            if (callBack != string.Empty)
                output += " )";

            //return output.Replace(",", ",\n").Replace("}", "}\n").Replace("{", "{\n").Replace("]", "]\n").Replace("[", "[\n"); ;
            return output;

        }






        /// <summary>
        /// Converts a UfDataNode structure into JSON
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="formatDescriber">Microformat format describer object</param>
        /// <returns>JSON string</returns>
        public string Convert(UfDataNode node, UfFormatDescriber formatDescriber)
        {
        
            foreach (UfDataNode childNode in node.Nodes)
            {
                if (childNode.Name == formatDescriber.BaseElement.Name)
                {
                    UfDataNode xChild = tree.Nodes.Append(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                    if (childNode.Nodes.Count > 0)
                        AddChildNodes(xChild, childNode, formatDescriber.BaseElement);
                    
                }
            }

            //string output = "// UfXtract \n";
            string output = "";
            if( callBack != string.Empty)
                output += callBack + "( ";

            output += "{\"microformats\": {";

            foreach (UfDataNode childNode in tree.Nodes)
                output += BuildDataString(childNode, true, false);

            if (tree.Nodes.Count > 0)
                output = output.Substring(0, output.Length - 2);

            output += AddUfErrors();
            output += AddReporting( node );

            // End whole block
            output += "}}";
         
            if (callBack != string.Empty)
                output += " )";

            return  output; 

        }



        private string AddReporting( UfDataNode node )
        {
            string output = string.Empty;
            if (urls != null)
            {

                if (tree.Nodes.Count > 0 || errors.Count > 0)
                    output += ", \"parser-information\" : {";
                else
                    output += "\"parser-information\" : {";

                output += "\"name\" : \"UfXtract\", ";
                output += "\"version\" : \"" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\", ";

                for (int i = 0; i < urls.Count; i++)
                {
                    output += "\"page\" : [{\"url\" : \"" + EncodeJsonText(urls[i].Address) + "\", ";
                    output += "\"http-status\" : \"" + urls[i].Status.ToString() + "\", ";
                    if(urls[i].HtmlPageTitle != null)
                        output += "\"title\" : \"" + EncodeJsonText(urls[i].HtmlPageTitle) + "\", ";
                    //ISODuration duration = new ISODuration(0, 0, 0, 0, 0, urls[i].LoadTime.Minutes, urls[i].LoadTime.Seconds);
                    output += "\"parse-time\" : \"" + EncodeJsonText(urls[i].LoadTime.Milliseconds.ToString()) + "\"";

                    if (i != urls.Count - 1)
                        output += "}, ";
                    else
                        output += "}] ";
                }
                output += "}";

            }
            return output;
        }


        private string AddUfErrors()
        {
            string output = string.Empty;
            if (errors != null)
            {
                if (errors.Count != 0)
                {
                    if (tree.Nodes.Count > 0)
                        output += ", \"UfErrors\": [";
                    else
                        output += "\"UfErrors\": [";

                    for (int i = 0; i < errors.Count; i++)
                    {
                        output += "{\"msg\" : \"" + EncodeJsonText(errors[i].Message) + "\", ";
                        if (errors[i].Status != 0)
                        {
                            output += "\"url\" : \"" + EncodeJsonText(errors[i].Address) + "\", ";
                            output += "\"status\" : \"" + errors[i].Status.ToString() + "\"";
                        }
                        else
                        {
                            output += "\"url\" : \"" + EncodeJsonText(errors[i].Address) + "\"";
                        }

                        if (i != errors.Count - 1)
                            output += "}, ";
                        else
                            output += "} ";
                    }
                    output += "]";
                }
            }

            return output;
        }




        private void AddChildNodes(UfDataNode xNode, UfDataNode node, UfElementDescriber ufElement)
        {
            //if (!string.IsNullOrEmpty(node.ElementId))
            //{
            //    xNode.Nodes.Add("id", node.ElementId);
            //}
            

            if (ufElement.AttributeValues.Count > 0)
            {
                // If its a rel or rev uf based on attribute values, just copy it
                // UfDataNode xNodeChild = xNode.Nodes.Append(node.Name, node.Value);
                foreach (UfDataNode childNode in node.Nodes)
                    xNode.Nodes.Add(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                
            }

            foreach (UfElementDescriber childElement in ufElement.Elements)
            {
                // Loop orginal data tree
                foreach (UfDataNode childNode in node.Nodes)
                {
                    // If node name = element describer name
                    if (childNode.Name == childElement.Name || childNode.Name == childElement.CompoundName)
                    {
                        // If element can have multiples call the AppendArrayList method
                        if (childElement.Multiples)
                        {
                            UfDataNode xChild = xNode.Nodes.Append(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                            AddChildNodes(xChild, childNode, childElement);
                        }
                        else
                        {
                            int index = xNode.Nodes.Add(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                            UfDataNode xChild = xNode.Nodes[index];
                            AddChildNodes(xChild, childNode, childElement);
                        }
                    }
                }
            }
        }


        private string BuildDataString(UfDataNode node, bool isArrayItem, bool isLastNode)
        {
            string output = string.Empty;

            // Trim just in case
            node.Name = node.Name.Trim();
            node.Value = node.Value.Trim();
            node.SourceUrl = node.SourceUrl.Trim();

            ////// Add source here
            //if (node.SourceUrl != string.Empty)
            //    output += "\"sourceurl\": \"" + EncodeJsonText(node.SourceUrl) + "\"";


            if (node.Value == "Array")
            {
                output += "\"" + node.Name + "\": [";
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    if (i == node.Nodes.Count-1)
                        output += BuildDataString(node.Nodes[i], true, true);
                    else
                        output += BuildDataString(node.Nodes[i], true, false);
                }

                if (isLastNode)
                    output += "]";
                else
                    output += "], ";
            }


            if (node.Value != "Array" && node.Nodes.Count > 0)
            {
               
                if (!IsNumeric(node.Name))
                    output += "\"" + node.Name + "\": {";
                else
                    output += "{";

                if (node.RepresentativeNode)
                    output += "\"representativehcard\": \"true\", ";

                // Add source here
                if (node.SourceUrl != string.Empty)
                    output += "\"sourceurl\": \"" + EncodeJsonText(node.SourceUrl) + "\", ";

                for (int i = 0; i < node.Nodes.Count; i++)
                    if (i == node.Nodes.Count-1)
                        output += BuildDataString(node.Nodes[i], false, true);
                    else
                        output += BuildDataString(node.Nodes[i], false, false);
                
                if (isLastNode)
                    output += "}";
                else
                    output += "}, ";

            }


            if (node.Value != "Array" && node.Nodes.Count == 0)
            {
                // Add source here
                if (node.SourceUrl != string.Empty)
                    output += "\"sourceurl\": \"" + EncodeJsonText(node.SourceUrl) + "\", ";

                if (node.Value != string.Empty)
                {
                    if (isArrayItem)
                        output += "\"" + EncodeJsonText(node.Value) + "\"";
                    else
                        output += "\"" + node.Name + "\": \"" + EncodeJsonText(node.Value) + "\"";

                    if (isLastNode)
                        output += " ";
                    else
                        output += ", ";
                }

                for (int i = 0; i < node.Nodes.Count; i++)
                    if (i == node.Nodes.Count-1)
                        output += BuildDataString(node.Nodes[i], false, true);
                    else
                        output += BuildDataString(node.Nodes[i], false, false);
            }
                
            return output;
        }



        /// <summary>
        /// Encodes text so that it can be used in Json
        /// </summary>
        public string EncodeJsonText(string text)
        {
            if (text != null)
            {
                text = text.Replace("/", "\\/"); // Escape char \
                return text.Replace("\"", "\\\""); // Containing char "
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// Is the string a number
        /// </summary>
        public bool IsNumeric(string numberString)
        {
            char[] ca = numberString.ToCharArray();
            for (int i = 0; i < ca.Length; i++)
            {
                if (!char.IsNumber(ca[i]))
                    return false;
            }
            return true;
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
