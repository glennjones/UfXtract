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

namespace Examples.Net
{
    public partial class read : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
string url = "http://www.glennjones.net/about/";

UfWebRequest webRequest = new UfWebRequest();
webRequest.Load(url, UfFormats.HCard());

UfDataNode hcard = webRequest.Data.Nodes[0];
string gn1 = hcard.DescendantValue("n/given-name");
string gn2 = hcard.DescendantNode("n/given-name").Value;
string ea = hcard.DescendantNode("adr[0]/extended-address[1]").Value;

Response.Write("<div>" + gn1 + "</div>");
Response.Write("<div>" + gn2 + "</div>");
Response.Write("<div>" + ea + "</div>");

Response.Write("<html><body>");
foreach (UfDataNode node in webRequest.Data.Nodes)
    WriteNode(node, "");

Response.Write("</body></html>");
        }

public void WriteNode(UfDataNode node, string indent)
{
    Response.Write("<div>" + indent + node.Name + " - " + node.Value);
    indent += "&nbsp;&nbsp;&nbsp;";
    foreach (UfDataNode childnode in node.Nodes)
    {
        //Response.Write("<div>" + indent + childnode.Name + " - " + childnode.Value + "</div>");
        WriteNode(childnode, indent);
    }
}
    }
}
