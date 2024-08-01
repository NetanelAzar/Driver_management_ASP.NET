using BLL; // ייבוא של השכבת העסקית (Business Logic Layer)
using System; // ייבוא הספרייה הבסיסית של .NET
using System.Collections.Generic; // ייבוא של רשימות ודומיהן
using System.Linq; // ייבוא של פונקציות LINQ
using System.Web.UI; // ייבוא של רכיבי UI של ASP.NET
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Controls כמו Repeater ו-DropDownList

namespace Driver_management.AdminManager
{
	public partial class AddressList : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				// הגדרת החודש הנוכחי כברירת מחדל
				int currentMonth = DateTime.Now.Month; // קבלת החודש הנוכחי מהתאריך הנוכחי
				ddlMonths.SelectedValue = currentMonth.ToString(); // הגדרת החודש הנבחר ב-DropDownList

				LoadAddresses(currentMonth); // טעינת הכתובות עבור החודש הנוכחי
			}
		}

		protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e) // אירוע שינוי בחירת חודש
		{
			int selectedMonth = int.Parse(ddlMonths.SelectedValue); // קבלת החודש הנבחר מה-DropDownList
			LoadAddresses(selectedMonth); // טעינת הכתובות עבור החודש שנבחר
		}

		private void LoadAddresses(int month) // פונקציה לטעינת כתובות לפי חודש
		{
			// קבלת רשימת המשלוחים לפי החודש הנבחר
			List<Shipment> shipments = Shipment.GetByMonth(month); // קבלת המשלוחים מה-BLL

			// מיון המשלוחים לפי ערים
			shipments = shipments.OrderBy(s => s.DestinationCity).ToList(); // מיון רשימת המשלוחים לפי עיר היעד

			// קביעת מקור הנתונים של ה-Repeater
			RptAddresses.DataSource = shipments; // הגדרת רשימת המשלוחים כמקור הנתונים של ה-Repeater
			RptAddresses.DataBind(); // הצגת המשלוחים ב-Repeater
		}

		protected void RptAddresses_ItemCommand(object source, RepeaterCommandEventArgs e) // אירוע קליק על פריט ב-Repeater
		{
			if (e.CommandName == "Delete") // בדיקה אם הפקודה היא מחיקה
			{
				int shipmentId = Convert.ToInt32(e.CommandArgument); // קבלת מזהה המשלוח מהפקודה
				DeleteShipment(shipmentId); // קריאה לפונקציה למחיקת המשלוח
				LoadAddresses(int.Parse(ddlMonths.SelectedValue)); // טעינת הכתובות מחדש עם החודש הנבחר
			}
		}

		private void DeleteShipment(int shipmentId) // פונקציה למחיקת משלוח
		{
			Shipment.Delete(shipmentId); // קריאה לפונקציה למחיקת המשלוח מה-BLL
		}

		// פונקציה סטטית להחזרת שם עיר
		public static string GetCityNameById(object cityId) // פונקציה לקבלת שם העיר לפי מזהה
		{
			if (cityId != null && int.TryParse(cityId.ToString(), out int id)) // בדיקת אם מזהה העיר אינו ריק וניתן להמירו למספר שלם
			{
				return City.GetCityNameById(id); // קבלת שם העיר לפי מזהה מה-BLL
			}
			return string.Empty; // החזרת מיתר ריק אם העיר לא נמצאה
		}

		// פונקציה לעיצוב תאריך
		public string FormatDate(object date) // פונקציה לעיצוב התאריך
		{
			if (date != null && DateTime.TryParse(date.ToString(), out DateTime dt)) // בדיקת אם התאריך אינו ריק וניתן להמירו לתאריך
			{
				return dt.ToString("dd/MM/yyyy"); // החזרת התאריך בעיצוב dd/MM/yyyy
			}
			return string.Empty; // החזרת מיתר ריק אם התאריך לא תקין
		}
	}
}
