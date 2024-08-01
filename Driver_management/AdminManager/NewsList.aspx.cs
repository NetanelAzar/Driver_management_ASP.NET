using BLL; // ייבוא של השכבה העסקית (Business Logic Layer)
using DATA; // ייבוא של השכבת נתונים (Data Access Layer)
using System; // ייבוא של ספריות הבסיס של .NET
using System.Collections.Generic; // ייבוא של רכיבי Collections
using System.Linq; // ייבוא של LINQ
using System.Web; // ייבוא של רכיבי Web Forms
using System.Web.UI; // ייבוא של רכיבי Web Forms
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Forms נוספים

namespace Driver_management.AdminManager
{
	public partial class NewsList : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				BindNewsGrid(); // קישור נתונים לריפיטר
			}
		}

		private void BindNewsGrid()
		{
			// קישור מקור הנתונים ל-RptNews (Repeater)
			rptNews.DataSource = News.GetAll();
			rptNews.DataBind(); // ביצוע דיבינד (עיגון) של הנתונים לריפיטר
		}

		protected void btnDelete_Click(object sender, EventArgs e) // אירוע לחיצה על כפתור מחיקה
		{
			LinkButton btnDelete = (LinkButton)sender; // קבלת הכפתור שנלחץ
			int newsID = int.Parse(btnDelete.CommandArgument); // קבלת ID החדשה מהכפתור

			// מחיקת החדשה
			DeleteNews(newsID);

			// רענון רשימת החדשות
			BindNewsGrid();
		}

		private void DeleteNews(int newsID)
		{
			News news = News.GetById(newsID); // קבלת פרטי החדשה מה-BLL
			if (news != null)
			{
				// ביצוע מחיקת חדשות מה-DAL באמצעות SQL
				DbContext Db = new DbContext();
				string Sql = $"DELETE FROM News WHERE NewsID = {newsID}";
				Db.Execute(Sql); // הרצת פקודת SQL למחיקת החדשה
				Db.Close(); // סגירת החיבור למסד הנתונים
			}
		}
	}
}
