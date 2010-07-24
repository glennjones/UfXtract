using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ufXtract
{
    public class ufDataToJson
    {
        //Copyright (c) 2007 Glenn Jones

        private ufDateNode xTree = new ufDateNode();
        private string m_sCallBack = string.Empty;
        private bool m_bReporting = false;
        private bool m_bMultiplesFormats = false;
        private Urls m_oUrls = null;
        private Errors m_oErrors = null;


        public ufDataToJson()
		{
		}




        public string Convert(ufDateNode node, 
            ufFormatDescriber ufFormat, 
            bool multiplesFormats, 
            string callBack)
        {
            m_sCallBack = callBack;
            m_sCallBack = m_sCallBack.Replace("(", "").Replace(")", "").Trim();
            return Convert(node, ufFormat, multiplesFormats);
        }

        public string Convert(ufDateNode node, 
            ufFormatDescriber ufFormat, 
            bool multiplesFormats, 
            string callBack, 
            Urls urls)
        {
            m_sCallBack = callBack;
            m_sCallBack = m_sCallBack.Replace("(", "").Replace(")", "").Trim();
            m_oUrls = urls;
            return Convert(node, ufFormat, multiplesFormats);
        }

        public string Convert(ufDateNode node, 
            ufFormatDescriber ufFormat, 
            bool multiplesFormats,
            string callBack, 
            Urls urls, 
            Errors errors)
        {
            m_oErrors = errors;
            m_sCallBack = callBack;
            m_sCallBack = m_sCallBack.Replace("(", "").Replace(")", "").Trim();
            m_oUrls = urls;
            return Convert(node, ufFormat, multiplesFormats);
        }

        public string Convert(ufDateNode node, 
            ufFormatDescriber ufFormat, 
            bool multiplesFormats, 
            string callBack, 
            Urls urls, 
            Errors errors, 
            bool reporting)
        {
            m_bReporting = reporting;
            m_oErrors = errors;
            m_sCallBack = callBack;
            m_sCallBack = m_sCallBack.Replace("(", "").Replace(")", "").Trim();
            m_oUrls = urls;
            return Convert(node, ufFormat, multiplesFormats);
        }

        public string Convert(ufDateNode node, 
            ufFormatDescriber ufFormat, 
            bool multiplesFormats, 
            Urls urls)
        {
            m_oUrls = urls;
            return Convert(node, ufFormat, multiplesFormats);
        }


        /// <summary>
        /// Takes multiple formatting objects - hAtom
        /// </summary>
        /// <param name="node"></param>
        /// <param name="formatArray"></param>
        /// <param name="multiplesFormats"></param>
        /// <param name="callBack"></param>
        /// <param name="urls"></param>
        /// <param name="errors"></param>
        /// <param name="reporting"></param>
        /// <returns></returns>
        public string Convert(ufDateNode node,
         ArrayList formatArray,
         bool multiplesFormats,
         string callBack,
         Urls urls,
         Errors errors,
         bool reporting)
        {
            
            m_sCallBack = callBack;
            m_sCallBack = m_sCallBack.Replace("(", "").Replace(")", "").Trim();
            m_oUrls = urls;
            m_oErrors = errors;
            m_bReporting = reporting;

            foreach (ufFormatDescriber ufFormat in formatArray)
            {
                foreach (ufDateNode childNode in node.Nodes)
                {
                    foreach (ufDateNode grandChildNode in childNode.Nodes)
                    {
                        if (grandChildNode.Name == ufFormat.BaseElement.Name)
                        {
                            ufDateNode xChild = xTree.Nodes.Append(grandChildNode.Name, grandChildNode.Value, grandChildNode.SourceUrl, grandChildNode.RepresentativeNode);
                            if (grandChildNode.Nodes.Count > 0)
                                AddChildNodes(xChild, grandChildNode, ufFormat.BaseElement);

                        }
                    }
                }
            }

            string output = "// ufXtract \n";
            if (m_sCallBack != string.Empty)
                output += m_sCallBack + "( ";

            output += "{";

            foreach (ufDateNode childNode in xTree.Nodes)
                output += BuildDataString(childNode, true, false);

            if (xTree.Nodes.Count > 0)
                output = output.Substring(0, output.Length - 2);

            output += AddErrors();
            output += AddReporting(node);

            // End whole block
            output += "}";

            if (m_sCallBack != string.Empty)
                output += " )";

            //return output.Replace(",", ",\n").Replace("}", "}\n").Replace("{", "{\n").Replace("]", "]\n").Replace("[", "[\n"); ;
            return output;

        }







        public string Convert(ufDateNode node, ufFormatDescriber ufFormat, bool multiplesFormats)
        {
            m_bMultiplesFormats = multiplesFormats;
            foreach (ufDateNode childNode in node.Nodes)
            {
                if (childNode.Name == ufFormat.BaseElement.Name)
                {
                    ufDateNode xChild = xTree.Nodes.Append(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                    if (childNode.Nodes.Count > 0)
                        AddChildNodes(xChild, childNode, ufFormat.BaseElement);
                    
                }
            }

            string output = "// ufXtract \n";
            if( m_sCallBack != string.Empty)
                output += m_sCallBack + "( ";

            output += "{";

            foreach (ufDateNode childNode in xTree.Nodes)
                output += BuildDataString(childNode, true, false);

            if (xTree.Nodes.Count > 0)
                output = output.Substring(0, output.Length - 2);

            output += AddErrors();
            output += AddReporting( node );

            // End whole block
            output += "}";
         
            if (m_sCallBack != string.Empty)
                output += " )";

            return  output; 

        }



        private string AddReporting( ufDateNode node )
        {
            string output = string.Empty;
            if (m_bReporting && m_oUrls != null)
            {

                if (xTree.Nodes.Count > 0 || m_oErrors.Count > 0)
                    output += ", \"report\" : [";
                else
                    output += "\"report\" : [";

                for (int i = 0; i < m_oUrls.Count; i++)
                {
                    output += "{\"url\" : \"" + EncodeJsonText(m_oUrls[i].Address) + "\", ";
                    output += "\"status\" : \"" + m_oUrls[i].Status.ToString() + "\", ";
                    output += "\"millisec\" : \"" + EncodeJsonText(m_oUrls[i].LoadTime.Milliseconds.ToString()) + "\"";

                    if (i != m_oUrls.Count - 1)
                        output += "}, ";
                    else
                        output += "} ";
                }
                output += "]";

                //output += ", \"found\" : \"" + node.Nodes.Count.ToString() + "\"";
            }

            return output;
        }


        private string AddErrors()
        {
            string output = string.Empty;
            if (m_oErrors != null)
            {
                if (m_oErrors.Count != 0)
                {
                    if (xTree.Nodes.Count > 0)
                        output += ", \"errors\": [";
                    else
                        output += "\"errors\": [";

                    for (int i = 0; i < m_oErrors.Count; i++)
                    {
                        output += "{\"msg\" : \"" + EncodeJsonText(m_oErrors[i].Message) + "\", ";
                        if (m_oErrors[i].Status != 0)
                        {
                            output += "\"url\" : \"" + EncodeJsonText(m_oErrors[i].Address) + "\", ";
                            output += "\"status\" : \"" + m_oErrors[i].Status.ToString() + "\"";
                        }
                        else
                        {
                            output += "\"url\" : \"" + EncodeJsonText(m_oErrors[i].Address) + "\"";
                        }

                        if (i != m_oErrors.Count - 1)
                            output += "}, ";
                        else
                            output += "} ";
                    }
                    output += "]";
                }
            }

            return output;
        }









        private void AddChildNodes(ufDateNode xNode, ufDateNode node, uFElementDescriber ufElement)
        {
            if (ufElement.AttributeValues.Count > 0)
            {
                // If its a rel or rev uf based on attribute values, just copy it
                //ufDateNode xNodeChild = xNode.Nodes.Append(node.Name, node.Value);
                foreach (ufDateNode childNode in node.Nodes)
                    xNode.Nodes.Add(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                
            }

            foreach (uFElementDescriber childElement in ufElement.Elements)
            {
                // Loop orginal data tree
                foreach (ufDateNode childNode in node.Nodes)
                {
                    // If node name = element describer name
                    if (childNode.Name == childElement.Name)
                    {
                        // If element can have multiples call the AppendArrayList method
                        if (childElement.Multiples)
                        {
                            ufDateNode xChild = xNode.Nodes.Append(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                            AddChildNodes(xChild, childNode, childElement);
                        }
                        else
                        {
                            int index = xNode.Nodes.Add(childNode.Name, childNode.Value, childNode.SourceUrl, childNode.RepresentativeNode);
                            ufDateNode xChild = xNode.Nodes[index];
                            AddChildNodes(xChild, childNode, childElement);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Build a string from data
        /// </summary>
        private string BuildDataString(ufDateNode node, bool isArrayItem, bool isLastNode)
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
          text = text.Replace("/", "\\/"); // Escape char \
          return text.Replace("\"", "\\\""); // Containing char "
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



    }
}
