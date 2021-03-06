using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.UserControl
{
    public partial class ucPager2 : System.Web.UI.UserControl
    {
        /// <summary>頁面</summary>
        public string Url { get; set; }
        /// <summary>總筆數</summary>
        public int TotalSize { get; set; }
        /// <summary>頁面筆數</summary>
        public int PageSize { get; set; }
        /// <summary>目前頁數</summary>
        public int CurrentPage { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int GetCurrentPage()
        {
            string pageText = this.Request.QueryString["page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int pageIndex = 0;
            if (!int.TryParse(pageText, out pageIndex))
                return 1;

            return pageIndex;
        }

        //算頁數、超連結
        public void Bind()
        {
            //檢查一頁筆數
            if (this.PageSize <= 0)
                throw new DivideByZeroException();

            //算總頁數，有餘數會再加一頁
            int totalPage = this.TotalSize / this.PageSize;
            if (this.TotalSize % this.PageSize > 0)
                totalPage += 1;

            this.aLinkFirst.HRef = $"{this.Url}?page=1";
            this.aLinkLast.HRef = $"{this.Url}?page={totalPage}";

            //this.aLink1.HRef = "https://www.google.com";
            //this.aLink1.InnerText = "50";

            //計算目前頁數
            this.CurrentPage = this.GetCurrentPage();
            this.ltlCurrentPage.Text = this.CurrentPage.ToString();

            //if (this.CurrentPage == 1)
            //{
            //    this.aLink1.Visible = false;
            //    this.aLink2.Visible = false;

            //    this.aLink3.HRef = "";
            //}
            //else if (this.CurrentPage == 2) 
            //{
            //    this.aLink1.Visible = false;
            //}
            //else if(this.CurrentPage == (totalPage-1))
            //{
            //    this.aLink5.Visible = false;
            //}
            //else if (this.CurrentPage == totalPage)
            //{
            //    this.aLink4.Visible = false;
            //    this.aLink5.Visible = false;

            //    this.aLink3.HRef = "";
            //}

            //計算頁數
            int prevM1 = this.CurrentPage - 1;
            int prevM2 = this.CurrentPage - 2;

            this.aLink2.HRef = $"{this.Url}?page={prevM1}";
            this.aLink2.InnerText = prevM1.ToString();
            this.aLink1.HRef = $"{this.Url}?page={prevM2}";
            this.aLink1.InnerText = prevM2.ToString();

            int nextP1 = this.CurrentPage + 1;
            int nextP2 = this.CurrentPage + 2;

            this.aLink4.HRef = $"{this.Url}?page={nextP1}";
            this.aLink4.InnerText = nextP1.ToString();
            this.aLink5.HRef = $"{this.Url}?page={nextP2}";
            this.aLink5.InnerText = nextP2.ToString();

            //this.aLink3.InnerText = this.CurrentPage.ToString();

            //依頁數，決定是否隱藏超連結，並處理提示文字。
            this.aLink1.Visible = (prevM2 > 0);
            this.aLink2.Visible = (prevM1 > 0);
            this.aLink4.Visible = (nextP1 <= totalPage);
            this.aLink5.Visible = (nextP2 <= totalPage);
            
            //if (prevM2 <= 0)
            //    this.aLink1.Visible = false;

            //if (prevM1 <= 0)
            //    this.aLink2.Visible = false;

            //if (nextP1 > totalPage)
            //    this.aLink4.Visible = false;

            //if (nextP2 > totalPage)
            //    this.aLink5.Visible = false;

            this.ltPager.Text = $"共{this.TotalSize}筆，共{totalPage}頁，目前在第{this.GetCurrentPage()}頁<br/>";
        }
    }
}