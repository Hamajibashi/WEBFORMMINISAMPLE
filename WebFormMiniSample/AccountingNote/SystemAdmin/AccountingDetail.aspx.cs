using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check is logined
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string account = this.Session["UserLoginInfo"] as string;
            var drUserInfo = UserInfoManager.GetUserInfoByAccount(account);

            if (drUserInfo == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            // Check is create mode or edit mode 確認是編輯模式還是新增模式
            if (!this.IsPostBack) 
            {
                //上方網址?ID=null時 = 新增模式，要把刪除鈕藏起來
                if (this.Request.QueryString["ID"] == null)  //剛剛打成不等於!= ...
                {
                    this.btnDelete.Visible = false;
                }
                else
                {
                    this.btnDelete.Visible = true;

                    string idText = this.Request.QueryString["ID"];
                    int id;
                    if (int.TryParse(idText, out id))
                    {
                        //var drAccounting = AccountingManager.GetAccounting(id);

                        //保護資料不被其他帳號修改
                        var drAccounting = AccountingManager.GetAccounting(id, drUserInfo["ID"].ToString());

                        //可能是已經被刪除的資料
                        if (drAccounting == null)
                        {
                            this.ltMsg.Text = "Data doesn't exist";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
                            this.dplActType.SelectedValue = drAccounting["ActType"].ToString();
                            this.txtAmount.Text = drAccounting["Amount"].ToString();
                            this.txtCaption.Text = drAccounting["Caption"].ToString();
                            this.txtDesc.Text = drAccounting["Body"].ToString();
                        }
                    }
                    else 
                    {
                        this.ltMsg.Text = "ID is Required.";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //檢查是否為正常輸入值
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList)) 
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            //現在登入的使用者
            string account = this.Session["UserLoginInfo"] as string;
            var dr = UserInfoManager.GetUserInfoByAccount(account);
            
            if (dr == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            //輸入值
            string userID = dr["ID"].ToString();
            string actTypeText = this.dplActType.SelectedValue;
            string amountText = this.txtAmount.Text;
            string caption = this.txtCaption.Text;
            string body = this.txtDesc.Text;

            //轉成整數
            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);


            string idText = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idText))//新增模式
            {
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else //編輯模式
            {
                int id;
                if (int.TryParse(idText, out id)) 
                {
                    AccountingManager.UpdateAccounting(id, userID, caption, amount, actType, body);
                }
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList) 
        {
            List<string> msgList = new List<string>();

            //Type
            if (this.dplActType.SelectedValue !="0" && this.dplActType.SelectedValue!="1") 
            {
                msgList.Add("Type must be 0 or 1.");
            }

            //Amount
            if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
            {
                msgList.Add("Amount is required.");
            }
            else 
            {
                int tempInt;
                if (!int.TryParse(this.txtAmount.Text, out tempInt)) 
                {
                    msgList.Add("Amount must be a number.");
                }

                if (tempInt < 0 || tempInt > 1000000) 
                {
                    msgList.Add("Amount must between 0 and 1,000,000.");
                }
            }

            errorMsgList = msgList;

            if (msgList.Count == 0)
                return true;
            else
                return false;
        }//bool

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idText = this.Request.QueryString["ID"];

            if (string.IsNullOrWhiteSpace(idText))
                return;
            int id;
            if (int.TryParse(idText, out id))//新增模式
            {
                AccountingManager.DeleteAccounting(id);
            }

            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
    }
}