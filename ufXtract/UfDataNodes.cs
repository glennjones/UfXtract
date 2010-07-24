//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace UfXtract
{

    /// <summary>
    /// A data node collection
    /// </summary>
    public class UfDataNodes : CollectionBase  //: IList
    {
        //Copyright (c) 2007 Glenn Jones

        public UfDataNodes() : base()
		{
		}

		public void Sort(string propertyName, string direction )
		{
			InnerList.Sort(new GenericSort(propertyName, direction));
		}

        public bool Exists(string name)
        {
            bool found = false;
            for (int i = 0; i < this.InnerList.Count; i++)
            {
                UfDataNode node = (UfDataNode)this.InnerList[i];
                if( node.Name == name )
                    found = true;
            }
            return found;
        }

        /// <summary>
        /// Finds a node by position from all the nodes with the same name
        /// </summary>
        /// <param name="name">Name to saerch for</param>
        /// <param name="pos">Position in the sub collection created by the search</param>
        /// <returns></returns>
        public UfDataNode GetNameByPosition(string name, int pos)
        {
            UfDataNodes subCollection = new UfDataNodes();
            for (int i = 0; i < this.InnerList.Count; i++)
            {
                UfDataNode testNode = (UfDataNode)this.InnerList[i];
                if (testNode.Name == name)
                    subCollection.Add(testNode);
            }
            if( subCollection.Count >= pos )
                return subCollection[pos];
            else
                return new UfDataNode();
        }



		#region "Strongly typed collection form IList"

        public void Insert(int index, UfDataNode newDataNode)
		{
			InnerList.Insert(index, newDataNode);
		}

        public void Remove(UfDataNode aDataNode)
		{
			InnerList.Remove(aDataNode);
		}

        public bool Contains(UfDataNode aDataNode)
		{
			return InnerList.Contains(aDataNode);
		}

        public int IndexOf(UfDataNode aDataNode)
		{
			return InnerList.IndexOf(aDataNode);
		}

        public int Add(UfDataNode newDataNode)
		{
			return InnerList.Add(newDataNode);
		}

        public int Add(string name, string value)
        {
            UfDataNode newDataNode = new UfDataNode(name,value);
            return InnerList.Add(newDataNode);
        }

        public int Add(string name, string value, string sourceurl, bool representativenode)
        {
            UfDataNode newDataNode = new UfDataNode(name, value, sourceurl, representativenode);
            return InnerList.Add(newDataNode);
        }

        public UfDataNode Append(string name, string value)
        {
            return Append(name, value, string.Empty, false);
        }

        public UfDataNode Append(string name, string value, string sourceurl, bool representativenode)
        {
             int index = -1;
             UfDataNode outputobj = new UfDataNode();

             for (int i = 0; i < this.InnerList.Count; i++)
             {
                UfDataNode testNode = (UfDataNode)this.InnerList[i];
                if (testNode.Name == name)
                {
                    UfDataNode newNode = new UfDataNode("", value, sourceurl, representativenode);
                    index = testNode.Nodes.Add(newNode);
                    newNode.Name = index.ToString();
                    outputobj = newNode;
                }
             }
             if (index == -1)
             {
                 // Create a new node
                 UfDataNode newNode1 = new UfDataNode(name, "Array");
                 this.InnerList.Add(newNode1);
                 // Add name value pair to first objects of nodes collection
                 UfDataNode newNode2 = new UfDataNode("0", value, sourceurl, representativenode);
                 index = newNode1.Nodes.Add(newNode2);
                 outputobj = newNode2;
                 
             }
             return outputobj;
        }

        public int AppendArrayList(string name, string value)
        {
            int index = -1;
            for (int i = 0; i < this.InnerList.Count; i++)
            {
                UfDataNode testNode = (UfDataNode)this.InnerList[i];
                if (testNode.Name == name)
                {
                    // Add to  value to the arraylist of already existing node
                    testNode.ValueArray.Add(value);
                    index = i;
                }
            }
            // If no node was found
            if (index == -1)
            {
                // Create a new and add value to array list
                UfDataNode newNode = new UfDataNode();
                newNode.Name = name;
                newNode.ValueArray.Add(value);
                index = this.InnerList.Add(newNode);
            }
            return index;
        }


        public UfDataNode this[int index]
		{
			get           
			{
				if (this.InnerList.Count > index)
				{
                    return (UfDataNode)this.InnerList[index];
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

        public UfDataNode this[string name]
        {
            get
            {
                UfDataNode node = null;
                for (int i = 0; i < this.InnerList.Count; i++)
                {
                    UfDataNode testNode = (UfDataNode)this.InnerList[i];
                    if (testNode.Name == name)
                        return testNode;
                }
                return node;
            }
        }
			


		#endregion

    }


    /// <summary>
	/// A data note.
	/// </summary>
    public class UfDataNode
    {

        private string elementId = string.Empty;
        private string name = string.Empty;
        private string value = string.Empty;
        private UfDataNodes nodes = new UfDataNodes();
        private ArrayList valueArray = new ArrayList();
        private string outerHtml = string.Empty;
        private string sourceUrl = string.Empty;
        private string parentNodeNames = string.Empty;
        private bool representativeNode = false;



        /// <summary>
        /// Gets the value of a descendant node using a custom tree expression
        /// </summary>
        /// <param name="treeExpression">Custom expression of a node tree position ie "n/given-name"</param>
        /// <returns>The text value of a node. The string is empty if not found</returns>
        public string DescendantValue(string treeExpression)
        {
            string output = "";
            UfDataNode node = DescendantNode(treeExpression);
            if (node.Value != null)
                output = node.Value;

            return output;
        }



        /// <summary>
        /// Gets the descendant node using a custom tree expression
        /// </summary>
        /// <param name="treeExpression">Custom expression of a node tree position ie "n/given-name"</param>
        /// <returns>The node or null</returns>
        public UfDataNode DescendantNode(string treeExpression)
        {
            UfDataNode output = new UfDataNode();
            if(treeExpression != "" )
            {
                UfDataNode currentNode = this;
                if (treeExpression.IndexOf('/') > 0)
                {
                    string[] expressions = treeExpression.Split('/');
                    for (int i = 0; i < expressions.Length; i++)
                    {
                        string propertyName = expressions[i];

                        // We are looking for a node from an array
                        if (propertyName.IndexOf('[') > 0)
                        {
                            string[] parts = propertyName.Split('[');
                            propertyName = parts[0];
                            int index = Convert.ToInt32( parts[1].Replace("]","") );

                            UfDataNodes nodeCollection = new UfDataNodes();
                            for (int x = 0; x < currentNode.Nodes.Count; x++)
                            {
                                if (currentNode.Nodes[x].Name == propertyName)
                                    nodeCollection.Add(currentNode.Nodes[x]);
                            }

                            if (nodeCollection.Count > 0)
                            {
                                if (nodeCollection.Count-1 >= index)
                                    currentNode = nodeCollection[index];
                            }

                        }
                        else
                        {
                            // We are looking for a single node 
                            if (currentNode.Nodes[propertyName] != null)
                            {
                                currentNode = currentNode.Nodes[propertyName];
                            }
                            else
                            {
                                currentNode = null;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (currentNode.Nodes[treeExpression] != null)
                        currentNode = currentNode.Nodes[treeExpression];

                }
                    if (currentNode != null)
                        output = currentNode;
            }
            return output;
        }




        public UfDataNode()
        {
        }

        public UfDataNode(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public UfDataNode(string name, string value, string sourceurl, bool representativenode)
        {
            this.Name = name;
            this.Value = value;
            this.SourceUrl = sourceurl;
            this.RepresentativeNode = representativenode;
        }


        // Method for sort
        public string GetName()
        {
            return this.Name;
        }

        // Method for sort
        public string GetValue()
        {
            return this.Value;
        }

        // Method for sort
        public string GetSourceUrl()
        {
            return this.SourceUrl;
        }


        /// <summary>
        /// The Html element id from which the data was taken
        /// </summary>
        public string ElementId
        {
            get { return elementId; }
            set { elementId = value.Trim(); }
        }

        /// <summary>
        /// The node name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        /// <summary>
        /// The node value
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value.Trim(); }
        }

        /// <summary>
        /// The Html node structure the data was parsed from
        /// </summary>
        public string OuterHtml
        {
            get { return outerHtml; }
            set { outerHtml = value.Trim(); }
        }

        /// <summary>
        /// The full Url of page the data was parsed from
        /// </summary>
        public string SourceUrl
        {
            get { return sourceUrl; }
            set { sourceUrl = value.Trim(); }
        }

        /// <summary>
        /// A collection of child data nodes
        /// </summary>
        public UfDataNodes Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ArrayList ValueArray
        {
            get { return valueArray; }
            set { valueArray = value; }
        }


        /// <summary>
        /// Stores a list of all the parent uf element node names
        /// </summary>
        public string ParentNodeNames
        {
            get { return parentNodeNames; }
            set { parentNodeNames = value; }
        }


        public bool RepresentativeNode
        {
            get { return representativeNode; }
            set { representativeNode = value; }
        }




    }


}
