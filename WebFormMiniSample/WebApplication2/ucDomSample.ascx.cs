using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class ucDomSample : System.Web.UI.UserControl
    {
        public Image MyImage1 { get { return this.Image1; } }
        public Image GetImage() 
        {
            return this.Image1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Button1.Visible = false;
            //this.Label1.Text = "Hello!";

            //Control ctl = this.FindControl("PlaceHolder1");

            ////找得到就隱藏
            //if (ctl != null) 
            //{
            //    //ctl.Visible = false;
            //    var firstSubControl = ctl.Controls[0];

            //    if (firstSubControl != null) 
            //    {
            //        firstSubControl.Visible = false;
            //    }
            //}

            //透過索引去找控制項！但是這樣就必須確保路徑上一定有東西，不能更改控制項順序
            this.Controls[0].Controls[1].Controls[0].Visible = false;

            //var ltl = this.FindControl("Literal1");

            var ltl = this.FindControl("Literal1") as Literal;
            

            if (ltl != null)
            {
                //ltl.Visible = false;
                ltl.Text = "Changed By Page_Load";
            }

            this.WriteControlID(this);
        }

        private void WriteControlID(Control ctl)
        {
            //是null就停止
            if (ctl == null)
                return;
            Response.Write(ctl.ID + "<br/>");

            //沒有子控制項了
            if (ctl.Controls.Count == 0)
                return;

            //把參數跑一遍
            foreach (Control item in ctl.Controls)
            {
                this.WriteControlID(item);
            }
        }
    }
}