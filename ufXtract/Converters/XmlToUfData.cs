//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UfXtract.Utilities;

namespace UfXtract
{
    /// <summary>
    /// Converts a UfXtract XML structure into UfDataNode structure
    /// </summary>
    public class XmlToUfData
    {

        /// <summary>
        /// Converts a UfXtract JSON structure into UfDataNode structure
        /// </summary>
        public XmlToUfData(){}


        /// <summary>
        /// Converts a UfXtract JSON structure into UfDataNode structure
        /// </summary>
        /// <param name="xml">XML string</param>
        /// <returns>UfDataNode</returns>
        public UfDataNode Convert(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return this.Convert(xmlDocument);
        }


        /// <summary>
        /// Converts a UfXtract JSON structure into UfDataNode structure
        /// </summary>
        /// <param name="xmlDocument">XmlDocument</param>
        /// <returns>UfDataNode</returns>
        public UfDataNode Convert(XmlDocument xmlDocument)
        {
            UfDataNode node = new UfDataNode();
            foreach(XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
                CreateNode(node, xmlNode);
            
            return node;
        }


        private void CreateNode(UfDataNode node, XmlNode xmlNode)
        {
     
            UfDataNode newNode = new UfDataNode();
            newNode.Name = xmlNode.Name;

            // Value from value
            if (!string.IsNullOrEmpty(xmlNode.Value))
                newNode.Value = xmlNode.Value;
           
            if (xmlNode.NodeType == XmlNodeType.Text)
                node.Value = xmlNode.InnerText;

            if (xmlNode.Attributes != null)
            {
                if (xmlNode.Attributes["id"] != null)
                {
                    XmlAttribute idAtt = xmlNode.Attributes["id"];
                    newNode.ElementId = idAtt.Value;
                }
            }

            foreach (XmlNode childXmlNode in xmlNode.ChildNodes)
                CreateNode(newNode, childXmlNode);

            if (xmlNode.NodeType != XmlNodeType.Text)
                node.Nodes.Add(newNode);
            
            
        }

    }
}
