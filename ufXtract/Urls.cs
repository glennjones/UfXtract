//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;

namespace UfXtract
{

	/// <summary>
	/// Collection of Url objects
	/// </summary>
	public class Urls : CollectionBase  //: IList
	{

		public Urls() : base()
		{
		}

		public void Sort(string propertyName, string direction )
		{
			InnerList.Sort(new GenericSort(propertyName, direction));
		}


		#region "Strongly typed collection form IList"

		public void Insert(int index, Url newUrl)
		{
			InnerList.Insert(index, newUrl);
		}

		public void Remove(Url aUrl)
		{
			InnerList.Remove(aUrl);
		}

		public bool Contains(Url aUrl)
		{
			return InnerList.Contains(aUrl);
		}

		public int IndexOf(Url aUrl)
		{
			return InnerList.IndexOf(aUrl);
		}

		public int Add(Url newUrl)
		{
			return InnerList.Add(newUrl);
		}

		public Url this[int index]
		{
			get           
			{
				if (this.InnerList.Count > index)
				{
					return (Url)this.InnerList[index];
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


        public Url this[string address]
        {
            get
            {
                Url url = null;
                for (int i = 0; i < this.InnerList.Count; i++)
                {
                    Url testUrl = (Url)this.InnerList[i];
                    if (testUrl.Address.ToLower().Trim() == address.ToLower().Trim())
                        return testUrl;
                }
                return url;
            }
        }
			


		#endregion

	}


	/// <summary>
	/// Url object
	/// </summary>
	public class Url
	{

        private string address = string.Empty;
		private int status = 0;
		private TimeSpan loadTime;
        private Uri uri = null;
        private string htmlPageTitle;

		public Url()
		{
		}

        // Method for sort
        public string GetAddress()
        {
            return this.Address;
        }

        // Method for sort
        public string GetDomin()
        {
            return this.Domain;
        }

        public Url(string address)
        {
            this.Address = address;
        }


        /// <summary>
        /// The Url addresss
        /// </summary>
        public string Address 
		{
            get { return address; }
            set 
            {   
                address = ParseAddress( value );
                uri = new Uri(address);
            }
		}


        /// <summary>
        /// The Url domain name (uri.Authority)
        /// </summary>
        public string Domain
        {
            get 
            {
                if (uri != null)
                    return uri.Authority.ToLower();
                else
                    return string.Empty;
            }
        }


        /// <summary>
        /// The Http status number
        /// </summary>
        public int Status
		{
            get { return status; }
            set { status = value; }
		}


        /// <summary>
        /// The Url load time
        /// </summary>
        public TimeSpan LoadTime
		{
            get { return loadTime; }
            set { loadTime = value; }
		}


        /// <summary>
        /// The Html page title
        /// </summary>
        public string HtmlPageTitle
        {
            get { return htmlPageTitle; }
            set { htmlPageTitle = value; }
        }


        /// <summary>
        /// The Uri
        /// </summary>
        public Uri Uri
        {
            get { return uri; }
        }


        /// <summary>
        /// Try to find any non file based resouce that
        /// does not end with / and adds it.
        /// </summary>
        private string ParseAddress(string address)
        {
           address = address.Trim();
           uri = new Uri(address);
           if (uri.IsFile == false)
           {
               // Has no extentions ? or #
               if (uri.Fragment == string.Empty && uri.Query == string.Empty)
               {
                   if (address.EndsWith("/") == false)
                       address = address + "/";
               }
           }
           return address;
        }

	}
}
