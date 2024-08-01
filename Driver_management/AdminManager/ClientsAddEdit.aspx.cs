using System; // ייבוא הספרייה הבסיסית של .NET
using System.Web.UI; // ייבוא של רכיבי Web Forms
using BLL; // ייבוא של שכבת הלוגיקה העסקית (Business Logic Layer)
using DAL; // ייבוא של שכבת הגישה לנתונים (Data Access Layer)

namespace Driver_management.AdminManager
{
	public partial class ClientsAddEdit : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				// Check if there's a client ID in query string
				string clientID = Request["ClientID"]; // קבלת מזהה הלקוח ממחרוזת השאילתה (Query String)
				if (!string.IsNullOrEmpty(clientID)) // בדיקה אם המזהה אינו ריק
				{
					if (int.TryParse(clientID, out int parsedClientID)) // ניסיון להמיר את המזהה למספר שלם
					{
						// Load client details
						Client client = Client.GetById(parsedClientID); // קריאה לפונקציה ב-BLL לקבלת פרטי הלקוח
						if (client != null) // אם הלקוח נמצא
						{
							HidClientID.Value = clientID; // הגדרת מזהה הלקוח ב-hidden field
							TxtClientName.Text = client.ClientName; // מילוי פרטי הלקוח בשדות הקלט
							TxtClientMail.Text = client.ClientMail;
							TxtClientPhone.Text = client.ClientPhone;
							TxtCompanyName.Text = client.CompanyName;
							TxtCityCode.Text = client.CityCode;
							TxtAddress.Text = client.Address;
						}
					}
				}
			}
		}

		protected void BtnSave_Click(object sender, EventArgs e) // אירוע לחיצה על כפתור שמירה
		{
			Client client = new Client(); // יצירת אובייקט לקוח חדש

			// Populate client object with form data
			client.ClientID = int.Parse(HidClientID.Value); // הגדרת מזהה הלקוח
			client.ClientName = TxtClientName.Text; // מילוי פרטי הלקוח מהטופס
			client.ClientMail = TxtClientMail.Text;
			client.ClientPhone = TxtClientPhone.Text;
			client.CompanyName = TxtCompanyName.Text;
			client.CityCode = TxtCityCode.Text;
			client.Address = TxtAddress.Text;

			// Save or update the client
			client.Save(); // קריאה לפונקציה ב-BLL לשמירה או עדכון של הלקוח

			// Update application-level variable
			Application["Clients"] = Client.GetAll(); // עדכון משתנה ברמת היישום עם רשימת כל הלקוחות

			// Redirect to client list page
			Response.Redirect("ClientsList.aspx"); // הפניה לדף רשימת הלקוחות
		}
	}
}
