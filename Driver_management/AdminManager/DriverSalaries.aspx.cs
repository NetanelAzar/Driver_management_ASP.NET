using DATA;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class DriverSalaries : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadMonths();
				LoadSalaries();
			}
		}

		private void LoadMonths()
		{
			ddlMonthYear.Items.Clear();

			// הוספת חודשים לשדה הבחירה
			for (int year = DateTime.Now.Year - 1; year <= DateTime.Now.Year + 1; year++)
			{
				for (int month = 1; month <= 12; month++)
				{
					DateTime monthStart = new DateTime(year, month, 1);
					ddlMonthYear.Items.Add(new ListItem(monthStart.ToString("MMMM yyyy"), monthStart.ToString("yyyy-MM")));
				}
			}

			// בחירת החודש הנוכחי כברירת מחדל
			ddlMonthYear.SelectedValue = DateTime.Now.ToString("yyyy-MM");
		}

		private void LoadSalaries()
		{
			DbContext dbContext = new DbContext();

			// קביעת התאריכים עבור תחילת וסיום החודש הנבחר
			string selectedMonth = ddlMonthYear.SelectedValue;
			DateTime selectedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
			DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
			DateTime endDate = startDate.AddMonths(1).AddDays(-1);

			string query = @"
                SELECT 
                    d.DriverName,
                    FORMAT(s.OrderDate, 'yyyy-MM') AS Month,
                    COUNT(s.ShipmentID) AS NumberOfDeliveries,
                    COUNT(s.ShipmentID) * 50 AS TotalAmount
                FROM Shipments s
                JOIN Drivers d ON s.DriverID = d.DriverID
                WHERE s.OrderDate >= @StartDate AND s.OrderDate <= @EndDate
                GROUP BY d.DriverName, FORMAT(s.OrderDate, 'yyyy-MM')
                ORDER BY d.DriverName, Month";

			SqlCommand command = new SqlCommand(query, dbContext.Conn);
			command.Parameters.AddWithValue("@StartDate", startDate);
			command.Parameters.AddWithValue("@EndDate", endDate);

			DataTable dt = new DataTable();
			try
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(command))
				{
					adapter.Fill(dt);
				}

				if (dt.Rows.Count == 0)
				{
					// הצגת ההודעה במרכז המסך
					ClientScript.RegisterStartupScript(this.GetType(), "showMessage", "document.getElementById('message').style.display = 'block';", true);
					RepeaterSalaries.Visible = false; // הסתרת הטבלה
				}
				else
				{
					RepeaterSalaries.DataSource = dt;
					RepeaterSalaries.DataBind();

					// הסתרת ההודעה אם יש נתונים
					ClientScript.RegisterStartupScript(this.GetType(), "hideMessage", "document.getElementById('message').style.display = 'none';", true);
					RepeaterSalaries.Visible = true; // הצגת הטבלה
				}
			}
			catch (Exception ex)
			{
				Response.Write($"Error: {ex.Message}");
			}
			finally
			{
				if (dbContext.Conn.State == ConnectionState.Open)
				{
					dbContext.Conn.Close();
				}
			}
		}

		protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadSalaries();
		}
	}
}
