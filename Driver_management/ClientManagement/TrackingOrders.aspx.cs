using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.ClientManagement
{
	public partial class TrackingOrders : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadOrders();
			}
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

			// Bind orders to Repeater
			RptOrders.DataSource = orders;
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
				City city = BLL.City.GetById(cityId); // Assuming this method exists in BLL
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