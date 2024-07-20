using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Driver_management.AdminManager
{
	public partial class AdminHomePage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Update active users count only if not a postback
			if (!IsPostBack)
			{
				// Initialize session if needed
				if (Session["Login"] == null)
				{
					Session["Login"] = new List<string>();
				}

				// Update label with active users count
				lblActiveUsersCount.Text = GetActiveUsersCount().ToString();
				LoadMonthlyShipments();
			}
		}
		private void LoadMonthlyShipments()
		{
			DbContext Db = new DbContext();
			int currentYear = DateTime.Now.Year;

			string Sql = $@"SELECT MONTH(OrderDate) AS Month, COUNT(*) AS ShipmentCount 
                    FROM Shipments 
                    WHERE YEAR(OrderDate) = {currentYear} 
                    GROUP BY MONTH(OrderDate) 
                    ORDER BY MONTH(OrderDate)";

			DataTable Dt = Db.Execute(Sql);

			List<int> months = new List<int>();
			List<int> shipmentCounts = new List<int>();

			foreach (DataRow row in Dt.Rows)
			{
				int month = Convert.ToInt32(row["Month"]);
				int count = Convert.ToInt32(row["ShipmentCount"]);

				// עבור כל חודש, הדפס את הנתונים למעקב
				Console.WriteLine($"Month: {month}, ShipmentCount: {count}");

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



		public void UserLogin(string username)
		{
			var loggedInUsers = Session["Login"] as List<string>;

			if (loggedInUsers == null)
			{
				loggedInUsers = new List<string>();
				Session["Login"] = loggedInUsers;
			}

			if (!loggedInUsers.Contains(username))
			{
				loggedInUsers.Add(username);
			}

			// Debugging output to trace issue
			System.Diagnostics.Debug.WriteLine($"User logged in: {username}. Total active users: {loggedInUsers.Count}");
		}

		public void UserLogout(string username)
		{
			var loggedInUsers = Session["Login"] as List<string>;

			if (loggedInUsers != null)
			{
				loggedInUsers.Remove(username);
			}

			// Debugging output to trace issue
			System.Diagnostics.Debug.WriteLine($"User logged out: {username}. Total active users: {loggedInUsers?.Count ?? 0}");
		}

		public int GetActiveUsersCount()
		{
			var loggedInUsers = Session["Login"] as List<string>;
			return loggedInUsers != null ? loggedInUsers.Count : 0;
		}
	}
}
