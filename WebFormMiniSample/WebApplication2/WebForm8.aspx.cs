using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm8 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //用FindControl找到指定控制項
            var ctl = this.ucDomSample.FindControl("Image1");

            if (ctl != null)
                ctl.Visible = false;
        }
    }
}