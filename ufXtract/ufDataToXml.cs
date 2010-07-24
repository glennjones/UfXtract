using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;


namespace ufXtract
{
    public class ufDataToXml
    {
        //Copyright (c) 2007 Glenn Jones

        private Urls m_oUrls = null;
        private Errors m_oErrors = null;
        private bool m_bReporting = false;
        private bool m_bMultiples = false;


        public ufDataToXml()
		{
		}

        public void Convert(ufDateNode node, Stream stream, bool multiples, Urls urls, Errors errors, bool reporting)
        {
            m_bReporting = reporting;
            m_oErrors = errors;
            m_oUrls = urls;
            Convert(node, stream, multiples);
        }

        public void Convert(ufDateNode node, Stream stream, bool multiples, Urls urls, Errors errors)
        {
            m_oErrors = errors;
            m_oUrls = urls;
            Convert(node, stream, multiples);
        }

        public void Convert(ufDateNode node, Stream stream, bool multiples, Urls urls)
        {
            m_oUrls = urls;
            Convert(node, stream, multiples);
        }
   

        /// <summary>
        /// Provides string version of data object
        /// </summary>
        public void Convert(ufDateNode node, Stream stream, bool multiples)
        {
            string output = string.Empty;
            m_bMultiples = multiples;
            //StringWriter stringwriter = new StringWriter();

            XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
            writer.Formatting = System.Xml.Formatting.Indented;

            writer.WriteStartDocument(true);
            writer.WriteStartElement("ufxtract");

            foreach (ufDateNode child in node.Nodes)
            {
                AddNode(child, writer);
            }


            //Find errors
            // -------------------------------------------------
            if (m_oErrors != null)
            {
                if (m_oErrors.Count != 0)
                {
                    writer.WriteStartElement("errors");
                    if (m_oErrors != null)
                    {
                        foreach (Error error in m_oErrors)
                        {
                            writer.WriteStartElement("error");
                            writer.WriteElementString("msg", error.Message);
                            writer.WriteStartElement("url");
                            if (error.Status != 0)
                                writer.WriteAttributeString("status", error.Status.ToString());
                            writer.WriteString(error.Address);
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
            }


            
            // Write report if a Urls object is provided
            // -------------------------------------------------
            if (m_bReporting && m_oUrls != null)
            {
                writer.WriteStartElement("report");
                foreach (Url url in m_oUrls)
                {
                    writer.WriteStartElement("url");
                    writer.WriteAttributeString("status", url.Status.ToString());
                    writer.WriteAttributeString("millisec", url.LoadTime.Milliseconds.ToString());
                    writer.WriteString(url.Address);
                    writer.WriteEndElement();
                }
                writer.WriteElementString("found", node.Nodes.Count.ToString());
                writer.WriteEndElement();
            }
            

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
        }


        private void AddNode(ufDateNode node, XmlTextWriter writer)
        {
            if (node.Name != string.Empty)
            {
                writer.WriteStartElement(node.Name);
                if (node.SourceUrl != string.Empty)
                    writer.WriteAttributeString("sourceurl", node.SourceUrl);
                if (node.RepresentativeNode)
                    writer.WriteAttributeString("representativehcard", "true");
                writer.WriteString(node.Value);
            }
            foreach (ufDateNode child in node.Nodes)
            {
                AddNode(child, writer);
            }
            if (node.Name != string.Empty)
                writer.WriteEndElement();
        }



     
        


    }
}
