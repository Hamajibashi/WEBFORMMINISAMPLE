using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write("WebForm5_Init<br/>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("WebForm5_Load<br/>");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write("WebForm5_PreRender<br/>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("WebForm5_Click<br/>");
        }
    }
}