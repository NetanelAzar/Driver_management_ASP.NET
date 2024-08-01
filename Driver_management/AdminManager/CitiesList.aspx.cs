using BLL; // ייבוא של שכבת הלוגיקה העסקית (Business Logic Layer)
using System; // ייבוא הספרייה הבסיסית של .NET
using System.Collections.Generic; // ייבוא של רשימות ודומיהן
using System.Linq; // ייבוא של LINQ לפעולות על אוספים
using System.Web.UI; // ייבוא של רכיבי Web Forms
using System.Web.UI.WebControls; // ייבוא של רכיבי בקרה עבור Web Forms

namespace Driver_management.AdminManager
{
	public partial class CitiesList : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				LoadCities(); // טעינת רשימת הערים
			}
		}

		private void LoadCities() // פונקציה לטעינת רשימת הערים
		{
			// קבלת רשימת הערים מה-BLL
			List<City> cities = City.GetAll(); // קריאה לפונקציה ב-BLL לקבלת כל הערים

			// מיון הערים לפי שם העיר
			cities = cities.OrderBy(c => c.CityName).ToList(); // מיון הערים לפי שם העיר באלפבית

			// קביעת מקור הנתונים של ה-Repeater
			RptCities.DataSource = cities; // הגדרת רשימת הערים כמקור נתונים ל-Repeater
			RptCities.DataBind(); // עדכון ה-Repeater להצגת הערים
		}

		protected void RptCities_ItemCommand(object source, RepeaterCommandEventArgs e) // אירוע עבור פקודות ב-Repeater
		{
			if (e.CommandName == "Delete") // בדיקה אם הפקודה היא מחיקת עיר
			{
				int cityId = Convert.ToInt32(e.CommandArgument); // קבלת מזהה העיר מהפקודה
				DeleteCity(cityId); // קריאה לפונקציה למחיקת העיר
				LoadCities(); // טעינת רשימת הערים מחדש
			}
		}

		private void DeleteCity(int cityId) // פונקציה למחיקת עיר
		{
			City.Delete(cityId); // קריאה לפונקציה ב-BLL למחיקת העיר
		}

		// פונקציה לעיצוב תאריך
		public string FormatDate(object date) // פונקציה לעיצוב תאריך בפורמט "dd/MM/yyyy"
		{
			if (date != null && DateTime.TryParse(date.ToString(), out DateTime dt)) // בדיקה אם התאריך ניתן להמרה ל-DateTime
			{
				return dt.ToString("dd/MM/yyyy"); // החזרת התאריך בפורמט המיועד
			}
			return string.Empty; // החזרת מיתר ריק אם התאריך אינו תקין
		}
	}
}
