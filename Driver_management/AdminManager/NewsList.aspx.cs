using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class NewsList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindNewsGrid();
			}
		}

		private void BindNewsGrid()
		{
			rptNews.DataSource = News.GetAll();
			rptNews.DataBind();
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			LinkButton btnDelete = (LinkButton)sender;
			int newsID = int.Parse(btnDelete.CommandArgument);

			// נמחק את החדשה
			DeleteNews(newsID);

			// רענן את רשימת החדשות
			BindNewsGrid();
		}

		private void DeleteNews(int newsID)
		{
			News news = News.GetById(newsID);
			if (news != null)
			{
				// במחיקה נשתמש ב-SQL כדי למחוק את החדשה
				DbContext Db = new DbContext();
				string Sql = $"DELETE FROM News WHERE NewsID = {newsID}";
				Db.Execute(Sql);
				Db.Close();
			}
		}
	}
}