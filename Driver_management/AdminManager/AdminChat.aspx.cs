using BLL; // ייבוא השכבת העסקית (Business Logic Layer)
using DATA; // ייבוא שכבת הנתונים (Data Layer)
using System; // ייבוא הספרייה הבסיסית של .NET
using System.Collections.Generic; // ייבוא של רשימות ודומיהן
using System.Data; // ייבוא של אובייקטים לעבודה עם נתונים
using System.Data.SqlClient; // ייבוא של אובייקטים לעבודה עם SQL Server
using System.Web.UI; // ייבוא של רכיבי Web Forms

namespace Driver_management.AdminManager
{
	public partial class AdminChat : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				LoadCustomers(); // טעינת רשימת הלקוחות
			}
			else
			{
				// Handle PostBack
				string selectedCustomerID = hfSelectedCustomerID.Value; // קבלת מזהה הלקוח הנבחר מה-hidden field
				if (!string.IsNullOrEmpty(selectedCustomerID))
				{
					if (int.TryParse(selectedCustomerID, out int customerId)) // המרת מזהה הלקוח למספר שלם
					{
						LoadMessages(customerId); // טעינת הודעות עבור הלקוח הנבחר
					}
				}
			}
		}

		private void LoadCustomers() // פונקציה לטעינת לקוחות
		{
			List<Client> clients = new List<Client>(); // רשימה לאחסון הלקוחות
			DbContext db = new DbContext(); // יצירת אובייקט DbContext לניהול חיבורי נתונים

			try
			{
				string sql = @"
                    SELECT c.ClientID, c.ClientName
                    FROM Client c
                    INNER JOIN CustomerMessages cm ON c.ClientID = cm.CustomerID
                    GROUP BY c.ClientID, c.ClientName
                ";

				DataTable dt = db.Execute(sql); // הרצת שאילתת SQL לקבלת נתונים

				foreach (DataRow row in dt.Rows) // עבור כל שורה בתוצאות
				{
					Client client = new Client
					{
						ClientID = Convert.ToInt32(row["ClientID"]), // קביעת מזהה הלקוח
						ClientName = row["ClientName"].ToString() // קביעת שם הלקוח
					};

					clients.Add(client); // הוספת הלקוח לרשימה
				}
			}
			catch (Exception ex) // טיפול בשגיאה במקרה של בעיה בטעינת הלקוחות
			{
				System.Diagnostics.Debug.WriteLine("Error loading customers: " + ex.Message); // רישום הודעת שגיאה
			}
			finally
			{
				db.Close(); // סגירת חיבור למסד הנתונים
			}

			rptCustomers.DataSource = clients; // הגדרת רשימת הלקוחות כמקור הנתונים ל-Repeater
			rptCustomers.DataBind(); // הצגת הלקוחות ב-Repeater
		}

		private void LoadMessages(int customerId) // פונקציה לטעינת הודעות עבור לקוח מסוים
		{
			List<CustomerMessage> messages = new List<CustomerMessage>(); // רשימה לאחסון הודעות
			DbContext db = new DbContext(); // יצירת אובייקט DbContext לניהול חיבורי נתונים

			try
			{
				string sql = "SELECT * FROM CustomerMessages WHERE CustomerID = @CustomerID ORDER BY SentDate";
				using (SqlCommand cmd = new SqlCommand(sql, db.Conn)) // יצירת אובייקט SqlCommand להרצת שאילתת SQL
				{
					cmd.Parameters.AddWithValue("@CustomerID", customerId); // הוספת פרמטר לשאילתה

					SqlDataAdapter da = new SqlDataAdapter(cmd); // יצירת SqlDataAdapter לקריאה מהמסד
					DataTable dt = new DataTable(); // יצירת DataTable לאחסון התוצאות
					da.Fill(dt); // מילוי ה-DataTable בתוצאות השאילתה

					foreach (DataRow row in dt.Rows) // עבור כל שורה בתוצאות
					{
						CustomerMessage message = new CustomerMessage
						{
							MessageID = Convert.ToInt32(row["MessageID"]), // קביעת מזהה ההודעה
							CustomerID = Convert.ToInt32(row["CustomerID"]), // קביעת מזהה הלקוח
							MessageText = row["MessageText"].ToString(), // קביעת תוכן ההודעה
							SentDate = Convert.ToDateTime(row["SentDate"]), // קביעת תאריך שליחת ההודעה
							IsFromCustomer = Convert.ToBoolean(row["IsFromCustomer"]) // קביעת האם ההודעה נכתבה על ידי הלקוח
						};

						messages.Add(message); // הוספת ההודעה לרשימה
					}
				}
			}
			catch (Exception ex) // טיפול בשגיאה במקרה של בעיה בטעינת ההודעות
			{
				System.Diagnostics.Debug.WriteLine("Error loading messages: " + ex.Message); // רישום הודעת שגיאה
			}
			finally
			{
				db.Close(); // סגירת חיבור למסד הנתונים
			}

			rptMessages.DataSource = messages; // הגדרת רשימת ההודעות כמקור הנתונים ל-Repeater
			rptMessages.DataBind(); // הצגת ההודעות ב-Repeater
		}

		protected void btnPostBack_Click(object sender, EventArgs e) // אירוע לחיצה על כפתור לשליחה מחדש
		{
			string selectedCustomerID = hfSelectedCustomerID.Value; // קבלת מזהה הלקוח הנבחר מה-hidden field
			System.Diagnostics.Debug.WriteLine("Selected CustomerID: " + selectedCustomerID); // רישום מזהה הלקוח הנבחר

			if (!string.IsNullOrEmpty(selectedCustomerID))
			{
				if (int.TryParse(selectedCustomerID, out int customerId)) // המרת מזהה הלקוח למספר שלם
				{
					LoadMessages(customerId); // טעינת הודעות עבור הלקוח הנבחר
				}
			}
		}

		protected void btnSendMessage_Click(object sender, EventArgs e) // אירוע לחיצה על כפתור שליחת הודעה
		{
			string selectedCustomerID = hfSelectedCustomerID.Value; // קבלת מזהה הלקוח הנבחר מה-hidden field
			string newMessageText = txtNewMessage.Text; // קבלת תוכן ההודעה מהטקסטבוקס

			if (!string.IsNullOrEmpty(selectedCustomerID) && !string.IsNullOrEmpty(newMessageText))
			{
				if (int.TryParse(selectedCustomerID, out int customerId)) // המרת מזהה הלקוח למספר שלם
				{
					DbContext db = new DbContext(); // יצירת אובייקט DbContext לניהול חיבורי נתונים
					try
					{
						string sql = "INSERT INTO CustomerMessages (CustomerID, MessageText, SentDate, IsFromCustomer) VALUES (@CustomerID, @MessageText, @SentDate, 0)";
						using (SqlCommand cmd = new SqlCommand(sql, db.Conn)) // יצירת אובייקט SqlCommand להרצת שאילתת SQL
						{
							cmd.Parameters.AddWithValue("@CustomerID", customerId); // הוספת פרמטר לשאילתה
							cmd.Parameters.AddWithValue("@MessageText", newMessageText); // הוספת תוכן ההודעה
							cmd.Parameters.AddWithValue("@SentDate", DateTime.Now); // הוספת תאריך שליחת ההודעה

							cmd.ExecuteNonQuery(); // ביצוע השאילתה
						}

						// Optionally, reload messages after sending
						LoadMessages(customerId); // טעינת הודעות לאחר שליחה
						txtNewMessage.Text = ""; // ניקוי הטקסטבוקס
					}
					catch (Exception ex) // טיפול בשגיאה במקרה של בעיה בשליחה
					{
						System.Diagnostics.Debug.WriteLine("Error sending message: " + ex.Message); // רישום הודעת שגיאה
					}
					finally
					{
						db.Close(); // סגירת חיבור למסד הנתונים
					}
				}
			}
		}
	}
}
