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

			// עדכון מצב ההובלה ל"נמסר"
			string Status = "נמסר";
			Shipment.UpdateDeliveryStatus(shipmentId, Status);

			// עדכון תאריך ההובלה לתאריך הנוכחי
			DateTime deliveryDate = DateTime.Now;
			ShipmentDAL.UpdateDeliveryDate(shipmentId, deliveryDate);

			// רענון הרשימה לאחר העדכון
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

			// עדכון מצב ההובלה ל"נאסף על ידי השליח"
			Shipment.UpdateDeliveryStatus(shipmentId, "נאסף על ידי השליח");

			// עדכון תאריך האיסוף לתאריך הנוכחי
			DateTime pickupDate = DateTime.Now;
			ShipmentDAL.UpdatePickupDate(shipmentId, pickupDate);

			// רענון הרשימה לאחר העדכון
			LoadMyShipments();
		}

		protected void btnSendWhatsApp_Click(object sender, EventArgs e)
		{
			Button btnSendWhatsApp = (Button)sender;
			string customerPhone = btnSendWhatsApp.CommandArgument;

			// יצירת לינק לשליחת הודעה ב-WhatsApp
			string whatsappLink = $"https://wa.me/{customerPhone}?text=Your shipment is ready for pickup.";

			// פתיחת חלון דפדפן חדש עם הלינק
			ScriptManager.RegisterStartupScript(this, GetType(), "OpenWhatsApp", $"window.open('{whatsappLink}','_blank');", true);
		}

		protected void btnNavigateWaze_Click(object sender, EventArgs e)
		{
			Button btnNavigateWaze = (Button)sender;
			string[] args = btnNavigateWaze.CommandArgument.ToString().Split(';');
			string destinationAddress = args[0];
			int cityId = Convert.ToInt32(args[1]);

			// קבלת שם העיר לפי מזהה
			string destinationCity = City.GetCityNameById(cityId);

			// הכנת לינק ניווט ל-Waze
			string wazeLink = GetWazeNavigationLink(destinationAddress, destinationCity);

			// פתיחת חלון דפדפן חדש עם הלינק
			ScriptManager.RegisterStartupScript(this, GetType(), "OpenWaze", $"window.open('{wazeLink}','_blank');", true);
		}

		private string GetWazeNavigationLink(string destinationAddress, string destinationCity)
		{
			// בניית לינק ניווט ל-Waze
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
