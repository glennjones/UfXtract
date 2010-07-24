//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using UfXtract.Utilities;
using Newtonsoft.Json;

namespace UfXtract
{
    /// <summary>
    /// Converts a UfXtract JSON structure into UfDataNode structure
    /// </summary>
    public class JsonToUfData
    {

        /// <summary>
        /// Converts a UfXtract JSON structure into UfDataNode structure
        /// </summary>
        public JsonToUfData(){}


        /// <summary>
        /// Converts a UfXtract JSON structure into UfDataNode structure
        /// </summary>
        public UfDataNode Convert(string json)
        {
            UfDataNode node = new UfDataNode();
            string propertyName = "";
            using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.Depth > 2)
                    {
                        if (jsonReader.TokenType == JsonToken.PropertyName)
                            propertyName = jsonReader.Value.ToString();

                        if (jsonReader.TokenType == JsonToken.StartArray)
                            CreateArray(node, jsonReader, propertyName);

                        if (jsonReader.TokenType == JsonToken.StartObject)
                            CreateObject(node, jsonReader, propertyName);

                        if (jsonReader.TokenType == JsonToken.String)
                            AddString(node, propertyName, jsonReader.Value);
                    }

                }
            }
            CleanData(node);
            return node;
        }

        /// <summary>
        /// Takes a JSON array and add child UfDataNode nodes to parent
        /// </summary>
        private void CreateArray(UfDataNode node, JsonReader jsonReader, string name)
        {
            string propertyName = "";
            UfDataNode newNode = new UfDataNode();
            newNode.Name = name;

            jsonReader.Read();
            if (jsonReader.TokenType == JsonToken.String && propertyName == "")
                AddString(node, name, jsonReader.Value);
            
            
            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.EndArray)
                {
                    break;
                }
                else
                {
                    if (jsonReader.TokenType == JsonToken.PropertyName)
                        propertyName = jsonReader.Value.ToString();

                    if (jsonReader.TokenType == JsonToken.StartArray)
                        CreateArray(newNode, jsonReader, propertyName);

                    if (jsonReader.TokenType == JsonToken.StartObject)
                        CreateObject(newNode, jsonReader, propertyName);

                    if (jsonReader.TokenType == JsonToken.String && propertyName == "")
                        AddString(node, name, jsonReader.Value);

                    if (jsonReader.TokenType == JsonToken.String && propertyName != "")
                        AddString(newNode, propertyName, jsonReader.Value);
                    
                }
            }
      

            node.Nodes.Add(newNode);
        }

        /// <summary>
        /// Takes a JSON object and add child UfDataNode node to parent
        /// </summary>
        private void CreateObject(UfDataNode node, JsonReader jsonReader, string name)
        {
            string propertyName = "";
            UfDataNode newNode = new UfDataNode();
            newNode.Name = name;

            while (jsonReader.Read())
            {
                if (jsonReader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
                else
                {
                    if (jsonReader.TokenType == JsonToken.PropertyName)
                        propertyName = jsonReader.Value.ToString();

                    if (jsonReader.TokenType == JsonToken.StartArray)
                        CreateArray(newNode, jsonReader, propertyName);

                    if (jsonReader.TokenType == JsonToken.StartObject)
                        CreateObject(newNode, jsonReader, propertyName);

                    if (jsonReader.TokenType == JsonToken.String)
                        AddString(newNode, propertyName, jsonReader.Value);
                }
            }
            node.Nodes.Add(newNode);
        }

        /// <summary>
        /// Takes a JSON string and add child UfDataNode node to parent
        /// </summary>
        private void AddString(UfDataNode node, string propertyName, object jsonValue)
        {
            if (propertyName == "id")
            {
                node.ElementId = System.Convert.ToString(jsonValue);
            }
            else
            {
                UfDataNode newNode = new UfDataNode();
                newNode.Name = propertyName;
                newNode.Value = System.Convert.ToString(jsonValue);
                node.Nodes.Add(newNode);
            }
        }



        /// <summary>
        /// Removes empty nodes from data tree
        /// </summary>
        private void CleanData(UfDataNode parent)
        {
            foreach (UfDataNode child in parent.Nodes)
                CleanDataNode(parent, child);

        }

        /// <summary>
        /// Removes empty node from data tree
        /// </summary>
        private void CleanDataNode(UfDataNode parent, UfDataNode child)
        {
            if (child.Value == string.Empty && child.Nodes.Count == 0)
                parent.Nodes.Remove(child);
            else
                for (int i = child.Nodes.Count; i > 0; i--)
                    CleanDataNode(child, child.Nodes[i - 1]);
        }

    }
}
