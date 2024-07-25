using BLL;
using DAL;
using DATA;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq; // הוספת LINQ לסינון ההזמנות
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

namespace Driver_management.ClientManagement
{
	public partial class ClientHome : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadOrders();
				BindNews();
			}

			if (Session["Login"] is Client loggedInClient)
			{
				lblUsername.Text = loggedInClient.ClientName;


			}
		}



		private void BindNews()
		{
			List<News> newsList = News.GetAll();
			rptNews.DataSource = newsList;
			rptNews.DataBind();
		}


		private void LoadOrders()
		{
			// Retrieve logged-in client information from session or wherever it's stored
			Client loggedInClient = Session["Login"] as Client;

			if (loggedInClient == null)
			{
				// Handle case where logged-in client is not found
				Response.Write("Error: Logged-in client information not found.");
				return;
			}

			// Fetch orders for the logged-in client
			List<Shipment> orders = BLL.Shipment.GetShipmentsByCustomerId(loggedInClient.ClientID);
			orders.Reverse();

			// Filter out orders with status "נמסר"
			var filteredOrders = orders.Where(order => order.ShippingStatus != "נמסר").ToList();

			// Bind orders to Repeater
			RptOrders.DataSource = filteredOrders;
			RptOrders.DataBind();
		}

		// Helper method to format city names
		protected string GetCityNameById(object cityIdObj)
		{
			if (cityIdObj == null || cityIdObj == DBNull.Value)
				return string.Empty;

			int cityId;
			if (int.TryParse(cityIdObj.ToString(), out cityId))
			{
				City city = City.GetById(cityId); // Assuming this method exists in BLL
				if (city != null)
				{
					return city.CityName;
				}
			}
			return string.Empty;
		}

		// Function to format date
		public string FormatDate(object date)
		{
			if (date != null && DateTime.TryParse(date.ToString(), out DateTime dt))
			{
				return dt.ToString("dd/MM/yyyy");
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

				db.Execute(sql);

				return "Message sent successfully.";
			}
			catch (Exception ex)
			{
				// Log the exception or write to console
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
				DataTable dt = db.Execute(sql);
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
				return new JavaScriptSerializer().Serialize(messages);
			}
			catch (Exception ex)
			{
				// Log the exception
				Console.WriteLine("Error: " + ex.Message);
				return new JavaScriptSerializer().Serialize(new { error = "Error: " + ex.Message });
			}
			finally
			{
				db.Close();
			}
		}







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

