using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class AddressList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// הגדרת החודש הנוכחי כברירת מחדל
				int currentMonth = DateTime.Now.Month;
				ddlMonths.SelectedValue = currentMonth.ToString();

				LoadAddresses(currentMonth);
			}
		}

		protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedMonth = int.Parse(ddlMonths.SelectedValue);
			LoadAddresses(selectedMonth);
		}

		private void LoadAddresses(int month)
		{
			// קבלת רשימת המשלוחים לפי החודש הנבחר
			List<Shipment> shipments = Shipment.GetByMonth(month);

			// מיון המשלוחים לפי ערים
			shipments = shipments.OrderBy(s => s.DestinationCity).ToList();

			// קביעת מקור הנתונים של ה-Repeater
			RptAddresses.DataSource = shipments;
			RptAddresses.DataBind();
		}

		protected void RptAddresses_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Delete")
			{
				int shipmentId = Convert.ToInt32(e.CommandArgument);
				DeleteShipment(shipmentId);
				LoadAddresses(int.Parse(ddlMonths.SelectedValue));
			}
		}

		private void DeleteShipment(int shipmentId)
		{
			Shipment.Delete(shipmentId);
		}

		// פונקציה סטטית להחזרת שם עיר
		public static string GetCityNameById(object cityId)
		{
			if (cityId != null && int.TryParse(cityId.ToString(), out int id))
			{
				return City.GetCityNameById(id); // ודא כי הפונקציה GetCityNameById זמינה במחלקה City
			}
			return string.Empty;
		}

		// פונקציה לעיצוב תאריך
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