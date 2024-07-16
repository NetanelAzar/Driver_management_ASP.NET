using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.ClientManagement
{
	public partial class ClientMaster : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)

		{

			if (Session["Login"] == null && !Page.AppRelativeVirtualPath.Equals("~/LoginRegister.aspx", StringComparison.OrdinalIgnoreCase))
			{
				Response.Redirect("~/LoginRegister.aspx");
			}
			else if (Session["Login"] != null)
			{
				if (Session["Login"] is Drivers loggedInDriver)
				{
					lblUsername.Text = "Welcome, " + loggedInDriver.DriverName;
				}
				else if (Session["Login"] is Client loggedInClient)
				{
					lblUsername.Text = "Welcome, " + loggedInClient.ClientName;
				}
			}
		}


	}
}