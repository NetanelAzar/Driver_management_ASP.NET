using System;
using System.Web.UI;
using BLL;
using DAL;

namespace Driver_management.AdminManager
{
	public partial class ClientsAddEdit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// Check if there's a client ID in query string
				string clientID = Request["ClientID"];
				if (!string.IsNullOrEmpty(clientID))
				{
					if (int.TryParse(clientID, out int parsedClientID))
					{
						// Load client details
						Client client = Client.GetById(parsedClientID);
						if (client != null)
						{
							HidClientID.Value = clientID;
							TxtClientName.Text = client.ClientName;
							TxtClientMail.Text = client.ClientMail;
							TxtClientPhone.Text = client.ClientPhone;
							TxtCompanyName.Text = client.CompanyName;
							TxtCityCode.Text = client.CityCode;
							TxtAddress.Text = client.Address;
						}
					}
				}
			}
		}

		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Client client = new Client();

			// Populate client object with form data
			client.ClientID = int.Parse(HidClientID.Value);
			client.ClientName = TxtClientName.Text;
			client.ClientMail = TxtClientMail.Text;
			client.ClientPhone = TxtClientPhone.Text;
			client.CompanyName = TxtCompanyName.Text;
			client.CityCode = TxtCityCode.Text;
			client.Address = TxtAddress.Text;

			// Save or update the client
			client.Save();

			// Update application-level variable
			Application["Clients"] = Client.GetAll();

			// Redirect to client list page
			Response.Redirect("ClientsList.aspx");
		}
	}
}