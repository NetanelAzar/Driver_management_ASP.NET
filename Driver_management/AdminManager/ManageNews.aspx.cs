using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class ManageNews : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				int newsID;
				if (int.TryParse(Request.QueryString["NewsID"], out newsID))
				{
					// עריכת חדשות קיימות
					LoadNews(newsID);
				}
			}
		}

		private void LoadNews(int newsID)
		{
			News news = News.GetById(newsID);
			if (news != null)
			{
				txtTitle.Text = news.NewsTitle;
				txtSummary.Text = news.NewsSummary;
				txtContent.Text = news.NewsContent;
				
			
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			News news;
			int newsID;
			if (int.TryParse(Request.QueryString["NewsID"], out newsID))
			{
				// עדכון חדשות קיימות
				news = News.GetById(newsID);
				if (news != null)
				{
					news.NewsTitle = txtTitle.Text;
					news.NewsSummary = txtSummary.Text;
					news.NewsContent = txtContent.Text;
					news.NewsDate = DateTime.Now;

					// קריאה לשיטה הסטטית עם הארגומנט הנכון
					News.Save(news);
				}
			}
			else
			{
				// הוספת חדשות חדשות
				news = new News
				{
					NewsTitle = txtTitle.Text,
					NewsSummary = txtSummary.Text,
					NewsContent = txtContent.Text,
					NewsDate = DateTime.Now
				};

				// קריאה לשיטה הסטטית עם הארגומנט הנכון
				News.Save(news);
			}
			Response.Redirect("NewsList.aspx");
		}

	}
}