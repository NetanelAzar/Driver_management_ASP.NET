using BLL; // ייבוא של השכבה העסקית (Business Logic Layer)
using System; // ייבוא של ספריות הבסיס של .NET
using System.Collections.Generic; // ייבוא של רכיבי Collections
using System.Linq; // ייבוא של LINQ
using System.Web; // ייבוא של רכיבי Web Forms
using System.Web.UI; // ייבוא של רכיבי Web Forms
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Forms נוספים

namespace Driver_management.AdminManager
{
	public partial class ManageNews : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				int newsID;
				if (int.TryParse(Request.QueryString["NewsID"], out newsID))
				{
					// אם יש ID חדשות, טוען חדשות לעריכה
					LoadNews(newsID);
				}
			}
		}

		private void LoadNews(int newsID)
		{
			News news = News.GetById(newsID); // קבלת פרטי החדשות מה-BLL
			if (news != null)
			{
				txtTitle.Text = news.NewsTitle; // הצגת כותרת החדשות
				txtSummary.Text = news.NewsSummary; // הצגת תקציר החדשות
				txtContent.Text = news.NewsContent; // הצגת תוכן החדשות
			}
		}

		protected void btnSave_Click(object sender, EventArgs e) // אירוע לחיצה על כפתור שמירה
		{
			News news;
			int newsID;
			if (int.TryParse(Request.QueryString["NewsID"], out newsID))
			{
				// עדכון חדשות קיימות
				news = News.GetById(newsID);
				if (news != null)
				{
					news.NewsTitle = txtTitle.Text; // עדכון כותרת
					news.NewsSummary = txtSummary.Text; // עדכון תקציר
					news.NewsContent = txtContent.Text; // עדכון תוכן
					news.NewsDate = DateTime.Now; // עדכון תאריך

					// קריאה לשיטה הסטטית עם האובייקט
					News.Save(news);
				}
			}
			else
			{
				// הוספת חדשות חדשות
				news = new News
				{
					NewsTitle = txtTitle.Text, // הגדרת כותרת
					NewsSummary = txtSummary.Text, // הגדרת תקציר
					NewsContent = txtContent.Text, // הגדרת תוכן
					NewsDate = DateTime.Now // הגדרת תאריך
				};

				// קריאה לשיטה הסטטית עם האובייקט החדש
				News.Save(news);
			}
			Response.Redirect("NewsList.aspx"); // הפניית המשתמש לדף רשימת חדשות לאחר השמירה
		}
	}
}
