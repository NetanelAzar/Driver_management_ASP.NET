using DAL;
using BLL;
using System;
using System.Web.UI;

namespace Driver_management.ClientManagement
{
	public partial class OrderDetails : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string orderId = Request.QueryString["OrderID"];
				if (!string.IsNullOrEmpty(orderId) && int.TryParse(orderId, out int id))
				{
					LoadOrderDetails(id);
				}
			}
		}

		private void LoadOrderDetails(int orderId)
		{
			try
			{
				// קבל את פרטי המשלוח דרך הפונקציה BLL
				var shipment = Shipment.GetById(orderId);
				if (shipment != null)
				{
					OrderNumber.Text = shipment.OrderNumber.ToString(); // המרה מפורשת למחרוזת
					OrderDate.Text = shipment.OrderDate.ToString("dd/MM/yyyy");
					ShippingStatus.Text = shipment.ShippingStatus;
					ShippingAddress.Text = shipment.DestinationAddress;
					DestinationCity.Text = City.GetCityNameById(shipment.DestinationCity);
					NumberOfPackages.Text =  shipment.NumberOfPackages.ToString();
					payment.Text = shipment.Payment.ToString();


				}
				else
				{
					OrderNumber.Text = "אין נתונים להצגה";
				}
			}
			catch (Exception ex)
			{
				// טיפול בשגיאות, תוכל לכתוב את השגיאה ללוג
				OrderNumber.Text = "שגיאה בטעינת הנתונים";
				// לדוגמה, תוכל להוסיף את השגיאה ללוג
			}
		}
	}
}
