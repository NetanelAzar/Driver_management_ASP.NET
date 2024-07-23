using BLL;
using DAL;
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
			// בדוק אם הסשן ריק ואם הדף הנוכחי אינו דף ההתחברות
			if (Session["Login"] == null && !Page.AppRelativeVirtualPath.Equals("~/LoginRegister.aspx", StringComparison.OrdinalIgnoreCase))
			{
				// הפנה לדף ההתחברות אם לא מחובר
				Response.Redirect("~/LoginRegister.aspx");
			}
			else if (Session["Login"] != null)
			{
				// קבל את המידע מהסשן
				var loggedInUser = Session["Login"];

				// בדוק את סוג המשתמש ועשה את ההתאמה הנדרשת
				if (loggedInUser is Drivers loggedInDriver)
				{
					lblUsername.Text = loggedInDriver.DriverName;
				}
				else if (loggedInUser is Client loggedInClient)
				{
					lblUsername.Text = loggedInClient.ClientName;
				}
				else if (loggedInUser is Manager loggedInAdmin) // הוספת בדיקה עבור מנהלים
				{
					lblUsername.Text = loggedInAdmin.Name; // הנחה שהמאפיין AdminName קיים במחלקת Admin
				}
			}
		}



	}
}