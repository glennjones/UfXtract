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
    /// Describers the use of HTML attribute, as part of microformat format description
    /// </summary>
    [XmlRoot("uFAttributeValueDescriber")]
    public class UfAttributeValueDescriber
    {

        private string name = string.Empty;
        private string description = string.Empty;
        private bool mandatory = false;
        private bool m_bMultiples = false;
        private ArrayList excludeValues = new ArrayList();


        /// <summary>
        /// Describers the use of HTML attribute, as part of microformat format description
        /// </summary>
        public UfAttributeValueDescriber() { }


        /// <summary>
        /// Describers the use of HTML attribute, as part of microformat format description
        /// </summary>
        /// <param name="name">Attribute name</param>
        public UfAttributeValueDescriber(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Describers the use of HTML attribute, as part of microformat format description
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="excludeValues">Space delimited list of excluded attribute values</param>
        public UfAttributeValueDescriber(string name, string excludeValues)
        {
            this.Name = name;
            if (excludeValues != string.Empty)
            {
                if (excludeValues.IndexOf(" ") > 0)
                {
                    string[] arrayValues = excludeValues.Split(' ');
                    for (int i = 0; i < arrayValues.Length; i++)
                    {
                        this.excludeValues.Add(arrayValues[i].Trim());
                    }
                }
                else
                {
                    this.excludeValues.Add(excludeValues);
                }
            }
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
        /// The description of the microformats format
        /// </summary>
        [XmlIgnoreAttribute] 
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Is the attribute value mandatory
        /// </summary>
        [XmlElement("mandatory")] 
        public bool Mandatory
        {
            get { return mandatory; }
            set { mandatory = value; }
        }

        /// <summary>
        /// Allow multiple instances of attribute value
        /// </summary>
        [XmlElement("multiples")] 
        public bool Multiples
        {
            get { return m_bMultiples; }
            set { m_bMultiples = value; }
        }

        /// <summary>
        /// Array of excludes the values
        /// </summary>
        [XmlElement("excludes-values")]
        public ArrayList ExcludeValues
        {
            get { return excludeValues; }
            set { excludeValues = value; }
        }





    }



    /// <summary>
    /// An elements collection.
    /// </summary>
    public class uFAttributeValueDescribers : CollectionBase  //: IList
    {

        public uFAttributeValueDescribers() : base() { }

        public void Sort(string propertyName, string direction)
        {
            InnerList.Sort(new GenericSort(propertyName, direction));
        }


        public void Insert(int index, UfAttributeValueDescriber newAttributeValue)
        {
            InnerList.Insert(index, newAttributeValue);
        }

        public void Remove(UfAttributeValueDescriber aAttributeValue)
        {
            InnerList.Remove(aAttributeValue);
        }

        public bool Contains(UfAttributeValueDescriber aAttributeValue)
        {
            return InnerList.Contains(aAttributeValue);
        }

        public int IndexOf(UfAttributeValueDescriber aAttributeValue)
        {
            return InnerList.IndexOf(aAttributeValue);
        }

        public int Add(UfAttributeValueDescriber newAttributeValue)
        {
            return InnerList.Add(newAttributeValue);
        }

        public UfAttributeValueDescriber this[int index]
        {
            get
            {
                if (this.InnerList.Count > index)
                {
                    return (UfAttributeValueDescriber)this.InnerList[index];
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


    }
}
