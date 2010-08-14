//Copyright (c) 2007 - 2010 Glenn Jones

using System;
using System.Web;
using System.Net;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using UfXtract.Utilities;

namespace UfXtract
{

    /// <summary>
    /// Collection of microformat optimization rules and helper functions use during parsing 
    /// </summary>
    public class UfHelpers
    {

        /// <summary>
        /// Runs a series of optimization rules across a collection
        /// </summary> 
        /// <param name="node">Node been optimized</param>
        public static void RunNodeOptimization(UfDataNode node)
        {
            for (int i = node.Nodes.Count; i > 0; i--)
                OptimizesNode(node, node.Nodes[i - 1]);

        }


        /// <summary>
        /// Runs a series of optimization rules against a node
        /// </summary>
        /// <param name="parent">Parent node</param>
        /// <param name="child">Child node been optimized</param>
        public static void OptimizesNode(UfDataNode parent, UfDataNode child)
        {


            if (child.Name == "org")
                OrgOptimization(child);

            if (child.Name == "fn")
                NameOptimization(parent, child);

            if (child.Name == "rrule")
                RruleOptimization(child);

            if (child.Name == "geo" || child.Name == "location")
                GeoOptimization(child);

            if (child.Name == "hentry")
                UpdatedDateOptimization(child);

            if (child.Name == "vevent")
                CalendarDateOptimization(child);


            if (child.Value == string.Empty && child.Nodes.Count == 0)
            {
                // Romove unneeded blank node
                parent.Nodes.Remove(child);
            }
            else
            {
                // Check that child node called "value" or "type" and override parent values 
                if (child.Name == "value" || child.Name == "type")
                    parent.Value = string.Empty;

                for (int i = child.Nodes.Count; i > 0; i--)
                {
                    OptimizesNode(child, child.Nodes[i - 1]);
                }
            }

        }


        /// <summary>
        /// This takes the text value of org and places it into organization-name
        /// </summary>
        /// <param name="node">Node containing 'org' data</param>
        public static void OrgOptimization(UfDataNode node)
        {
            if (node.Nodes["organization-name"] == null)
            {
                string name = node.Value;
                node.Nodes.Add(new UfDataNode("organization-name", name));
                node.Value = "";
            }
        }


        /// <summary>
        /// This add a updated element to hEntry if its missing
        /// </summary>
        /// <param name="node">Node containing 'hentry' data</param>
        public static void UpdatedDateOptimization(UfDataNode node)
        {
            // Swap value into organization-name node
            if (node.Nodes["published"] != null && node.Nodes["updated"] == null)
            {
                UfDataNode newUfDataNode = new UfDataNode("updated", node.Nodes["published"].Value);
                node.Nodes.Add(newUfDataNode);
            }
        }


        /// <summary>
        /// Calander end date optimization
        /// </summary>
        /// <param name="node">Node containing 'vevent' data</param>
        public static void CalendarDateOptimization(UfDataNode node)
        {
            if (node.Nodes["dtend"] != null && node.Nodes["dtstart"] != null)
            {
                //// Does dtend have the chars used for dates or datetime structures
                if (node.Nodes["dtend"].Value.StartsWith("T"))
                {
                    // Get date from dtstart and add to dtend if 
                    ISODateTime isoDateTime = new ISODateTime();
                    isoDateTime.Parse(node.Nodes["dtstart"].Value);
                    // Has to be complete date structure
                    if (isoDateTime.Date > 0)
                    {
                        // Knock over into next day
                        if (node.Nodes["dtend"].Value.Contains("T24"))
                        {
                            DateTime dateTime = new DateTime(isoDateTime.Year, isoDateTime.Month, isoDateTime.Date);
                            dateTime = dateTime.AddDays(1);
                            node.Nodes["dtend"].Value = dateTime.Year + "-" + isoDateTime.TwoDigitString(dateTime.Month) + "-" + isoDateTime.TwoDigitString(dateTime.Day) + node.Nodes["dtend"].Value.Replace("T24", "T00");
                        }
                        else
                        {
                            node.Nodes["dtend"].Value = isoDateTime.Year + "-" + isoDateTime.TwoDigitString(isoDateTime.Month) + "-" + isoDateTime.TwoDigitString(isoDateTime.Date) + node.Nodes["dtend"].Value;
                        }
                    }
                }
            }
        }


        /// <summary>
        ///  Implied name "n" optimization. Where needed breaks fn(formatted name) into given and family names.
        /// </summary>
        /// <param name="parent">Parent node containing 'vcard' data</param>
        /// <param name="child">Node containing 'fn' data</param>
        public static void NameOptimization(UfDataNode parent, UfDataNode fnnode)
        {
            if (fnnode.Value.Trim().IndexOf(" ") > 0)
            {
                // Trims unneeded whitespace
                string fn = fnnode.Value.Trim();

                string[] elm = Regex.Replace(fn.ToString(), "\\s+", " ").Split(' ');
                if (elm.Length == 2)
                {
                    if (parent.Nodes["n"] == null)
                    {
                        bool addOptimization = true;
                        bool givenFrist = true;

                        // If org = fn then stop implied name "n" optimization
                        for (int i = 0; i < parent.Nodes.Count; i++)
                        {
                            if (parent.Nodes[i].Name == "org"){
                                if (fnnode.Value == parent.Nodes[i].Value || 
                                    fnnode.Value == parent.Nodes[i].DescendantValue("organization-name"))
                                    addOptimization = false;

                            }
                        }


                        if (elm[0].EndsWith(","))
                        {
                            elm[0] = elm[0].Replace(",", "");
                            givenFrist = false;
                        }

                        if (elm[1].Replace(".", "").Length == 1)
                            givenFrist = false;

                        if (addOptimization)
                        {
                            UfDataNode n = new UfDataNode("n", "");
                            UfDataNode givenname = new UfDataNode();
                            UfDataNode familyname = new UfDataNode();

                            if (givenFrist)
                            {
                                givenname = new UfDataNode("given-name", elm[0]);
                                familyname = new UfDataNode("family-name", elm[1]);
                            }
                            else
                            {
                                givenname = new UfDataNode("given-name", elm[1]);
                                familyname = new UfDataNode("family-name", elm[0]);
                            }
                            parent.Nodes.Add(n);
                            n.Nodes.Add(givenname);
                            n.Nodes.Add(familyname);
                        }
                    }
                }
            }

            // Implied "nickname" Optimization
            if (fnnode.Value.Trim() != string.Empty && fnnode.Value.IndexOf(" ") < 0)
            {
                if (parent.Nodes["nickname"] == null)
                {
                    bool addOptimization = true;

                    if (parent.Nodes["org"] != null)
                        if (fnnode.Value.Trim() == parent.Nodes["org"].Value.Trim())
                            addOptimization = false;

                    if (addOptimization)
                    {
                        UfDataNode nickname = new UfDataNode("nickname", fnnode.Value.Trim());
                        parent.Nodes.Add(nickname);
                    }
                }
            }
        }

        /// <summary>
        /// Breaks geo string such as 37.77;-122.41 into longitude and latitude child nodes
        /// </summary>
        /// <param name="node">Node containing 'geo' data</param>
        public static void GeoOptimization(UfDataNode node)
        {

            // If geo has child nodes check they are valid and reset values
            if (node.Nodes.Count > 0)
            {
                string latitude = "";
                if (node.Nodes["latitude"] != null)
                    latitude = node.Nodes["latitude"].Value;

                string longitude = "";
                if (node.Nodes["longitude"] != null)
                    longitude = node.Nodes["longitude"].Value;

                try
                {
                    Geo geo = new Geo(latitude + ';' + longitude);
                    if (node.Nodes["latitude"] != null)
                        node.Nodes["latitude"].Value = geo.Latitude.ToString();

                    if (node.Nodes["longitude"] != null)
                        node.Nodes["longitude"].Value = geo.Longitude.ToString();

                }
                catch (Exception ex)
                {
                    // On error reset by removing child nodes
                    if (node.Nodes["longitude"] != null)
                        node.Nodes.Remove(node.Nodes["longitude"]);

                    if (node.Nodes["latitude"] != null)
                        node.Nodes.Remove(node.Nodes["latitude"]);
                }
            }

            // If geo has no child nodes parse parent text
            if (node.Nodes.Count == 0 && node.Value != "")
            {
                // If there are no child nodes
                if (node.Value.IndexOf(';') > 0)
                {
                    UfDataNode lonNode = new UfDataNode();
                    UfDataNode latNode = new UfDataNode();
                    try
                    {
                        Geo geo = new Geo(node.Value);
                        node.Nodes.Add(new UfDataNode("latitude", geo.Latitude.ToString()));
                        node.Nodes.Add(new UfDataNode("longitude", geo.Longitude.ToString()));
                    }
                    catch (Exception ex)
                    {
                        // On error reset by removing child nodes
                        node.Nodes.Clear();
                    }
                }
            }

            // Remove parent value if its a true geo and not a compound name use like "location"
            if (node.Name == "geo")
                node.Value = "";
        }


        /// <summary>
        /// Breaks calendar repeat rules strings such as "freq=weekly;byday=mo,tu,we,th,fr;byhour=17;byminute=30" into child nodes
        /// </summary>
        /// /// <param name="node">Node containing 'rrule' data</param>
        public static void RruleOptimization(UfDataNode rnode)
        {
            // Child node of rrule and value ranges

            // freq (yearly, monthly, weekly, daily, hourly, minutely, secondly)
            // count=10 (number)
            // interval=2 (number)
            // until=2008-01-01 (ISODate)
            // bysecond (-59 to 0 to 59) 
            // byminute (-59 to 0 to 59)
            // byhour (-23 to 0 to 23)
            // bymonthday (-31 to 1 to 31)
            // byyearday (-366 to 1 to 366)
            // byweekno (-53 to 1 to 53)
            // bymonth (-12 to 1 to 12)
            // byday (SU, MO, TU, WE, TH, FR, SA) comma based array of

            if (rnode.Value.Trim() != "")
            {
                if (rnode.Value.IndexOf(';') > 0)
                {
                    string[] items = rnode.Value.Split(';');
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i].IndexOf('=') > 0)
                        {
                            string[] parts = items[i].Split('=');
                            switch (parts[0].ToLower().Trim())
                            {
                                case "freq":
                                    rnode.Nodes.Add(new UfDataNode("freq", parts[1]));
                                    break;

                                case "count":
                                    rnode.Nodes.Add(new UfDataNode("count", parts[1]));
                                    break;

                                case "interval":
                                    rnode.Nodes.Add(new UfDataNode("interval", parts[1]));
                                    break;

                                case "until":
                                    rnode.Nodes.Add(new UfDataNode("until", parts[1]));
                                    break;

                                case "bysecond":
                                    rnode.Nodes.Add(new UfDataNode("bysecond", parts[1]));
                                    break;

                                case "byminute":
                                    rnode.Nodes.Add(new UfDataNode("byminute", parts[1]));
                                    break;

                                case "byhour":
                                    rnode.Nodes.Add(new UfDataNode("byhour", parts[1]));
                                    break;

                                case "bymonthday":
                                    rnode.Nodes.Add(new UfDataNode("bymonthday", parts[1]));
                                    break;

                                case "byyearday":
                                    rnode.Nodes.Add(new UfDataNode("byyearday", parts[1]));
                                    break;

                                case "byweekno":
                                    rnode.Nodes.Add(new UfDataNode("byweekno", parts[1]));
                                    break;

                                case "bymonth":
                                    rnode.Nodes.Add(new UfDataNode("bymonth", parts[1]));
                                    break;

                                case "byday":
                                    rnode.Nodes.Add(new UfDataNode("byday", parts[1]));
                                    break;

                            }
                        }
                    }
                }
            }
            // Remove parent value
            rnode.Value = "";

        }


        /// <summary>
        /// Break telephonestrings such as "fax:01234 1234567" into value/type child nodes
        /// </summary>
        /// <param name="node">Node containing 'tel' data</param>
        /// <param name="text">Telephone number string</param>
        public static void TelOptimization(UfDataNode node, string text)
        {
            // If it contains both the value and type
            if (text.IndexOf(":") > 0)
            {
                string[] parts = text.Split(':');
                node.Name = "tel";
                node.Value = "";
                node.Nodes.Add(new UfDataNode("value", parts[1]));
                node.Nodes.Add(new UfDataNode("type", parts[0]));
            }
            else
            {
                node.Name = "tel";
                node.Value = "";
                node.Nodes.Add(new UfDataNode("value", text));
            }
        }








        /// <summary>
        /// Converts Html to text equivalent
        /// </summary>
        /// <param name="node">HtmlAgilityPack html node</param>
        /// <param name="equivalentMode"></param>
        /// <returns>Text from HTML</returns>
        public static string HtmlToText(HtmlNode node, bool equivalentMode)
        {
            return HtmlToTextLooping(node, equivalentMode);
        }


        private static string HtmlToTextLooping(HtmlNode node, bool equivalentMode)
        {
            string internalString = string.Empty;
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {

                // If its not a text node
                if (node.ChildNodes[i].NodeType.ToString() != HtmlNodeType.Text.ToString())
                    if (node.ChildNodes[i].Name != "script")
                        internalString += HtmlToTextLooping(node.ChildNodes[i], equivalentMode);

                // If its is a text node
                if (node.ChildNodes[i].NodeType.ToString() == HtmlNodeType.Text.ToString())
                    internalString += FormatText(HttpUtility.HtmlDecode(node.ChildNodes[i].InnerText));

            }
            return internalString;
        }


        /// <summary>
        /// Removes unwanted white space and other chars
        /// </summary>
        /// <param name="text">Text to be formatted</param>
        /// <returns>Formatted text</returns>
        public static string FormatText(string text)
        {
            // Removes extra white space
            return Regex.Replace(text, @"\s+", @" ");
        }


        /// <summary>
        /// Finds a single attribute value from a space delimited list attribute.
        /// </summary>
        /// <param name="attString">The attribute value string</param>
        /// <param name="valueName">The single value to search for</param>
        /// <returns>True if value is present</returns>
        public static bool FindAttributeValue(string attString, string valueName)
        {
            bool found = false;
            if (attString.IndexOf(" ") > -1)
            {
                string[] valueNameArray = attString.Split(' ');
                for (int i = 0; i < valueNameArray.Length; i++)
                {
                    if (valueNameArray[i].ToLower() == valueName)
                        found = true;
                }
            }
            else
            {
                if (attString.ToLower() == valueName)
                    found = true;
            }
            return found;
        }


        /// <summary>
        /// Cleans an email address, removing protcol and querystrings
        /// </summary>
        /// <returns>Email address</returns>
        public static string CleanEmailAddress(string address)
        {
            if (address != string.Empty)
            {
                // remove protcol 
                if (address.ToLower().StartsWith("mailto:"))
                    address = address.ToLower().Replace("mailto:", "");

                // remove any querystring
                if (address.IndexOf('?') > 0)
                {
                    string[] array = address.Split('?');
                    address = array[0];
                }
            }
            return address;
        }


        /// <summary>
        /// Get the absolute url using the basehref or by relative position of the requesting document
        /// </summary>
        /// <param name="url">The url been processed</param>
        /// <param name="url">The url from base matatag (optional can be an empty string)</param>
        /// <param name="url">The document url</param>
        /// <returns>A absolute url</returns>
        public static string GetAbsoluteUrl(string url, string urlBase, string documentUrl)
        {
            if (url.Trim().ToLower() != string.Empty)
            {
                if (url.IndexOf("http") < 0 || url.IndexOf("https") < 0)
                {
                    if (urlBase != string.Empty)
                    {
                        // Use baseref as relative position
                        Uri uriBase = new Uri(urlBase);
                        Uri uri = new Uri(uriBase, url);
                        url = uri.ToString();
                    }
                    else if (documentUrl != string.Empty)
                    {
                        // Use document url as relative position
                        Uri documentUri = new Uri(documentUrl);
                        Uri uri = new Uri(documentUri, url);
                        url = uri.ToString();
                    }
                }
            }
            return url;
        }


        /// <summary>
        /// Get the tag element of a url string
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>Tag</returns>
        public static string GetTagFromUrl(string url)
        {
            string tag = "";
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Trim().ToLower();
                int index = url.LastIndexOf("/");
                if (index > 0)
                    if (index != url.Length)
                        tag = url.Substring(index + 1, url.Length - 1 - index);
            }
            return tag;
        }



    }
}
