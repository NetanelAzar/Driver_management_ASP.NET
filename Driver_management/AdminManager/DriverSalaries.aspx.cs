using DATA; // ייבוא של שכבת הגישה לנתונים (Data Access Layer)
using System; // ייבוא הספרייה הבסיסית של .NET
using System.Data; // ייבוא של רכיבי Data
using System.Data.SqlClient; // ייבוא של SqlClient לנתוני SQL Server
using System.Web.UI; // ייבוא של רכיבי Web Forms
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Forms נוספים

namespace Driver_management.AdminManager
{
	public partial class DriverSalaries : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טעינת הדף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				LoadMonths(); // טעינת חודשים לשדה הבחירה
				LoadSalaries(); // טעינת משכורות
			}
		}

		private void LoadMonths()
		{
			ddlMonthYear.Items.Clear(); // ניקוי פריטי השדה

			// הוספת חודשים לשדה הבחירה
			for (int year = DateTime.Now.Year - 1; year <= DateTime.Now.Year + 1; year++)
			{
				for (int month = 1; month <= 12; month++)
				{
					DateTime monthStart = new DateTime(year, month, 1); // יצירת תאריך לתחילת החודש
					ddlMonthYear.Items.Add(new ListItem(monthStart.ToString("MMMM yyyy"), monthStart.ToString("yyyy-MM")));
				}
			}

			// בחירת החודש הנוכחי כברירת מחדל
			ddlMonthYear.SelectedValue = DateTime.Now.ToString("yyyy-MM");
		}

		private void LoadSalaries()
		{
			DbContext dbContext = new DbContext(); // יצירת אובייקט DbContext

			// קביעת התאריכים עבור תחילת וסיום החודש הנבחר
			string selectedMonth = ddlMonthYear.SelectedValue;
			DateTime selectedDate = DateTime.ParseExact(selectedMonth, "yyyy-MM", null);
			DateTime startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1); // תחילת החודש
			DateTime endDate = startDate.AddMonths(1).AddDays(-1); // סוף החודש

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

			SqlCommand command = new SqlCommand(query, dbContext.Conn); // יצירת פקודת SQL
			command.Parameters.AddWithValue("@StartDate", startDate); // הוספת פרמטר לתחילת החודש
			command.Parameters.AddWithValue("@EndDate", endDate); // הוספת פרמטר לסוף החודש

			DataTable dt = new DataTable(); // יצירת DataTable
			try
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(command)) // שימוש ב-SqlDataAdapter למילוי ה-DataTable
				{
					adapter.Fill(dt);
				}

				if (dt.Rows.Count == 0) // אם אין נתונים
				{
					// הצגת הודעה במרכז המסך
					ClientScript.RegisterStartupScript(this.GetType(), "showMessage", "document.getElementById('message').style.display = 'block';", true);
					RepeaterSalaries.Visible = false; // הסתרת הטבלה
				}
				else
				{
					RepeaterSalaries.DataSource = dt; // הגדרת מקור הנתונים לרפיטר
					RepeaterSalaries.DataBind(); // ביצוע DataBind ל-Repeater

					// הסתרת ההודעה אם יש נתונים
					ClientScript.RegisterStartupScript(this.GetType(), "hideMessage", "document.getElementById('message').style.display = 'none';", true);
					RepeaterSalaries.Visible = true; // הצגת הטבלה
				}
			}
			catch (Exception ex)
			{
				Response.Write($"Error: {ex.Message}"); // טיפול בשגיאות
			}
			finally
			{
				if (dbContext.Conn.State == ConnectionState.Open) // סגירת החיבור אם הוא פתוח
				{
					dbContext.Conn.Close();
				}
			}
		}

		protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e) // אירוע שינוי ערך בשדה הבחירה
		{
			LoadSalaries(); // טעינת משכורות מחדש על פי החודש הנבחר
		}
	}
}
