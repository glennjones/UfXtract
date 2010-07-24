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

namespace Examples.Inputs
{
    public partial class html : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string html = "<div class=\"vcard\">";
            html += "<span class=\"fn\">Glenn Jones</span>";
            html += "</div>";

            UfParse parser = new UfParse();
            parser.Load(html, UfFormats.HCard());

            if (parser.Data.Nodes.Count > 0)
            {
                UfDataToJson dataConvertor = new UfDataToJson();
                Response.ContentType = "application/json";
                Response.Write(dataConvertor.Convert(parser.Data, UfFormats.HCard()));
            }
        }
    }
}
