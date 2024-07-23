using BLL;
using System;
using System.Web.UI;

namespace Driver_management.AdminManager
{
	public partial class BackAdmin : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// בדוק אם הסשן ריק ואם הדף הנוכחי אינו דף ההתחברות
			if (Session["Login"] == null || !(Session["Login"] is Manager))
			{
				// הפנה לדף ההתחברות אם לא מחובר כמנהל
				Response.Redirect("~/LoginRegister.aspx");
			}
			else
			{
				// קבל את המידע מהסשן
				var loggedInUser = Session["Login"];

				// בדוק את סוג המשתמש ועשה את ההתאמה הנדרשת
				if (loggedInUser is Manager loggedInAdmin)
				{
					lblUsername.Text = loggedInAdmin.Name; // וודא שהמאפיין Name קיים במחלקת Manager
				}
				else
				{
					// אם המשתמש אינו מזוהה, הפנה אותו לדף ההתחברות
					Response.Redirect("~/LoginRegister.aspx");
				}
			}
		}
	}
}
