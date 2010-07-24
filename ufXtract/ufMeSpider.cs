using System;
using System.Xml;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Collections;
using HtmlAgilityPack;


namespace ufXtract
{
    //Copyright (c) 2007 Glenn Jones

    /// <summary>
    /// Summary description for ufSpider
    /// </summary>
    public class ufMeSpider
    {

        int m_liPageLimit = 20;

        Urls m_AllParsedUrls = new Urls();
        private ufDateNode m_oData = new ufDateNode();

        private ufDateNode m_oProfiles = new ufDateNode();
        private ufDateNode m_oXfn = new ufDateNode();
        private ufDateNode m_oMe = new ufDateNode();

        private ufBuilder m_ufBuilder = new ufBuilder();
        private ufFormatDescriber m_oHCardDescriber = new ufFormatDescriber();
        private ufFormatDescriber m_oHCardXfnDescriber = new ufFormatDescriber();
        private ufFormatDescriber m_oXfnDescriber = new ufFormatDescriber();
        private ufFormatDescriber m_oMeDescriber = new ufFormatDescriber();


        public ufMeSpider() 
        {
            m_oHCardDescriber = m_ufBuilder.BuildhCard();
            m_oHCardXfnDescriber  = m_ufBuilder.BuildhCardXFN();
            m_oXfnDescriber = m_ufBuilder.BuildXfn();
            m_oMeDescriber = m_ufBuilder.BuildMe();
        }

        
        public void LoadStartPage(string url)
        {
            LoadPage(url, m_oMeDescriber);
            m_oMe.Nodes.Add(m_oData);
            foreach(ufDateNode node in m_oData.Nodes)
            {
                if (node.Nodes["link"] != null)
                {
                    string link = node.Nodes["link"].Value;
                    if (ShouldParseUrl(link))
                    {
                        LoadPage(link, m_oMeDescriber);
                    }
                }
            }
        }



        private void LoadPage(string url, ufFormatDescriber format)
        {
            if (url != string.Empty)
            {
                Url urlReport = new Url();
                urlReport.Address = url;
                //m_oFormatDescriber = format;

                UrlModule urlModule = new UrlModule();
                Uri uri = new Uri(url);
                urlModule.DocumentContentType = UrlModule.ContentType.Html;
                urlModule.DocumentRequestType = UrlModule.RequestType.Get;
                urlModule.Load(uri);

                urlReport.Status = urlModule.StatusCode;
                DateTime started = DateTime.Now;
                m_AllParsedUrls.Add(urlReport);

                if (urlModule.StatusCode == 200 && urlModule.Html != null)
                    ParseUf(urlModule.Html, uri.ToString(), format);

                //----------------------
                Urls newUrls = new Urls();
                m_oMe.Nodes.Add(m_oData);

                foreach(ufDateNode node in m_oData.Nodes)
                {
                    if (node.Nodes["link"] != null)
                    {
                        string link = node.Nodes["link"].Value;
                        if (ShouldParseUrl(link))
                            newUrls.Add(new Url(link)); 
                    }
                }
                foreach (Url newUrl in newUrls)
                {
                    LoadPage(newUrl.Address, m_oMeDescriber);
                }
                //----------------------

                DateTime ended = DateTime.Now;
                urlReport.LoadTime = ended.Subtract(started);
            }
        }



        // Parse uf
        private void ParseUf(HtmlDocument htmlDoc, string url, ufFormatDescriber format )
        {
            ufParse ufparse = new ufParse();
            ufparse.Load(htmlDoc, url, format);
            this.Data = ufparse.Data;
        }



        // Works out if to parse the next page
        private bool ShouldParseUrl( string parseUrl )
        {
            bool parseit = false;
            if (parseUrl!= string.Empty && m_AllParsedUrls.Count <= m_liPageLimit)
            {
                bool found = false;
                for (int i = 0; i < m_AllParsedUrls.Count; i++)
                {
                    if (m_AllParsedUrls[i].Address.ToLower() == parseUrl.ToLower())
                       found = true;
                }
                if (found == false)
                    parseit = true;
            }
            return parseit;
        }
   



        public int PageLimit
        {
            get { return m_liPageLimit; }
            set { m_liPageLimit = value; }
        }

        public Urls Urls
        {
            get { return m_AllParsedUrls; }
            set { m_AllParsedUrls = value; }
        }


        /// <summary>
        /// The resulting data structure from a parse 
        /// </summary>
        public ufDateNode Data
        {
            get { return m_oData; }
            set { m_oData = value; }
        }

        public ufDateNode Profile
        {
            get { return m_oProfiles; }
            set { m_oProfiles = value; }
        }

        public ufDateNode Xfn
        {
            get { return m_oXfn; }
            set { m_oXfn = value; }
        }

        public ufDateNode Me
        {
            get { return m_oMe; }
            set { m_oMe = value; }
        }


   






        //public ufFormatDescriber FormatDescriber
        //{
        //    get { return m_oFormatDescriber; }
        //    set { m_oFormatDescriber = value; }
        //}


    }

}