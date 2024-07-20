using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Driver_management.User_Controls
{
	public partial class LoginCube : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				PopulateCities();
			}
		}

		private void PopulateCities()
		{
			List<City> cities = City.GetAll(); // Assuming City.GetAll() retrieves a list of cities
			if (cities != null && cities.Count > 0)
			{
				DDLRegCity.DataSource = cities; // Changed from DDLCity to DDLRegCity
				DDLRegCity.DataTextField = "CityName";
				DDLRegCity.DataValueField = "CityId";
				DDLRegCity.DataBind();
			}
			else
			{
				DDLRegCity.Items.Clear();
				DDLRegCity.Items.Add(new ListItem("לא נמצאו ערים", ""));
			}
		}

		protected void BtnLogin_Click(object sender, EventArgs e)
		{
			string Email = TxtEmail.Text;
			string Password = TxtPassword.Text;

			List<Drivers> LstDrivers = Application["Drivers"] as List<Drivers>;
			List<Client> LstClients = Application["Clients"] as List<Client>;

			bool isValidUser = false;

			if (LstDrivers != null)
			{
				foreach (var driver in LstDrivers)
				{
					if (driver.DriverMail.Equals(Email, StringComparison.OrdinalIgnoreCase) && driver.DriverPassword == Password)
					{
						Session["Login"] = driver;
						AddLoginNotification($"{driver.DriverName} התחבר כמנהל נהגים");
						Response.Redirect("~/DriverManagement/MyShipments.aspx");
						isValidUser = true;
						break;
					}
				}
			}
			else
			{
				LtlMsg.Text = "<div class='alert alert-danger' role='alert'>אין נתוני נהגים זמינים</div>";
				return;
			}

			if (!isValidUser && LstClients != null)
			{
				foreach (var client in LstClients)
				{
					if (client.ClientMail.Equals(Email, StringComparison.OrdinalIgnoreCase) && client.ClientPassword == Password)
					{
						Session["Login"] = client;
						AddLoginNotification($"{client.ClientName} התחבר כלקוח");
						Response.Redirect("~/ClientManagement/ClientHome.aspx"); // דף הלקוחות
						isValidUser = true;
						break;
					}
				}
			}
			else if (LstClients == null)
			{
				LtlMsg.Text = "<div class='alert alert-danger' role='alert'>אין נתוני לקוחות זמינים</div>";
				return;
			}

			if (!isValidUser)
			{
				LtlMsg.Text = "<div class='alert alert-danger' role='alert'>שם וסיסמא אינם תקינים</div>";
			}
		}

		private void AddLoginNotification(string message)
		{
			var notifications = Session["LoginNotifications"] as List<string> ?? new List<string>();

			string time = DateTime.Now.ToString("HH:mm:ss");
			notifications.Add($"{message} - {time}");

			// Keep only the last 10 notifications
			if (notifications.Count > 10)
			{
				notifications.RemoveAt(0);
			}

			Session["LoginNotifications"] = notifications;
		}



		protected void BtnRegister_Click(object sender, EventArgs e)
		{
			List<Client> clients = (List<Client>)Application["Clients"] ?? new List<Client>();

			Client newClient = new Client
			{
				ClientName = TxtRegFullName.Text,
				ClientMail = TxtRegEmail.Text,
				ClientPhone = TxtRegPhone.Text,
				Address = TxtRegAdd.Text,
				CityCode = DDLRegCity.SelectedValue,
				CompanyName = TxtRegCompanyName.Text
			};

			bool validPassword = TxtRegPass.Text.Length >= 6;
			bool emailExists = clients.Exists(c => c.ClientMail == newClient.ClientMail);

			if (!validPassword)
			{
				LtlRegMsg.Text = "הסיסמה צריכה להיות לפחות 6 תווים.";
			}
			else if (emailExists)
			{
				LtlRegMsg.Text = "כתובת האימייל כבר קיימת במערכת";
			}
			else
			{
				newClient.ClientPassword = TxtRegPass.Text;
				clients.Add(newClient);
				Application["Clients"] = clients;
				Session["Login"] = newClient;
				LtlRegMsg.Text = "הרשמה מוצלחת!";
				Response.Redirect("ProductsList.aspx");
			}
		}


		private void AddActiveUser(string username)
		{
			var activeUsers = Application["ActiveUsers"] as List<string> ?? new List<string>();
			if (!activeUsers.Contains(username))
			{
				activeUsers.Add(username);
				Application["ActiveUsers"] = activeUsers;
			}
		}

		protected void LinkToRegister_Click(object sender, EventArgs e)
		{
			MultiView1.ActiveViewIndex = 1;
		}

		protected void LinkToLogin_Click(object sender, EventArgs e)
		{
			MultiView1.ActiveViewIndex = 0;
		}

	}
}
