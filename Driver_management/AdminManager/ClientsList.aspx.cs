using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DATA;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class ClientsList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadClients();
			}
		}

		private void LoadClients()
		{
			// קבלת רשימת הלקוחות מה-BLL
			List<Client> clients = Client.GetAll();

			// קביעת מקור הנתונים של ה-Repeater
			RptClients.DataSource = clients;
			RptClients.DataBind();
		}

		protected void RptClients_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Delete")
			{
				int clientId = Convert.ToInt32(e.CommandArgument);
				BLL.Client.Delete(clientId); // Call the BLL method for deletion
				LoadClients(); // Reload the clients after deletion
			}
		}


		private void DeleteClient(int clientId)
		{
			Client.Delete(clientId);
		}
	}
}