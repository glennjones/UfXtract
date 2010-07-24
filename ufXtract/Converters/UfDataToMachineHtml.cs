//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Web.UI;

using UfXtract.Utilities;


namespace UfXtract
{
    /// <summary>
    /// Converts a UfDataNode structure into a very basic form HTML.
    /// </summary>
    public class UfDataToMachineHtml
    {


        private int indentNum = 0;

        /// <summary>
        /// Converts a UfDataNode structure into a very basic form HTML.
        /// </summary>
        public UfDataToMachineHtml(){}


        
        /// <summary>
        /// Converts a UfDataNode structure into a very basic form HTML.
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="formatDescriber">Microformat format describer object</param>
        /// <returns>HTML string</returns>
        public string Convert(UfDataNode node, UfFormatDescriber formatDescriber)
        {
            string output = string.Empty;

            StringWriter stringWriter = new StringWriter();
            UfElementDescriber elementDescriber = formatDescriber.BaseElement;

            using (XhtmlTextWriter writer = new XhtmlTextWriter(stringWriter))
            {
                writer.WriteBeginTag("div");
                writer.WriteAttribute("class", "microformats");
                writer.Write(HtmlTextWriter.TagRightChar);

                foreach (UfDataNode child in node.Nodes)
                {
                    writer.WriteLine();
                    AddNode(child, elementDescriber, writer);
                }
              
                writer.WriteEndTag("div");
                writer.WriteLine();
            }

            return stringWriter.ToString();
        }



        private void AddNode(UfDataNode node, UfElementDescriber elementDescriber, XhtmlTextWriter writer)
        {

            if (node.Name != string.Empty)
            {
                indentNum++;
                writer.Indent = indentNum;

                UfElementDescriber currentDescriber = elementDescriber;
                foreach (UfElementDescriber childElementDescriber in elementDescriber.Elements)
                {
                    if (node.Name == childElementDescriber.Name || node.Name == childElementDescriber.CompoundName)
                    {
                        currentDescriber = childElementDescriber;
                    }
                }

                if (currentDescriber.Attribute == "class")
                {

                    writer.WriteBeginTag("div");
                    if (currentDescriber.CompoundName == "")
                        writer.WriteAttribute("class", node.Name);
                    else
                        writer.WriteAttribute("class", node.Name + " " + currentDescriber.Name);

                    if (!string.IsNullOrEmpty(node.ElementId))
                        writer.WriteAttribute("id", node.ElementId);

                    writer.Write(HtmlTextWriter.TagRightChar);
                    writer.WriteEncodedText(node.Value);

                    
                    foreach (UfDataNode child in node.Nodes)
                    {
                        writer.WriteLine();
                        AddNode(child, currentDescriber, writer);
                    }


                    if (node.Name != string.Empty)
                    {
                        writer.WriteEndTag("div");
                        writer.WriteLine();
                    }
                }

                if (currentDescriber.Attribute == "rel")
                {
                    writer.WriteBeginTag("a");
                    writer.WriteAttribute("href", node.DescendantValue("link"));
                    writer.WriteAttribute("rel", node.Name);
                    writer.Write(HtmlTextWriter.TagRightChar);

                    writer.WriteEncodedText(node.DescendantValue("text"));
                    writer.WriteEndTag("a");
                    writer.WriteLine();
                }

                indentNum--;
                writer.Indent = indentNum;

            }
        }



    }
}
