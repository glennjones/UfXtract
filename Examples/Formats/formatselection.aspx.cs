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
    public partial class formatselection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
string url = "";
string formatString = "";
UfFormatDescriber formatDescriber = null; ;

if (Request.QueryString["format"] != null)
    formatString = Request.QueryString["format"];

if (Request.QueryString["url"] != null)
    url = Request.QueryString["url"];

switch (formatString)
{
    case "hcard":
        formatDescriber = UfFormats.HCard();
        break;
    case "hcalendar":
        formatDescriber = UfFormats.HCalendar();
        break;
    case "hreview":
        formatDescriber = UfFormats.HReview();
        break;
    case "hresume":
        formatDescriber = UfFormats.HResume();
        break;
    case "hatom":
        formatDescriber = UfFormats.HAtomItem();
        break;
    case "xfn":
        formatDescriber = UfFormats.Xfn();
        break;
    case "tag":
        formatDescriber = UfFormats.Tag();
        break;
    case "geo":
        formatDescriber = UfFormats.Geo();
        break;
    case "adr":
        formatDescriber = UfFormats.Adr();
        break;
    case "no-follow":
        formatDescriber = UfFormats.NoFollow();
        break;
    case "license":
        formatDescriber = UfFormats.License();
        break;
    case "votelinks":
        formatDescriber = UfFormats.VoteLinks();
        break;
    case "hcard-xfn":
        formatDescriber = UfFormats.HCardXFN();
        break;
    case "me":
        formatDescriber = UfFormats.Me();
        break;
    case "nextprevious":
        formatDescriber = UfFormats.NextPrevious();
        break;
    case "test-suite":
        formatDescriber = UfFormats.TestSuite();
        break;
    case "test-fixture":
        formatDescriber = UfFormats.TestFixture();
        break;

}


if (formatDescriber != null && url != "")
{
    UfWebRequest webRequest = new UfWebRequest();
    webRequest.Load(url, formatDescriber);

    if (webRequest.Data.Nodes.Count > 0)
    {
        UfDataToJson dataConvertor = new UfDataToJson();
        Response.ContentType = "application/json";
        Response.Write(dataConvertor.Convert(webRequest.Data, formatDescriber));
    }
}
        }
    }
}
