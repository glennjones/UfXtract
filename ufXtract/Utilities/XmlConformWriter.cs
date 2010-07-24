//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections;

namespace UfXtract.Utilities
{
    /// <summary>
    /// Derived from XmlTextWriter class. Overrides the WriteString and WriteStartElement methods.
    /// </summary>
    public class XmlConformWriter : XmlTextWriter
    {
        internal void CheckUnicodeString(String value)
        {
            for (int i = 0; i < value.Length; ++i)
            {
                if (value[i] > 0xFFFD)
                {
                    throw new Exception("Invalid Unicode");
                }
                else if (value[i] < 0x20 && value[i] != '\t' & value[i] != '\n' & value[i] != '\r')
                {
                    throw new Exception("Invalid Xml Characters");
                }
            }
        }

        public XmlConformWriter(String fileName, Encoding encoding) : base(fileName, encoding) { }
        public XmlConformWriter(Stream stream, Encoding encoding) : base(stream, encoding) { }
        public XmlConformWriter(TextWriter writer) : base(writer) { }

        public override void WriteString(String value)
        {
            CheckUnicodeString(value);
            base.WriteString(value);
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            base.WriteStartElement(prefix, XmlConvert.EncodeLocalName(localName), ns);
        }
    }
}
