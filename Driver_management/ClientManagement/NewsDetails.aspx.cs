using BLL; // שימוש במחלקות מהשכבת BLL
using System;
using System.Collections.Generic;
using System.Linq; // שימוש ב-LINQ אם נדרש בעתיד
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; // לשימוש בקונטרולרים של ASP.NET

namespace Driver_management.ClientManagement
{
	public partial class NewsDetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// מנסה לקרוא את מזהה החדשה מהשאילתה של ה-URL
				int newsID;
				if (int.TryParse(Request.QueryString["NewsID"], out newsID))
				{
					// מקבל את פרטי החדשה מהמסד נתונים לפי המזהה
					News news = News.GetById(newsID);
					if (news != null)
					{
						// מציג את פרטי החדשה בעמוד
						NewsTitle.Text = news.NewsTitle;
						NewsContent.Text = news.NewsContent;
					}
					else
					{
						// מציג הודעה במקרה של חדשות שלא נמצאה
						NewsTitle.Text = "חדשה לא נמצאה";
						NewsContent.Text = "החדשה שביקשת לא נמצאה.";
					}
				}
				else
				{
					// מציג הודעה במקרה של מזהה חדש לא חוקי
					NewsTitle.Text = "שגיאה";
					NewsContent.Text = "לא הוזן מזהה חוקי של חדשה.";
				}
			}
		}

	}
}
