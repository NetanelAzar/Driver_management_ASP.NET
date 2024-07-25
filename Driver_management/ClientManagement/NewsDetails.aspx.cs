using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.ClientManagement
{
	public partial class NewsDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				int newsID;
				if (int.TryParse(Request.QueryString["NewsID"], out newsID))
				{
					News news = News.GetById(newsID);
					if (news != null)
					{
						NewsTitle.Text = news.NewsTitle;
						NewsContent.Text = news.NewsContent;
					}
					else
					{
						// Handle news not found case
						NewsTitle.Text = "חדשה לא נמצאה";
						NewsContent.Text = "החדשה שביקשת לא נמצאה.";
					}
				}
				else
				{
					// Handle invalid NewsID
					NewsTitle.Text = "שגיאה";
					NewsContent.Text = "לא הוזן מזהה חוקי של חדשה.";
				}
			}
		}

	}
}