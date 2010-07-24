////Copyright (c) 2007 - 2010 Glenn Jones

//using System;
//using System.Xml;
//using System.Web;
//using System.Net;
//using System.IO;
//using System.Text;
//using System.Collections;
//using HtmlAgilityPack;


//namespace UfXtract
//{
//    //Copyright (c) 2007 Glenn Jones

//    /// <summary>
//    /// Loads an one or more webpage and parse the results
//    /// </summary>
//    public class UfSpider
//    {

//        private bool useRelPaging = true;
//        private int pageLimit = 20;
//        private Urls parsedUrls = new Urls();
//        private UfErrors errors = new UfErrors();
//        private UfDataNode data = new UfDataNode();
//        private UfFormatDescriber formatDescriber = new UfFormatDescriber();
//        private bool multiples = false;

//        public UfSpider()
//        {
         
//        }

//        /// <summary>
//        /// Loads a single Html pages and does a single parse
//        /// </summary>
//        /// <param name="url">A full web page address</param>
//        /// <param name="format">A UfFormatDescriber of the uf</param>
//        public void LoadPage(string url, UfFormatDescriber format)
//        {
//            try
//            {
//                if (url != string.Empty)
//                {
//                    // Check for issues with url
//                    url = url.Trim();
//                    url = HttpUtility.UrlDecode(url);

//                    UfWebPage webPage = LoadHtmlDoc(url);
//                    formatDescriber = format;

//                    if (webPage != null)
//                    {
//                        Url urlReport = new Url();
//                        urlReport.Address = webPage.Url;
//                        urlReport.Status = webPage.StatusCode;
//                        parsedUrls.Add(urlReport);

//                        if (webPage.StatusCode == 200 && webPage.Html != null)
//                            ParseUf(webPage.Html, webPage.Url, format, false, urlReport);

//                        if (webPage.StatusCode != 200 && webPage.Html != null)
//                            errors.Add(new UfError("Could not load url", url, webPage.StatusCode));

//                    }

//                }
//                else
//                {
//                    errors.Add(new UfError("No Url given", url));
//                }

//            }
//            catch (Exception ex)
//            {
//                if (ex.Message != string.Empty)
//                    errors.Add(new UfError(ex.Message, url));
//                else
//                    errors.Add(new UfError("Could not load Url", url));
//            }
            
//        }



//        /// <summary>
//        /// Loads a single Html pages and runs multiple parses
//        /// </summary>
//        /// <param name="url">A full web page address</param>
//        /// <param name="formatArray">An array of UfFormatDescriber's</param>
//        public void LoadPage(string url, ArrayList formatArray)
//        {
//            try
//            {
//                if (url != string.Empty)
//                {
//                    url = url.Trim();
//                    UfWebPage webPage = LoadHtmlDoc(url);
//                    if (webPage != null)
//                    {
//                        Url urlReport = new Url();
//                        urlReport.Address = webPage.Url;

//                        // Process many time
//                        foreach (UfFormatDescriber format in formatArray)
//                        {
//                            urlReport.Status = webPage.StatusCode;
//                            parsedUrls.Add(urlReport);

//                            if (webPage.StatusCode == 200 && webPage.Html != null)
//                                ParseUf(webPage.Html, webPage.Url, format, true, urlReport);

//                            if (webPage.StatusCode != 200 && webPage.Html != null)
//                                errors.Add(new UfError("Could not load Url", url, webPage.StatusCode));
                            
//                        }
//                    }
//                }
//                else
//                {
//                    errors.Add(new UfError("No Url given", url));
//                }

//            }
//            catch (Exception ex)
//            {
//                if (ex.Message != string.Empty)
//                    errors.Add(new UfError(ex.Message, url));
//                else
//                    errors.Add(new UfError("Could not process Url", url));
//            }

//        }


//        /// <summary>
//        /// Load a exteranl html document using webPage
//        /// </summary>
//        /// <param name="url">A full web page address</param>
//        /// <returns></returns>
//        private UfWebPage LoadHtmlDoc(string url)
//        {
//            UfWebPage webPage = new UfWebPage();
//            //try
//            //{
//                if (url != string.Empty)
//                {
//                    // Check for issues with url
//                    url = url.Trim();
//                    if (url.StartsWith("http://") == false && url.StartsWith("https://") == false && url.StartsWith("file://") == false)
//                        url = "http://" + url;

//                    // Load page once
//                    Uri uri = new Uri(url);
//                    webPage.DocumentContentType = UfWebPage.ContentType.Html;
//                    webPage.DocumentRequestType = UfWebPage.RequestType.Get;
//                    webPage.Load(uri);
//                }
//            //}
//            //catch (Exception ex)
//            //{
//            //    webPage = null;
//            //    if (ex.Message != string.Empty)
//            //        errors.Add(new UfError(ex.Message, url));
//            //    else
//            //        errors.Add(new UfError("Could not load Url", url));
//            //}
//            return webPage;
//        }



//        // Parse uf
//        private void ParseUf(HtmlDocument htmlDoc, string url, UfFormatDescriber format, bool multiples, Url urlReport)
//        {
//            DateTime started = DateTime.Now;

//            UfParse ufparse = new UfParse();
//            ufparse.Load(htmlDoc, url, format);
//            if (multiples)
//                data.Nodes.Add(ufparse.Data);
//            else
//                data = ufparse.Data;

//            DateTime ended = DateTime.Now;
//            urlReport.LoadTime = ended.Subtract(started);
//            urlReport.HtmlPageTitle = ufparse.HtmlPageTitle;
//        }



//        // Works out if to parse the next page
//        public bool ShouldParseUrl( string parseUrl )
//        {
//            bool parseit = false;
//            if (useRelPaging == true && parseUrl!= string.Empty && parsedUrls.Count <= pageLimit)
//            {
//                bool found = false;
//                for (int i = 0; i < parsedUrls.Count; i++)
//                {
//                    if (parsedUrls[i].Address.ToLower() == parseUrl.ToLower())
//                        found = true;
//                }
//                if (found == false)
//                    parseit = true;
//            }
//            return parseit;
//        }
   



//        public bool UseRelPaging
//        {
//            get { return useRelPaging; }
//            set { useRelPaging = value; }
//        }

//        public int PageLimit
//        {
//            get { return pageLimit; }
//            set { pageLimit = value; }
//        }

//        public Urls Urls
//        {
//            get { return parsedUrls; }
//            set { parsedUrls = value; }
//        }


//        public UfErrors Errors
//        {
//            get { return errors; }
//            set { errors = value; }
//        }



//        /// <summary>
//        /// The resulting data structure from a parse 
//        /// </summary>
//        public UfDataNode Data
//        {
//            get { return data; }
//            set { data = value; }
//        }

//        public UfFormatDescriber FormatDescriber
//        {
//            get { return formatDescriber; }
//            set { formatDescriber = value; }
//        }


//    }

//}
