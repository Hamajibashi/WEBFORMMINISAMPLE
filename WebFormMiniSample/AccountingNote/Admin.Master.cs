using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();

            //string account = this.Session["UserLoginInfo"] as string;
            //DataRow dr = UserInfoManager.GetUserInfoByAccount(account);

            if (currentUser == null) //如果帳號不存在，導至登入頁
            {
                Response.Redirect("/Login.aspx");
                return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.ltlMsg.Text = this.TextBox1.Text;
        }
    }
}