using BLL; // שימוש במחלקות מהשכבת BLL
using DAL; // שימוש במחלקות מהשכבת DAL
using DATA; // שימוש במחלקות מהשכבת DATA
using Stripe; // שימוש ב-SDK של Stripe
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // שימוש ב-SQL Server
using System.Linq; // הוספת LINQ לסינון ההזמנות
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization; // למרת JSON
using System.Web.Services; // לשירותי אינטרנט
using System.Web.UI; // לשימוש בעמודי ASP.NET

namespace Driver_management.ClientManagement
{
	public partial class ClientHome : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// טוען הזמנות חדשות וחדשות אם העמוד נטען בפעם הראשונה בלבד
				LoadOrders();
				BindNews();
			}

			if (Session["Login"] is Client loggedInClient)
			{
				// מציג את שם הלקוח המחובר
				lblUsername.Text = loggedInClient.ClientName;
			}
		}

		private void BindNews()
		{
			// טוען את כל החדשות ומקשר אותן לרפיטר
			List<News> newsList = News.GetAll();
			rptNews.DataSource = newsList;
			rptNews.DataBind();
		}

		private void LoadOrders()
		{
			// מקבל את פרטי הלקוח המחובר מהסשן
			Client loggedInClient = Session["Login"] as Client;

			if (loggedInClient == null)
			{
				// טיפול במקרה שבו פרטי הלקוח לא נמצאים
				Response.Write("Error: Logged-in client information not found.");
				return;
			}

			// מקבל את ההזמנות עבור הלקוח המחובר
			List<Shipment> orders = BLL.Shipment.GetShipmentsByCustomerId(loggedInClient.ClientID);
			orders.Reverse(); // הופך את סדר ההזמנות

			// מסנן את ההזמנות לפי סטטוס שאינו "נמסר"
			var filteredOrders = orders.Where(order => order.ShippingStatus != "נמסר").ToList();

			// מקשר את ההזמנות לספק רפיטר
			RptOrders.DataSource = filteredOrders;
			RptOrders.DataBind();
		}

		// עוזר לשיטה להחזיר את שם העיר לפי מזהה
		protected string GetCityNameById(object cityIdObj)
		{
			if (cityIdObj == null || cityIdObj == DBNull.Value)
				return string.Empty;

			int cityId;
			if (int.TryParse(cityIdObj.ToString(), out cityId))
			{
				City city = City.GetById(cityId); // הנחה שמטרת המתודה קיימת ב-BLL
				if (city != null)
				{
					return city.CityName;
				}
			}
			return string.Empty;
		}

		// פונקציה לעיצוב התאריך
		public string FormatDate(object date)
		{
			if (date != null && DateTime.TryParse(date.ToString(), out DateTime dt))
			{
				return dt.ToString("dd/MM/yyyy"); // עיצוב התאריך בפורמט יום/חודש/שנה
			}
			return string.Empty;
		}

		[WebMethod]
		public static string SendMessage(string message)
		{
			int customerId = GetLoggedInCustomerId();
			DbContext db = new DbContext();

			try
			{
				if (customerId == -1)
				{
					return "Error: Customer is not logged in.";
				}

				string sql = $"INSERT INTO CustomerMessages (CustomerID, MessageText, SentDate, IsFromCustomer) " +
							 $"VALUES ({customerId}, N'{message}', GETDATE(), 1)";

				db.Execute(sql); // מבצע שאילתה להוספת הודעה

				return "Message sent successfully.";
			}
			catch (Exception ex)
			{
				// רושם את השגיאה או כותב לקונסול
				Console.WriteLine("Error: " + ex.Message);
				return "Error: " + ex.Message;
			}
			finally
			{
				db.Close();
			}
		}

		[WebMethod]
		public static string GetMessages()
		{
			int customerId = GetLoggedInCustomerId();
			List<CustomerMessage> messages = new List<CustomerMessage>();

			string sql = $"SELECT MessageText, SentDate, IsFromCustomer FROM CustomerMessages WHERE CustomerID = {customerId} ORDER BY SentDate ASC";
			DbContext db = new DbContext();

			try
			{
				DataTable dt = db.Execute(sql); // מבצע שאילתה לקבלת הודעות
				foreach (DataRow row in dt.Rows)
				{
					messages.Add(new CustomerMessage
					{
						MessageText = row["MessageText"].ToString(),
						SentDate = Convert.ToDateTime(row["SentDate"]),
						IsFromCustomer = Convert.ToBoolean(row["IsFromCustomer"])
					});
				}
				db.Close();
				return new JavaScriptSerializer().Serialize(messages); // מחזיר את ההודעות בפורמט JSON
			}
			catch (Exception ex)
			{
				// רושם את השגיאה
				Console.WriteLine("Error: " + ex.Message);
				return new JavaScriptSerializer().Serialize(new { error = "Error: " + ex.Message });
			}
			finally
			{
				db.Close();
			}
		}

		// פונקציה להחזיר את מזהה הלקוח המחובר
		private static int GetLoggedInCustomerId()
		{
			Client loggedInClient = HttpContext.Current.Session["Login"] as Client;
			if (loggedInClient == null)
			{
				Console.WriteLine("Error: No client is logged in.");
			}
			return loggedInClient != null ? loggedInClient.ClientID : -1;
		}
	}
}
