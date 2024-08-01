using BLL;
using DATA; // ייבוא שכבת הנתונים (Data Layer)
using System; // ייבוא הספרייה הבסיסית של .NET
using System.Collections.Generic; // ייבוא של רשימות ודומיהן
using System.Data; // ייבוא של אובייקטים לעבודה עם נתונים
using System.Data.SqlClient;
using System.Web.Script.Serialization; // ייבוא של אובייקט JSON Serializer
using System.Web.UI; // ייבוא של רכיבי Web Forms

namespace Driver_management.AdminManager
{
	public partial class AdminHomePage : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			// Update active users count only if not a postback
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				// Initialize session if needed
				if (Session["Login"] == null) // אם אין מידע על כניסות כרגע
				{
					Session["Login"] = new List<string>(); // יצירת רשימה חדשה למעקב אחרי משתמשים מחוברים
				}

				// Update label with active users count
				lblActiveUsersCount.Text = GetActiveUsersCount().ToString(); // הצגת מספר המשתמשים הפעילים
				LoadMonthlyShipments(); // טעינת נתוני השילוחים החודשיים
				LoadNotifications(); // טעינת התראות
				LoadDailyShipments();
				LoadDailyOrdersCount(); // הוספת שורה זו
			}
		}

		private void LoadMonthlyShipments() // פונקציה לטעינת נתוני שילוחים חודשיים
		{
			DbContext Db = new DbContext(); // יצירת אובייקט DbContext לניהול חיבורי נתונים
			int currentYear = DateTime.Now.Year; // קבלת השנה הנוכחית

			string Sql = $@"SELECT MONTH(OrderDate) AS Month, COUNT(*) AS ShipmentCount 
                    FROM Shipments 
                    WHERE YEAR(OrderDate) = {currentYear} 
                    GROUP BY MONTH(OrderDate) 
                    ORDER BY MONTH(OrderDate)";

			DataTable Dt = Db.Execute(Sql); // הרצת השאילתה לקבלת נתונים

			List<int> months = new List<int>(); // רשימה לחודשים
			List<int> shipmentCounts = new List<int>(); // רשימה למספרי השילוחים

			foreach (DataRow row in Dt.Rows) // עבור כל שורה בתוצאות
			{
				int month = Convert.ToInt32(row["Month"]); // קבלת החודש
				int count = Convert.ToInt32(row["ShipmentCount"]); // קבלת מספר השילוחים

				// עבור כל חודש, הדפס את הנתונים למעקב
				Console.WriteLine($"Month: {month}, ShipmentCount: {count}");

				months.Add(month); // הוספת חודש לרשימה
				shipmentCounts.Add(count); // הוספת מספר שילוחים לרשימה
			}

			JavaScriptSerializer serializer = new JavaScriptSerializer(); // יצירת אובייקט Serializer להמרת נתונים ל-JSON
			string monthsJson = serializer.Serialize(months); // המרת רשימת החודשים ל-JSON
			string shipmentCountsJson = serializer.Serialize(shipmentCounts); // המרת רשימת מספרי השילוחים ל-JSON

			ClientScript.RegisterStartupScript(this.GetType(), "loadChart",
				$"window.onload = function() {{ loadChart({monthsJson}, {shipmentCountsJson}); }};", // הוספת קוד JS להרצת פונקציית chart בטעינת הדף
				true);
		}

		public void UserLogin(string username) // פונקציה להוספת משתמש לרשימת המשתמשים המחוברים
		{
			var loggedInUsers = Session["Login"] as List<string>; // קבלת רשימת המשתמשים המחוברים מה-session

			if (loggedInUsers == null) // אם הרשימה לא קיימת
			{
				loggedInUsers = new List<string>(); // יצירת רשימה חדשה
				Session["Login"] = loggedInUsers; // עדכון ה-session
			}

			if (!loggedInUsers.Contains(username)) // אם המשתמש לא ברשימה
			{
				loggedInUsers.Add(username); // הוספת המשתמש לרשימה
			}

			// Debugging output to trace issue
			System.Diagnostics.Debug.WriteLine($"User logged in: {username}. Total active users: {loggedInUsers.Count}"); // רישום הודעת שגיאה
		}

		public void UserLogout(string username) // פונקציה להסרת משתמש מרשימת המשתמשים המחוברים
		{
			var loggedInUsers = Session["Login"] as List<string>; // קבלת רשימת המשתמשים המחוברים מה-session

			if (loggedInUsers != null) // אם הרשימה קיימת
			{
				loggedInUsers.Remove(username); // הסרת המשתמש מהרשימה
			}

			// Debugging output to trace issue
			System.Diagnostics.Debug.WriteLine($"User logged out: {username}. Total active users: {loggedInUsers?.Count ?? 0}"); // רישום הודעת שגיאה
		}

		public int GetActiveUsersCount() // פונקציה לקבלת מספר המשתמשים הפעילים
		{
			var activeUsers = Application["Login"] as List<string>; // קבלת רשימת המשתמשים הפעילים מה-Application
			return activeUsers != null ? activeUsers.Count : 0; // החזרת מספר המשתמשים הפעילים
		}

		private void LoadNotifications() // פונקציה לטעינת התראות
		{
			var notifications = Session["LoginNotifications"] as List<string> ?? new List<string>(); // קבלת התראות מה-session או יצירת רשימה ריקה

			rptNotifications.DataSource = notifications; // הגדרת מקור הנתונים ל-Repeater
			rptNotifications.DataBind(); // הצגת ההתראות ב-Repeater
		}

		protected void rptNotifications_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e) // אירוע עבור פעולות ב-Repeater
		{

		}











		private void LoadDailyOrdersCount()
		{
			DbContext Db = new DbContext();
			DateTime today = DateTime.Now.Date;

			string Sql = $@"SELECT COUNT(*) AS OrderCount
                    FROM Shipments
                    WHERE OrderDate = '{today:yyyy-MM-dd}'";

			DataTable Dt = Db.Execute(Sql);

			int orderCount = 0;
			if (Dt.Rows.Count > 0)
			{
				orderCount = Convert.ToInt32(Dt.Rows[0]["OrderCount"]);
			}

			lblDailyOrdersCount.Text = orderCount.ToString();
		}





















		private void LoadDailyShipments()
		{
			DbContext Db = new DbContext();
			DateTime today = DateTime.Now.Date;

			string Sql = $@"SELECT ShipmentID, OrderDate, DestinationAddress, DestinationCity, NumberOfPackages, Payment
                    FROM Shipments
                    WHERE OrderDate = '{today:yyyy-MM-dd}'";

			DataTable Dt = Db.Execute(Sql);

			List<Shipment> shipments = new List<Shipment>();

			foreach (DataRow row in Dt.Rows)
			{
				var shipment = new Shipment
				{
					ShipmentID = row.IsNull("ShipmentID") ? 0 : Convert.ToInt32(row["ShipmentID"]),
					DestinationAddress = row.IsNull("DestinationAddress") ? string.Empty : Convert.ToString(row["DestinationAddress"]),
					DestinationCity = row.IsNull("DestinationCity") ? 0 : Convert.ToInt32(row["DestinationCity"]),
					OrderDate = row.IsNull("OrderDate") ? DateTime.MinValue : Convert.ToDateTime(row["OrderDate"]),
					NumberOfPackages = row.IsNull("NumberOfPackages") ? 0 : Convert.ToInt32(row["NumberOfPackages"]),
					Payment = row.IsNull("Payment") ? 0m : Convert.ToDecimal(row["Payment"])
				};

				shipments.Add(shipment);
			}

			rptShipments.DataSource = shipments;
			rptShipments.DataBind();
		}







	}
}
