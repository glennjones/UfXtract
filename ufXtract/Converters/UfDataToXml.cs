//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using UfXtract.Utilities;


namespace UfXtract
{
    /// <summary>
    /// Converts a UfDataNode structure into XML
    /// </summary>
    public class UfDataToXml
    {
        //Copyright (c) 2007 Glenn Jones

        private Urls urls = new Urls();
        private UfErrors errors = new UfErrors();



        /// <summary>
        /// Converts a UfDataNode structure into XML
        /// </summary>
        public UfDataToXml(){}



        /// <summary>
        /// Converts a UfDataNode structure into XML
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="stream">Stream XML is added to</param>
        public void Convert(UfDataNode node, Stream stream)
        {
            XmlConformWriter writer = new XmlConformWriter(stream, Encoding.UTF8);
            ConvertIt(node, writer);
        }


        /// <summary>
        /// Converts a UfDataNode structure into XML
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="Writer">Text writer</param>
        public void Convert(UfDataNode node, TextWriter writer)
        {
            XmlConformWriter xmlWriter = new XmlConformWriter(writer);
            ConvertIt(node, xmlWriter);
        }




 
        private void ConvertIt(UfDataNode node, XmlTextWriter writer)
        {
            string output = string.Empty;

            writer.Formatting = System.Xml.Formatting.Indented;

            writer.WriteStartDocument(true);
            writer.WriteStartElement("microformats");

            foreach (UfDataNode child in node.Nodes)
            {
                AddNode(child, writer);
            }


            //Find errors
            // -------------------------------------------------
            if (errors != null)
            {
                if (errors.Count != 0)
                {
                    writer.WriteStartElement("errors");
                    if (errors != null)
                    {
                        foreach (UfError UfError in errors)
                        {
                            writer.WriteStartElement("UfError");
                            writer.WriteElementString("msg", UfError.Message);
                            writer.WriteStartElement("url");
                            if (UfError.Status != 0)
                                writer.WriteAttributeString("status", UfError.Status.ToString());
                            writer.WriteString(UfError.Address);
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
            }


            if (urls.Count > 0)
            {

                // Write parser information
                writer.WriteStartElement("parser-information");
                writer.WriteElementString("name", "UfXtract");
                writer.WriteElementString("version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                if (urls != null)
                {
                    foreach (Url url in urls)
                    {
                        writer.WriteStartElement("page");
                        writer.WriteElementString("url", url.Address);
                        writer.WriteElementString("http-status", url.Status.ToString());
                        writer.WriteElementString("title", url.HtmlPageTitle);
                        writer.WriteElementString("parse-time", url.LoadTime.Milliseconds.ToString());
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
            
            

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
        }


        private void AddNode(UfDataNode node, XmlTextWriter writer)
        {
            if (node.Name != string.Empty)
            {
                writer.WriteStartElement(node.Name);
                if (!string.IsNullOrEmpty(node.SourceUrl))
                    writer.WriteAttributeString("sourceurl", node.SourceUrl);

                if (node.RepresentativeNode)
                    writer.WriteAttributeString("representativehcard", "true");

                if (!string.IsNullOrEmpty(node.ElementId))
                    writer.WriteAttributeString("id", node.ElementId);

                writer.WriteString(node.Value);

            }
            foreach (UfDataNode child in node.Nodes)
            {
                AddNode(child, writer);
            }
            if (node.Name != string.Empty)
                writer.WriteEndElement();
        }


        /// <summary>
        /// Collection of error information for reporting
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
