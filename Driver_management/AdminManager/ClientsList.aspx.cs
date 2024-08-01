using BLL; // ייבוא של שכבת הלוגיקה העסקית (Business Logic Layer)
using System; // ייבוא הספרייה הבסיסית של .NET
using System.Collections.Generic; // ייבוא של רשימות
using System.Linq; // ייבוא של LINQ
using System.Web.UI; // ייבוא של רכיבי Web Forms
using DATA; // ייבוא של שכבת הגישה לנתונים (Data Access Layer)
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Forms נוספים

namespace Driver_management.AdminManager
{
	public partial class ClientsList : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				LoadClients(); // טעינת רשימת הלקוחות
			}
		}

		private void LoadClients()
		{
			// קבלת רשימת הלקוחות מה-BLL
			List<Client> clients = Client.GetAll(); // קריאה לפונקציה ב-BLL לקבלת כל הלקוחות

			// קביעת מקור הנתונים של ה-Repeater
			RptClients.DataSource = clients; // הגדרת מקור הנתונים לרפיטר
			RptClients.DataBind(); // ביצוע DataBind ל-Repeater
		}

		protected void RptClients_ItemCommand(object source, RepeaterCommandEventArgs e) // אירוע פקודת פריט של Repeater
		{
			if (e.CommandName == "Delete") // בדיקה אם הפקודה היא "Delete"
			{
				int clientId = Convert.ToInt32(e.CommandArgument); // קבלת מזהה הלקוח מהפקודה
				BLL.Client.Delete(clientId); // קריאה לפונקציה ב-BLL למחיקת הלקוח
				LoadClients(); // טעינת רשימת הלקוחות מחדש לאחר מחיקה
			}
		}

		private void DeleteClient(int clientId) // פונקציה למחיקת לקוח
		{
			Client.Delete(clientId); // קריאה לפונקציה ב-BLL למחיקת הלקוח
		}
	}
}
