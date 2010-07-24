using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using UfXtract;

namespace Examples.Output
{
    public partial class _xml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "http://www.glennjones.net/about/";

            UfWebRequest webRequest = new UfWebRequest();
            webRequest.Load(url, UfFormats.HCard());

            if (webRequest.Data.Nodes.Count > 0)
            {
                UfDataToXml dataConvertor = new UfDataToXml();
                Response.ContentType = "text/xml";
                dataConvertor.Convert(webRequest.Data, Response.OutputStream);
            }

        }
    }
}
