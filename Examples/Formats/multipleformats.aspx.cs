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

namespace Examples.Formats
{
    public partial class multipleformats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "http://www.glennjones.net/about/";

            UfWebRequest webRequest = new UfWebRequest();
            ArrayList formatArray = new ArrayList();
            formatArray.Add(UfFormats.HCard());
            formatArray.Add(UfFormats.Xfn());


            webRequest.Load(url, formatArray);

            if (webRequest.Data.Nodes.Count > 0)
            {
                UfDataToJson dataConvertor = new UfDataToJson();
                Response.ContentType = "application/json";

                Response.Write(dataConvertor.Convert(webRequest.Data, formatArray));
            }
        }
    }
}
