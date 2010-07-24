//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace UfXtract
{

    /// <summary>
    /// Microformats format description
    /// </summary>
    [XmlRoot("UfFormatDescriber")]
    public class UfFormatDescriber
    {

 

        private string name = string.Empty;
        private string description = string.Empty;
        private FormatTypes type = FormatTypes.Elemental;
        private UfElementDescriber baseElement = new UfElementDescriber();

        /// <summary>
        /// Microformats format description
        /// </summary>
        public UfFormatDescriber() { }

        

        /// <summary>
        /// The name of the microformats formet
        /// </summary>
        [XmlAttribute("name")] 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The description of the microformats formet
        /// </summary>
        [XmlIgnoreAttribute] 
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// The base element
        /// </summary> 
        [XmlElement("base-element")]
        public UfElementDescriber BaseElement
        {
            get { return baseElement; }
            set { baseElement = value; }
        }

        /// <summary>
        /// The type of microformats format
        /// </summary>
        [XmlElement("type")]
        public FormatTypes Type
        {
            get { return type; }
            set { type = value; }
        }


        /// <summary>
        /// Microformats format type
        /// </summary>
        public enum FormatTypes
        {
            Elemental,
            Compound
        }
    }
   
}
