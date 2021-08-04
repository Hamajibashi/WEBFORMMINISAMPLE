using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Main : System.Web.UI.MasterPage
    {
        //設定一屬性MyTitle並初始化
        public string MyTitle { get; set; } = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write("MasterPage-Page_Init<br/>");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("MasterPage-Page_Load<br/>");
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write("MasterPage-Page_PreRender<br/>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            this.ltlMsg.Text = this.txtEmail.Text;
            Response.Write("MasterPage-Button1_Click<br/>");
        }

        public void SetPageCaption(string title)
        {
            //若不是空值才做輸出
            if (!string.IsNullOrWhiteSpace(title))
                this.ltlCaption.Text = title;
        }

    }
}