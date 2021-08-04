using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            //取得自己的Master轉型成Main
            Main mainMaster = this.Master as Main;
            mainMaster.MyTitle = "預設頁";
            mainMaster.SetPageCaption("預設頁");

            //this.ucCoverImage.SetText("第一個uc");
            this.ucCoverImage1.SetText("第二個uc");
            this.ucCoverImage2.SetText("第三個uc");
        }
    }
}