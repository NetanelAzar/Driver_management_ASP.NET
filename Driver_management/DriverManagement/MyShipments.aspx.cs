using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.DriverManagement
{
	public partial class MyShipments : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadMyShipments();
			}
		}

		protected void BtnConfirmDelivery_Click(object sender, EventArgs e)
		{
			Button btnConfirmDelivery = (Button)sender;
			int shipmentId = Convert.ToInt32(btnConfirmDelivery.CommandArgument);

			// Update shipment status to "Delivered"
			string Status = "נמסר";
			Shipment.UpdateDeliveryStatus(shipmentId, Status);

			// Update delivery date to current date
			DateTime deliveryDate = DateTime.Now;
			ShipmentDAL.UpdateDeliveryDate(shipmentId, deliveryDate);

			// Refresh the list after update
			LoadMyShipments();
		}

		private void LoadMyShipments()
		{
			int userId = GetCurrentUserId();
			if (userId > 0)
			{
				List<Shipment> myShipments = Shipment.GetShipmentsByUserIdSortedByOrderDateDesc(userId);
				rptMyShipments.DataSource = myShipments;
				rptMyShipments.DataBind();
			}
			else
			{
				Response.Redirect("~/LoginRegister.aspx");

			}
		}


		protected void BtnConfirmPickup_Click(object sender, EventArgs e)
		{
			Button btnConfirmPickup = (Button)sender;
			int shipmentId = Convert.ToInt32(btnConfirmPickup.CommandArgument);

			// Update shipment status to "On the way to destination"
			Shipment.UpdateDeliveryStatus(shipmentId, "נאסף על ידי השליח");

			// Update pickup date to current date
			DateTime pickupDate = DateTime.Now;
			ShipmentDAL.UpdatePickupDate(shipmentId, pickupDate);



			// Refresh the list after update
			LoadMyShipments();
		}

		protected void btnSendWhatsApp_Click(object sender, EventArgs e)
		{
			Button btnSendWhatsApp = (Button)sender;
			string customerPhone = btnSendWhatsApp.CommandArgument;

			// Create WhatsApp message link
			string whatsappLink = $"https://wa.me/{customerPhone}?text=Your shipment is ready for pickup.";

			// Open a new browser window with the link
			ScriptManager.RegisterStartupScript(this, GetType(), "OpenWhatsApp", $"window.open('{whatsappLink}','_blank');", true);
		}

		protected void btnNavigateWaze_Click(object sender, EventArgs e)
		{
			Button btnNavigateWaze = (Button)sender;
			string[] args = btnNavigateWaze.CommandArgument.ToString().Split(';');
			string destinationAddress = args[0];
			int cityId = Convert.ToInt32(args[1]);

			// Get city name by ID
			string destinationCity = City.GetCityNameById(cityId);

			// Prepare the Waze navigation link
			string wazeLink = GetWazeNavigationLink(destinationAddress, destinationCity);

			// Open a new browser window with the link
			ScriptManager.RegisterStartupScript(this, GetType(), "OpenWaze", $"window.open('{wazeLink}','_blank');", true);
		}

		private string GetWazeNavigationLink(string destinationAddress, string destinationCity)
		{
			// Construct the Waze navigation link
			string wazeBaseUri = "https://waze.com/ul";
			string queryParams = $"?q={Uri.EscapeDataString(destinationAddress)}, {Uri.EscapeDataString(destinationCity)}, Israel";
			string wazeLink = $"{wazeBaseUri}{queryParams}";

			return wazeLink;
		}



		private int GetCurrentUserId()
		{
			if (Session["Login"] != null)
			{
				Drivers currentUser = (Drivers)Session["Login"];
				return currentUser.DriverID;
			}
			else
			{
				Response.Redirect("~/LoginRegister.aspx");

				return -1;
			}
		}
	}
}