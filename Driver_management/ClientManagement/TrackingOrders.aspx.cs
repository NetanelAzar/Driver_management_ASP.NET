using BLL; // שימוש ב-Business Logic Layer
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
			// קבלת פרטי הלקוח המחובר מה-session או ממקום אחר שבו הם נשמרים
			Client loggedInClient = Session["Login"] as Client;

			if (loggedInClient == null)
			{
				// טיפול במקרה שבו פרטי הלקוח המחובר לא נמצאו
				Response.Write("שגיאה: לא נמצאו פרטי הלקוח המחובר.");
				return;
			}

			// שליפת ההזמנות עבור הלקוח המחובר
			List<Shipment> orders = BLL.Shipment.GetShipmentsByCustomerId(loggedInClient.ClientID);

			// קישור ההזמנות ל-Repeater
			RptOrders.DataSource = orders;
			RptOrders.DataBind();
		}

		// פונקציה לעזור לעצב את שמות הערים
		protected string GetCityNameById(object cityIdObj)
		{
			if (cityIdObj == null || cityIdObj == DBNull.Value)
				return string.Empty;

			int cityId;
			if (int.TryParse(cityIdObj.ToString(), out cityId))
			{
				City city = BLL.City.GetById(cityId); // הנחה שיש שיטה כזו ב-BLL
				if (city != null)
				{
					return city.CityName;
				}
			}
			return string.Empty;
		}

		// פונקציה לעצב תאריך
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