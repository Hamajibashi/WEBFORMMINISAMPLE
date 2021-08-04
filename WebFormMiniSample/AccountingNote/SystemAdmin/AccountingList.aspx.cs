using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.Auth;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        public object AccounitngManager { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //check is logined
            //if (this.Session["UserLoginInfo"] == null) 
            if(!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null) 
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            ////read accounting data (
            //var dt = AccountingManager.GetAccountingList(currentUser.ID);

            //if (dt.Rows.Count > 0)// check is empty data
            //{
            //    this.gvAccountingList.DataSource = dt;
            //    this.gvAccountingList.DataBind();
            //}
            //else
            //{
            //    this.gvAccountingList.Visible = false;
            //    this.plcNoData.Visible = true;
            //}

            //read accounting data
            var dt = AccountingManager.GetAccountingList(currentUser.ID);

            if (dt.Rows.Count > 0)//Check is empty data
            {
                var dtPaged = this.GetPagedDataTable(dt);

                //int totalPages = this.GetTotalPages(dt);
                //var dtPaged = this.GetPagedDataTable(dt);
                this.ucPager2.TotalSize = dt.Rows.Count;
                this.ucPager2.Bind();

                this.gvAccountingList.DataSource = dtPaged;
                this.gvAccountingList.DataBind();

                //var pages = (dt.Rows.Count / 10);
                //if (dt.Rows.Count % 10 > 0)
                //    pages += 1;

                //this.ltPager.Text = $"共{dt.Rows.Count}筆，共{pages}頁，目前在第{this.GetCurrentPage()}頁<br/>";

                //for (var i = 1; i <= totalPages; i++) 
                //{
                //    this.ltPager.Text = $"<a href='AccountingList.aspx?page={i}'>{i}</a>&nbsp;";
                //}
            }
            else 
            {
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
            }
        }

        //private int GetTotalPages(DataTable dt) 
        //{
        //    int pagers = dt.Rows.Count / 10;
        //    if ((dt.Rows.Count % 10) > 0)
        //        pagers += 1;
        //    return pagers;
        //}

        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["Page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }

        private DataTable GetPagedDataTable(DataTable dt)
        {
            //Clone只拿結構，Copy結構資料都拿，但沒有資料時會出錯。
            DataTable dtPaged = dt.Clone();
            int pageSize = this.ucPager2.PageSize;

            int startIndex = (this.GetCurrentPage() - 1) * pageSize;
            int endIndex = (this.GetCurrentPage()) * pageSize;
            if (endIndex > dt.Rows.Count)
                endIndex = dt.Rows.Count;

            //foreach (DataRow dr in dt.Rows)
            //for(var i= 0; i< 10; i++) 會壞掉？
            for(var i = startIndex; i<endIndex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();

                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }

                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
            return;
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            //var dr = e.Row.DataItem as DataRow;
            //int actType = (int)dr["ActType"];

           if (row.RowType == DataControlRowType.DataRow) 
            {
                //Literal ltl = row.FindControl("ltActType") as Literal;
                Label lbl = row.FindControl("lblActType") as Label;

                //ltl.Text = "OK";

                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");

                if (actType == 0)
                {
                    //ltl.Text = "支出";
                    lbl.Text = "支出";
                }
                else 
                {
                    //ltl.Text = "收入";
                    lbl.Text = "收入";
                }

                if (dr.Row.Field<int>("Amount") > 1500) 
                {
                    lbl.ForeColor = Color.Red;
                }
            }
        }
    }
}