using BLL;
using DAL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Driver_management.DriverManagement
{
	public partial class DriverHome : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadMonthlyShipments();
				LoadDriverData();
				BindNews();
			}
		}

		private void LoadDriverData()
		{
			Drivers loggedInDriver = Session["Login"] as Drivers;
			if (loggedInDriver == null)
			{
				lblUsername.Text = "שגיאה: לא ניתן לטעון נתוני נהג.";
				return;
			}

			lblUsername.Text = "שלום, " + loggedInDriver.DriverName;

			// Load today's deliveries
			var today = DateTime.Today;
			var deliveriesToday = BLL.Shipment.GetAll().Where(s => s.DriverID == loggedInDriver.DriverID && s.OrderDate.Date == today).ToList();
			RptDeliveriesToday.DataSource = deliveriesToday;
			RptDeliveriesToday.DataBind();

			// Load number of deliveries made this month
			var deliveriesThisMonth = BLL.Shipment.GetAll().Where(s => s.DriverID == loggedInDriver.DriverID && s.OrderDate.Month == today.Month && s.OrderDate.Year == today.Year).Count();
			LblDeliveriesThisMonth.Text = deliveriesThisMonth.ToString();
		}

		private void BindNews()
		{
			List<News> newsList = News.GetAll();
			RptNews.DataSource = newsList;
			RptNews.DataBind();
		}


		private void LoadMonthlyShipments()
		{
			DbContext Db = new DbContext();
			int currentYear = DateTime.Now.Year;

			Drivers loggedInDriver = Session["Login"] as Drivers;
			if (loggedInDriver == null)
			{
				lblUsername.Text = "שגיאה: לא ניתן לטעון נתוני נהג.";
				return;
			}

			int driverId = loggedInDriver.DriverID; // קבלת מזהה הנהג מה־Session

			// יצירת השאילתא עם פרמטרים משולבים ישירות במחרוזת השאילתא
			string sql = $@"SELECT MONTH(OrderDate) AS Month, COUNT(*) AS ShipmentCount 
                   FROM Shipments 
                   WHERE YEAR(OrderDate) = {currentYear} 
                   AND DriverID = {driverId}
                   GROUP BY MONTH(OrderDate) 
                   ORDER BY MONTH(OrderDate)";

			DataTable dt = Db.Execute(sql); // אין צורך להעביר פרמטרים

			List<int> months = new List<int>();
			List<int> shipmentCounts = new List<int>();

			foreach (DataRow row in dt.Rows)
			{
				int month = Convert.ToInt32(row["Month"]);
				int count = Convert.ToInt32(row["ShipmentCount"]);

				months.Add(month);
				shipmentCounts.Add(count);
			}

			JavaScriptSerializer serializer = new JavaScriptSerializer();
			string monthsJson = serializer.Serialize(months);
			string shipmentCountsJson = serializer.Serialize(shipmentCounts);

			ClientScript.RegisterStartupScript(this.GetType(), "loadChart",
				$"window.onload = function() {{ loadChart({monthsJson}, {shipmentCountsJson}); }};",
				true);
		}


	}
}
