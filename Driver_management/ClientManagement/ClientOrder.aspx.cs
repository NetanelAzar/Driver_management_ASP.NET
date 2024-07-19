using BLL;
using System;
using System.Linq;
using System.Web.UI;

namespace Driver_management.ClientManagement
{
	public partial class ClientOrder : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// Populate dropdown with cities from database
				var cities = BLL.City.GetAll().OrderBy(c => c.CityName).ToList();
				DdlCityDestination.DataSource = cities;
				DdlCityDestination.DataTextField = "CityName";
				DdlCityDestination.DataValueField = "CityID";
				DdlCityDestination.DataBind();

				// Set current date on the Order Date field
				TxtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
			}
		}

		protected void TxtNumberOfPackages_TextChanged(object sender, EventArgs e)
		{
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
				int numberOfPackages;
				if (!int.TryParse(TxtNumberOfPackages.Text, out numberOfPackages) || numberOfPackages <= 0)
				{
					Response.Write("שגיאה: מספר החבילות חייב להיות מספר שלם חיובי חוקי.");
					return;
				}

				DateTime orderDate;
				if (!DateTime.TryParse(TxtOrderDate.Text, out orderDate))
				{
					Response.Write("שגיאה: תאריך הזמנה אינו חוקי.");
					return;
				}

				Client loggedInClient = Session["Login"] as Client;

				if (loggedInClient == null)
				{
					Response.Write("שגיאה: מידע על הלקוח המחובר לא נמצא.");
					return;
				}

				// Redirect to payment page with order details
				string paymentUrl = $"PayOrder.aspx?NumberOfPackages={numberOfPackages}&TotalAmount={numberOfPackages * 60}&DestinationAddress={TxtDestinationAddress.Text}&OrderDate={orderDate.ToString("yyyy-MM-dd")}&DestinationCity={DdlCityDestination.SelectedValue}";
				Response.Redirect(paymentUrl);
			}
			catch (Exception ex)
			{
				Response.Write($"שגיאה: {ex.Message}");
			}
		}
	}
}
