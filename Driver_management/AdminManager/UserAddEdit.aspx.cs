using BLL; // ייבוא של השכבה העסקית (Business Logic Layer)
using DAL; // ייבוא של השכבת נתונים (Data Access Layer)
using System; // ייבוא של ספריות הבסיס של .NET
using System.Collections.Generic; // ייבוא של רכיבי Collections
using System.Linq; // ייבוא של LINQ
using System.Web; // ייבוא של רכיבי Web Forms
using System.Web.UI; // ייבוא של רכיבי Web Forms
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Forms נוספים

namespace Driver_management.AdminManager
{
	public partial class UserAddEdit : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				string UserID = Request["UserID"] + ""; // קבלת מזהה המשתמש מהשאילתא
				if (string.IsNullOrEmpty(UserID))
				{
					UserID = "-1"; // אם לא קיים מזהה משתמש, נניח שזה ID לא חוקי
				}
				else
				{
					int userID = int.Parse(UserID); // המרת מזהה המשתמש למספר שלם
					Drivers Tmp = Drivers.GetById(userID); // קבלת פרטי הנהג לפי מזהה
					if (Tmp != null)
					{
						// אם נמצא נהג, מילוי שדות הטופס בפרטי הנהג
						TxtUserFullName.Text = Tmp.DriverName;
						TxtUserPhone.Text = Tmp.DriverPhone;
						TxtUserMail.Text = Tmp.DriverMail;
						TxtAddress.Text = Tmp.Address;
						TxtPassword.Text = Tmp.DriverPassword;
						TxtMax.Text = Tmp.MaxDeliveries.ToString();
						TxtCityCode.Text = Tmp.CityCode.ToString();
						TxtZoneID.Text = Tmp.zoneID.ToString();
						TxtUserCode.Text = Tmp.DriverCode;
						HidDriverID.Value = UserID; // שמירת מזהה הנהג בסוד
					}
					else
					{
						UserID = "-1"; // אם לא נמצא נהג, נניח שזה ID לא חוקי
					}
				}
			}
		}

		protected void BtnSave_Click(object sender, EventArgs e) // אירוע לחיצה על כפתור שמירה
		{
			Drivers user = new Drivers
			{
				DriverID = int.Parse(HidDriverID.Value), // קבלת מזהה הנהג מהסוד
				DriverName = TxtUserFullName.Text, // קביעת שמו של הנהג
				DriverPhone = TxtUserPhone.Text, // קביעת מספר הטלפון של הנהג
				DriverMail = TxtUserMail.Text, // קביעת דוא"ל הנהג
				Address = TxtAddress.Text, // קביעת כתובת הנהג
				DriverPassword = TxtPassword.Text, // קביעת סיסמת הנהג
				zoneID = TxtZoneID.Text, // קביעת מזהה האזור של הנהג
				DriverCode = TxtUserCode.Text, // קביעת קוד הנהג
				CityCode = int.Parse(TxtCityCode.Text), // קביעת קוד העיר של הנהג
				MaxDeliveries = int.Parse(TxtMax.Text), // קביעת מספר המשלוחים המרבי
				CurrentDeliveries = int.Parse(TxtMax.Text), // קביעת מספר המשלוחים הנוכחי (כנראה טעות כי זה דומה ל-MaxDeliveries)
			};

			user.Save(); // שמירת הפרטים במסד הנתונים

			List<Drivers> LstClient = Drivers.GetAll(); // קבלת רשימת הנהגים המעודכנת
			Application["Drivers"] = LstClient; // עדכון הרשימה באפליקציה

			Response.Redirect("UserList.aspx"); // הפניית המשתמש לדף רשימת הנהגים
		}
	}
}
