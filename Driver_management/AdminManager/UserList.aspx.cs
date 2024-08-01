using BLL; // ייבוא של השכבה העסקית (Business Logic Layer)
using System; // ייבוא של ספריות הבסיס של .NET
using System.Collections.Generic; // ייבוא של רכיבי Collections
using System.Web.UI; // ייבוא של רכיבי Web Forms
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Forms נוספים

namespace Driver_management.AdminManager
{
	public partial class UserList : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				LoadUsers(); // טעינת רשימת הנהגים
			}
		}

		private void LoadUsers()
		{
			// קבלת רשימת הנהגים מה-Application
			List<Drivers> LstUser = (List<Drivers>)Application["Drivers"];
			RptUser.DataSource = LstUser; // קביעת מקור הנתונים ל-Repeater
			RptUser.DataBind(); // עידכון ה-Repeater עם הנתונים
		}

	}
}
