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

namespace Examples.Reporting
{
    public partial class errors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
string url = "http://www.xxxxxxxxxx.yyy/";

UfWebRequest webRequest = null;
UfErrors errors = new UfErrors();
try
{
    webRequest = new UfWebRequest();
    webRequest.Load(url, UfFormats.HCard());
}
catch (Exception ex)
{
    if(webRequest.Urls.Count > 0) 
        errors.Add(new UfError(ex.Message, url, webRequest.Urls[0].Status));
    else
        errors.Add(new UfError(ex.Message, url));
}


UfDataToJson dataConvertor = new UfDataToJson();
Response.ContentType = "application/json";

//Add reporting and errors
dataConvertor.Urls = webRequest.Urls;
dataConvertor.Errors = errors;

Response.Write(dataConvertor.Convert(webRequest.Data, UfFormats.HCard()));
            
        }
    }
}
