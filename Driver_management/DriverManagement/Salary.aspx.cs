using BLL;
using DATA;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.DriverManagement
{
	public partial class Salary : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadMonths();

				Drivers loggedInDriver = Session["Login"] as Drivers;

				if (loggedInDriver != null)
				{
					LoadSalaries(loggedInDriver.DriverID);
				}
				else
				{
					Response.Redirect("~/LoginRegister.aspx");
				}
			}
		}

		private void LoadMonths()
		{
			ddlMonthYear.Items.Clear();

			for (int year = DateTime.Now.Year - 1; year <= DateTime.Now.Year ; year++)
			{
				for (int month = 1; month <= 12; month++)
				{
					DateTime monthStart = new DateTime(year, month, 1);
					ddlMonthYear.Items.Add(new ListItem(monthStart.ToString("MMMM yyyy"), monthStart.ToString("yyyy-MM")));
				}
			}

			ddlMonthYear.SelectedValue = DateTime.Now.ToString("yyyy-MM");
		}

		private void LoadSalaries(int driverId)
		{
			DbContext dbContext = new DbContext(); // יצירת אובייקט DbContext
			try
			{
				string selectedMonth = ddlMonthYear.SelectedValue;

				if (!DateTime.TryParseExact(selectedMonth, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
				{
					throw new FormatException($"The selected date '{selectedMonth}' is not in the expected format 'yyyy-MM'.");
				}

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
            AND s.DriverID = @DriverID
            GROUP BY d.DriverName, FORMAT(s.OrderDate, 'yyyy-MM')
            ORDER BY d.DriverName, Month";

				SqlCommand command = new SqlCommand(query, dbContext.Conn); // יצירת פקודת SQL
				command.Parameters.AddWithValue("@DriverID", driverId); // הוספת פרמטר ל-DriverID
				command.Parameters.AddWithValue("@StartDate", startDate); // הוספת פרמטר לתחילת החודש
				command.Parameters.AddWithValue("@EndDate", endDate); // הוספת פרמטר לסוף החודש

				DataTable dt = new DataTable(); // יצירת DataTable
				using (SqlDataAdapter adapter = new SqlDataAdapter(command)) // שימוש ב-SqlDataAdapter למילוי ה-DataTable
				{
					adapter.Fill(dt);
				}

				if (dt.Rows.Count == 0) // אם אין נתונים
				{
					ClientScript.RegisterStartupScript(this.GetType(), "showMessage", "document.getElementById('message').style.display = 'block';", true);
					RepeaterSalaries.Visible = false; // הסתרת ה-Repeater
				}
				else
				{
					RepeaterSalaries.DataSource = dt; // הגדרת מקור הנתונים ל-Repeater
					RepeaterSalaries.DataBind(); // ביצוע DataBind ל-Repeater

					// הסתרת ההודעה אם יש נתונים
					ClientScript.RegisterStartupScript(this.GetType(), "hideMessage", "document.getElementById('message').style.display = 'none';", true);
					RepeaterSalaries.Visible = true; // הצגת ה-Repeater
				}
			}
			catch (FormatException ex)
			{
				Response.Write($"Date Format Error: {ex.Message}");
			}
			catch (Exception ex)
			{
				Response.Write($"Error: {ex.Message}");
			}
			finally
			{
				if (dbContext.Conn.State == ConnectionState.Open) // סגירת החיבור אם הוא פתוח
				{
					dbContext.Conn.Close();
				}
			}
		}


		protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			Drivers loggedInDriver = Session["Login"] as Drivers;
			if (loggedInDriver != null)
			{
				LoadSalaries(loggedInDriver.DriverID);
			}
		}
	}
}
