using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManager
    {
        /// <summary>
        ///  check is logined
        /// </summary>
        /// <returns></returns>
        public static bool IsLogined() 
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 取得已登入的使用者資訊(如果沒有登入就回傳null)
        /// </summary>
        /// <returns></returns>
        public static UserInfoModel GetCurrentUser() 
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
                return null;

            DataRow dr = UserInfoManager.GetUserInfoByAccount(account);
            //return dr;

            if (dr == null)
                return null;

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;

        }

        public static void Logout() 
        {
            HttpContext.Current.Session["UserLogInfo"] = null;
        }

        public static bool TryLogin(string account, string pwd, out string errorMsg) 
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "Account/PWD is required.";
                return false;
            }

            var dr = UserInfoManager.GetUserInfoByAccount(account);

            //check null
            if (dr == null)
            {
                errorMsg = $"Accounting: {account} doesn't exists";
                return false;
            }

            //check account pwd
            if (string.Compare(dr["Account"].ToString(), account, true) == 0 && string.Compare(dr["PWD"].ToString(), pwd, false) == 0) //密碼區分大小寫false
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();

                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "Login fail. Please check Account / PWD";
                return false;
            }

        } 
    }
}
