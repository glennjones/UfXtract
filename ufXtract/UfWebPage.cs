//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using HtmlAgilityPack;

namespace UfXtract
{

    //Copyright (c) 2007 Glenn Jones

	/// <summary>
	/// UfWebPage
	/// </summary>
	public class UfWebPage
	{
        private Uri uri;
        private string url = string.Empty;
        private HttpWebRequest WebReq;
        private HttpWebResponse WebResp;
        private int statusCode = 0;
		private HtmlWeb htmlWeb = new HtmlWeb();
		private HtmlDocument htmlDocument = new HtmlDocument();
		private XmlDocument xmlDocument = new XmlDocument();
        private string htmlString = string.Empty;
		private NameValueCollection parameters = new NameValueCollection(); 
		private string parameterString = string.Empty;
		private ContentType contentType = ContentType.Html;
		private RequestType requestType = RequestType.Get;
		private string password = string.Empty;
		private string username = string.Empty;
		private bool ufError = false;
        private string userAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; pt-PT; rv:1.9.2.6) Gecko/20100625 Firefox/3.6.6";
        
 

        public UfWebPage()
		{
		}



        /// <summary>
        /// Load the web page from a Uri
        /// </summary>
		public void Load( Uri uri )
		{
			this.Uri = uri;
			this.Url = uri.ToString();

			foreach ( string str in parameters.AllKeys )
				parameterString += str + "=" + parameters[str] + "&";
			
			if( parameterString != string.Empty )
				parameterString = parameterString.Substring( 0, parameterString.Length-1 );


            try
            {
                // Set useragent to say its Firefox 2
                if (this.DocumentRequestType == RequestType.Get)
                {
                    WebReq = (HttpWebRequest)WebRequest.Create(this.Url);
                    //WebReq = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", this.Url, "?" +parameterString));
                    WebReq.UserAgent = userAgent;
                    WebReq.Method = "GET";

                }
                else
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(parameterString);
                    WebReq = (HttpWebRequest)WebRequest.Create(this.Url);
                    WebReq.UserAgent = userAgent;
                    WebReq.Method = "POST";
                    WebReq.AllowAutoRedirect = false;
                    WebReq.KeepAlive = false;
                    WebReq.ContentType = "application/x-www-form-urlencoded";
                    WebReq.ContentLength = buffer.Length;
                }

                if (this.Username != string.Empty && this.Password != string.Empty)
                {
                    CredentialCache credentialCache = new CredentialCache();
                    credentialCache.Add(this.Uri, "Basic", new NetworkCredential(username, password));
                    WebReq.Credentials = credentialCache;
                }

                

                WebResp = (HttpWebResponse)WebReq.GetResponse();
                Stream stream = WebResp.GetResponseStream();
                StreamReader streamReader;

                // Deal with WebResponse encoding issues - This is not as good as it could be
                if (WebResp.CharacterSet == "ISO-8859-1" && WebResp.ContentType.IndexOf("charset") > -1)
                    streamReader = new StreamReader(stream, Encoding.GetEncoding("iso8859-1"));
                else
                    streamReader = new StreamReader(stream);

                this.HtmlString = streamReader.ReadToEnd();
                this.Url = WebResp.ResponseUri.AbsoluteUri;
                //this.Domain = WebResp.ResponseUri.Authority;
                statusCode = (int)WebResp.StatusCode;

                // Close to stop connection locking
                WebResp.Close();
                

            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                    throw;

                statusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
                WebResp.Close();
            }


			
			try
			{
                // Temp fix - Removing xhtml strict headers from html
                this.HtmlString = this.HtmlString.Replace("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">", "");
                this.HtmlString = this.HtmlString.Replace("<meta content=\"text/html; charset => utf-8\" http-equiv=\"Content-Type\" />", "");

				if( this.DocumentContentType == ContentType.Html )
					htmlDocument.LoadHtml( this.HtmlString );

				if( this.DocumentContentType == ContentType.Xml )
					xmlDocument.LoadXml( this.HtmlString );
			}
			catch(Exception ex)
			{
				//this.HtmlString = this.Url + parameterString;
			}

		}




  



		public Uri Uri
		{
			get{ return uri; }
			set{ uri = value; }
		}

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public string HtmlString
        {
            get { return htmlString; }
            set { htmlString = value; }
        }

        public HtmlDocument Html
        {
            get { return htmlDocument; }
            set { htmlDocument = value; }
        }

        public XmlDocument Xml
        {
            get { return xmlDocument; }
            set { xmlDocument = value; }
        }

		public ContentType DocumentContentType
		{
			get{ return contentType; }
			set{ contentType = value; }
		}

		public RequestType DocumentRequestType
		{
			get{ return requestType; }
			set{ requestType = value; }
		}

		public NameValueCollection Parameters
		{
			get{ return parameters; }
			set{ parameters = value; }
		}

		public string Username
		{
			get{return username;}
			set{username = value;}
		}

		public string Password
		{
			get{return password;}
			set{password = value;}
		}


        public int StatusCode
        {
            get { return statusCode; }
        }


        public bool UfError
        {
            get { return UfError; }
            set { UfError = value; }
        }


        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

		public enum ContentType
		{
			String,
			Html,
			Xml,
		}

		public enum RequestType
		{
			Get,
			Post,
		}



 



 


	}
}
