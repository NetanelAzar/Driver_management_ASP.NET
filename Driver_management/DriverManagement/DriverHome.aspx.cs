using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Driver_management.DriverManagement
{
	public partial class DriverHome : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
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
	}
}
