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
    /// Describers the use of HTML element (tag), as part of microformat format description
    /// </summary>
    [XmlRoot("UfElementDescriber")]
    public class UfElementDescriber
    {
  

        private string name = string.Empty;
        private string attribute = "class";
        private string compoundName = string.Empty;
        private string compoundAttribute = "class";
        private string description = string.Empty;
        private bool mandatory = false;
        private bool multiples = false;
        private bool concatenateValues = false;
        private bool rootElement = false;
        private ArrayList allowedTags = new ArrayList();
        private PropertyTypes type = PropertyTypes.UrlTextAttribute;
        private StructureTypes structureTypes = StructureTypes.NonStructural; 
        private UfElementDescribers elements = new UfElementDescribers();
        private uFAttributeValueDescribers attributeValues = new uFAttributeValueDescribers();

        /// <summary>
        /// Describers the use of HTML element (tag), as part of microformat format description
        /// </summary>
        public UfElementDescriber() { }


        /// <summary>
        /// Describers the use of HTML element (tag), as part of microformat format description
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="mandatory">Is mandatory</param>
        /// <param name="multiples">Allow multiples</param>
        public UfElementDescriber(string name, bool mandatory, bool multiples)
        {
            this.Name = name;
            this.Mandatory = mandatory;
            this.Multiples = multiples;
            this.Type = PropertyTypes.Text;
        }


        /// <summary>
        /// Describers the use of HTML element (tag), as part of microformat format description
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="mandatory">Is mandatory</param>
        /// <param name="multiples">Allow multiples</param>
        /// <param name="type">Property type</param>
        public UfElementDescriber(string name, bool mandatory, bool multiples, PropertyTypes type)
        {
            this.Name = name;
            this.Mandatory = mandatory;
            this.Multiples = multiples;
            this.Type = type;
        }


        /// <summary>
        /// Describers the use of HTML element (tag), as part of microformat format description
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="mandatory">Is mandatory</param>
        /// <param name="multiples">Allow multiples</param>
        /// <param name="concatenateValues">Concatenate multiple text values together</param>
        /// <param name="type">Property type</param>
        public UfElementDescriber(string name, bool mandatory, bool multiples, bool concatenateValues, PropertyTypes type)
        {
            this.Name = name;
            this.Mandatory = mandatory;
            this.Multiples = multiples;
            this.ConcatenateValues = concatenateValues;
            this.Type = type;
        }


        /// <summary>
        /// Describers the use of HTML element (tag), as part of microformat format description
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="mandatory">Is mandatory</param>
        /// <param name="multiples">Allow multiples</param>
        /// <param name="typevalues">Comma delimited list of allow values</param>
        public UfElementDescriber(string name, bool mandatory, bool multiples, string typevalues)
        {
            this.Name = name;
            this.Mandatory = mandatory;
            this.Multiples = multiples;
            this.Type = PropertyTypes.Type;

            // **** Todo: Need to add typevalues support **** //
        }

        /// <summary>
        /// Describers the use of HTML element (tag), as part of microformat format description
        /// </summary>
        /// <param name="name">Element name</param>
        /// <param name="mandatory">Is mandatory</param>
        /// <param name="multiples">Allow multiples</param>
        /// <param name="concatenateValues">Concatenate multiple text values together</param>
        /// <param name="typevalues">Comma delimited list of allow values</param>
        public UfElementDescriber(string name, bool mandatory, bool multiples, bool concatenateValues, string typevalues)
        {
            this.Name = name;
            this.Mandatory = mandatory;
            this.Multiples = multiples;
            this.ConcatenateValues = concatenateValues;
            this.Type = PropertyTypes.Type;

            // **** Todo: Need to add typevalues support **** //
        }


        /// <summary>
        /// The name of the microformats format
        /// </summary>
        [XmlAttribute("name")] 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The compound name this is used to build compound structrues ie vcard and reviewer in hreview
        /// </summary>
        [XmlElement("compound-name")]
        public string CompoundName
        {
            get { return compoundName; }
            set { compoundName = value; }
        }

        /// <summary>
        /// The compound attribute this is used to build compound structrues ie vcard and reviewer in hreview
        /// </summary>
        [XmlElement("compound-attribute")]
        public string CompoundAttribute
        {
            get { return compoundAttribute; }
            set { compoundAttribute = value; }
        }

        /// <summary>
        /// The description of the microformats format
        /// </summary>
        [XmlIgnoreAttribute] 
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// The element attribute to search on ie class, rel or rev
        /// </summary>
        [XmlElement("attribute")]
        public string Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        /// <summary>
        /// Is the element mandatory
        /// </summary>
        [XmlElement("mandatory")]
        public bool Mandatory
        {
            get { return mandatory; }
            set { mandatory = value; }
        }

        /// <summary>
        /// Allow multiple instances of element
        /// Sets concatenate values flag to false
        /// </summary>
        [XmlElement("multiples")]
        public bool Multiples
        {
            get { return multiples; }
            set 
            {
                if (value == true)
                    this.ConcatenateValues = false;
                multiples = value; 
            }
        }

        /// <summary>
        /// Marks the top most element in a format description
        /// </summary>
        [XmlElement("root-element")]
        public bool RootElement
        {
            get { return rootElement; }
            set {rootElement = value; }
        }

        /// <summary>
        /// Allow multiple instances to be concatenate in to a single value.
        /// Sets multiples flag to false
        /// </summary>
        [XmlElement("concatenate-values")]
        public bool ConcatenateValues
        {
            get { return concatenateValues; }
            set 
            {
                if (value == true)
                    this.Multiples = false;
                concatenateValues = value; 
            }
        }

        /// <summary>
        /// The type of properties to return
        /// </summary>
        [XmlElement("type")]
        public PropertyTypes Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// The type node 
        /// </summary>
        [XmlElement("node-type")]
        public StructureTypes NodeType
        {
            get { return structureTypes; }
            set { structureTypes = value; }
        }

        /// <summary>
        /// A list of Html tags to restrict the parse to. If empty it will use all tags
        /// </summary>
        [XmlElement("allowed-tags")]
        public ArrayList AllowedTags
        {
            get { return allowedTags; }
            set { allowedTags = value; }
        }


        /// <summary>
        /// The child elements of this element
        /// </summary>
        [XmlElement("elements")]
        public UfElementDescribers Elements
        {
            get { return elements; }
            set { elements = value; }
        }


        /// <summary>
        /// The child attribute values of this element
        /// </summary>
        [XmlElement("attribute-values")]
        public uFAttributeValueDescribers AttributeValues
        {
            get { return attributeValues; }
            set { attributeValues = value; }
        }

        public enum PropertyTypes
        {
            None,
            Text,
            FormattedText,
            Url,
            Uid,
            Date,
            Email,
            Image,
            Type,
            UrlText,
            UrlTextTag,
            UrlTextAttribute,
            Number
        }


        public enum StructureTypes
        {
            StartPoint,
            Structure,
            NonStructural,
            TypeValuePair
        }

    }


    /// <summary>
    /// An elements collection.
    /// </summary>
    public class UfElementDescribers : CollectionBase  //: IList
    {

        public UfElementDescribers() : base() { }

        public void Sort(string propertyName, string direction)
        {
            InnerList.Sort(new GenericSort(propertyName, direction));
        }


        public void Insert(int index, UfElementDescriber newElement)
        {
            InnerList.Insert(index, newElement);
        }

        public void Remove(UfElementDescriber aElement)
        {
            InnerList.Remove(aElement);
        }

        public bool Contains(UfElementDescriber aElement)
        {
            return InnerList.Contains(aElement);
        }

        public int IndexOf(UfElementDescriber aElement)
        {
            return InnerList.IndexOf(aElement);
        }

        public int Add(UfElementDescriber newElement)
        {
            return InnerList.Add(newElement);
        }

        public UfElementDescriber this[int index]
        {
            get
            {
                if (this.InnerList.Count > index)
                {
                    return (UfElementDescriber)this.InnerList[index];
                }
                else
                {
                    throw new ArgumentException("Out of range");
                }
            }
            set
            {
                if (this.InnerList.Count > index)
                {
                    this.InnerList[index] = value;
                }
                else
                {
                    throw new ArgumentException("Out of range");
                }
            }
        }

        public UfElementDescriber this[string name]
        {
            get
            {
                UfElementDescriber elementDescriber = null;
                for (int i = 0; i < this.InnerList.Count; i++)
                {
                    UfElementDescriber testNode = (UfElementDescriber)this.InnerList[i];
                    if (testNode.Name == name)
                        return testNode;
                }
                return elementDescriber;
            }
        }




    }
}
