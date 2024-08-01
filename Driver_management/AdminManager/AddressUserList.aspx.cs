using System; // ייבוא הספרייה הבסיסית של .NET
using System.Collections.Generic; // ייבוא של רשימות ודומיהן
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Controls כמו DropDownList ו-GridView
using BLL; // ייבוא של השכבת העסקית (Business Logic Layer)

namespace Driver_management.AdminManager
{
	public partial class AddressUserList : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				try
				{
					// Bind user IDs to DropDownList
					List<Drivers> users = Drivers.GetAll(); // קבלת כל הנהגים מה-BLL
					ddlUserIDs.DataSource = users; // הגדרת רשימת הנהגים כמקור הנתונים של ה-DropDownList
					ddlUserIDs.DataTextField = "DriverName"; // שם המאפיין להצגה ב-DropDownList
					ddlUserIDs.DataValueField = "DriverID"; // ערך המאפיין שישמש את הערך הנבחר ב-DropDownList
					ddlUserIDs.DataBind(); // הצגת רשימת הנהגים ב-DropDownList

					// Load addresses for the first user ID by default
					if (users.Count > 0) // בדיקה אם ישנם נהגים ברשימה
					{
						int selectedUserId = users[0].DriverID; // בחירת הנהג הראשון כברירת מחדל
						LoadAddresses(selectedUserId); // טעינת הכתובות עבור הנהג הראשון
					}
				}
				catch (Exception ex) // טיפול בשגיאה במקרה של בעיה בטעינת הנתונים
				{
					// טיפול בשגיאה (רישום או הצגת הודעת שגיאה)
					lblErrorMessage.Text = "שגיאה בטעינת הנתונים: " + ex.Message; // הצגת הודעת שגיאה
				}
			}
		}

		protected void ddlUserIDs_SelectedIndexChanged(object sender, EventArgs e) // אירוע שינוי בחירת נהג
		{
			try
			{
				int selectedUserId = Convert.ToInt32(ddlUserIDs.SelectedValue); // קבלת מזהה הנהג שנבחר מה-DropDownList
				LoadAddresses(selectedUserId); // טעינת הכתובות עבור הנהג שנבחר
			}
			catch (Exception ex) // טיפול בשגיאה במקרה של בעיה בטעינת הכתובות
			{
				// Handle exception (log or display error message)
				lblErrorMessage.Text = "שגיאה בטעינת הכתובות: " + ex.Message; // הצגת הודעת שגיאה
			}
		}

		private void LoadAddresses(int userId) // פונקציה לטעינת כתובות לפי מזהה נהג
		{
			try
			{
				// קריאה לפעולת BLL לקבלת הכתובות לפי DriverID
				List<Shipment> addresses = Shipment.GetShipmentsByUserId(userId); // קבלת רשימת הכתובות מה-BLL

				// קישור הכתובות ל-GridView
				GridViewAddresses.DataSource = addresses; // הגדרת רשימת הכתובות כמקור הנתונים של ה-GridView
				GridViewAddresses.DataBind(); // הצגת הכתובות ב-GridView
			}
			catch (Exception ex) // טיפול בשגיאה במקרה של בעיה בטעינת הכתובות
			{
				// טיפול בשגיאה (רישום או הצגת הודעת שגיאה)
				lblErrorMessage.Text = "שגיאה בטעינת הכתובות: " + ex.Message; // הצגת הודעת שגיאה
			}
		}
	}
}
