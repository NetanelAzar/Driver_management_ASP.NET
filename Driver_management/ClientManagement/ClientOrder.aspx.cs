using BLL; // שימוש במחלקות מהשכבת BLL
using System;
using System.Linq; // שימוש ב-LINQ לסינון וסידור נתונים
using System.Web.UI; // לשימוש בעמודי ASP.NET

namespace Driver_management.ClientManagement
{
	public partial class ClientOrder : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// ממלא את רשימת הערים בתיבת הבחירה מהמסד נתונים
				var cities = BLL.City.GetAll().OrderBy(c => c.CityName).ToList();
				DdlCityDestination.DataSource = cities;
				DdlCityDestination.DataTextField = "CityName";
				DdlCityDestination.DataValueField = "CityID";
				DdlCityDestination.DataBind();

				// קובע את התאריך הנוכחי בשדה תאריך ההזמנה
				TxtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			}
		}

		protected void TxtNumberOfPackages_TextChanged(object sender, EventArgs e)
		{
			// מחשב את הסכום הכולל על פי מספר החבילות שהוזן
			int numberOfPackages;
			if (int.TryParse(TxtNumberOfPackages.Text, out numberOfPackages))
			{
				LblTotalAmountValue.Text = (numberOfPackages * 60).ToString();
			}
		}

		protected void BtnSubmit_Click(object sender, EventArgs e)
		{
			try
			{
				// בודק אם מספר החבילות חוקי
				int numberOfPackages;
				if (!int.TryParse(TxtNumberOfPackages.Text, out numberOfPackages) || numberOfPackages <= 0)
				{
					Response.Write("שגיאה: מספר החבילות חייב להיות מספר שלם חיובי חוקי.");
					return;
				}

				// בודק אם תאריך ההזמנה חוקי
				DateTime orderDate;
				if (!DateTime.TryParse(TxtOrderDate.Text, out orderDate))
				{
					Response.Write("שגיאה: תאריך הזמנה אינו חוקי.");
					return;
				}

				// מקבל את פרטי הלקוח המחובר מהסשן
				Client loggedInClient = Session["Login"] as Client;

				if (loggedInClient == null)
				{
					Response.Write("שגיאה: מידע על הלקוח המחובר לא נמצא.");
					return;
				}

				// מבצע הפניה לדף התשלום עם פרטי ההזמנה
				string paymentUrl = $"PayOrder.aspx?NumberOfPackages={numberOfPackages}&TotalAmount={numberOfPackages * 60}&DestinationAddress={TxtDestinationAddress.Text}&OrderDate={orderDate.ToString("yyyy-MM-dd")}&DestinationCity={DdlCityDestination.SelectedValue}";
				Response.Redirect(paymentUrl);
			}
			catch (Exception ex)
			{
				// מציג את השגיאה במקרה של שגיאה
				Response.Write($"שגיאה: {ex.Message}");
			}
		}
	}
}
