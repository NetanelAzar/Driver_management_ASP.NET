using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq; // הוספת LINQ לסינון ההזמנות
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
	}
}
