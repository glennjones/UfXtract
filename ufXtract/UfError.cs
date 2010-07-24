//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;

namespace UfXtract
{

    /// <summary>
    /// Collection of UfError objects.
    /// </summary>
    public class UfErrors : CollectionBase  //: IList
    {


        public UfErrors(): base()
        {
        }

        public void Sort(string propertyName, string direction)
        {
            InnerList.Sort(new GenericSort(propertyName, direction));
        }


        #region "Strongly typed collection form IList"

        public void Insert(int index, UfError newUfError)
        {
            InnerList.Insert(index, newUfError);
        }

        public void Remove(UfError aUfError)
        {
            InnerList.Remove(aUfError);
        }

        public bool Contains(UfError aUfError)
        {
            return InnerList.Contains(aUfError);
        }

        public int IndexOf(UfError aUfError)
        {
            return InnerList.IndexOf(aUfError);
        }

        public int Add(UfError newUfError)
        {
            return InnerList.Add(newUfError);
        }

        public UfError this[int index]
        {
            get
            {
                if (this.InnerList.Count > index)
                {
                    return (UfError)this.InnerList[index];
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



        #endregion

    }


    /// <summary>
    /// UfError object
    /// </summary>
    public class UfError
    {
    
        private string message = string.Empty;
        private string address = string.Empty;
        private int status = 0;

        public UfError()
        {
        }

        public UfError(string message, string address)
        {
            this.message = message;
            this.address = address;
        }

        public UfError(string msg, string address, int status)
        {
            this.message = message;
            this.address = address;
            this.status = status;
        }

        public string Message
        {
            get { return message; }
            set { message = value.Trim(); }
        }

        public string Address
        {
            get { return address; }
            set { address = value.Trim(); }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }


    }
}
