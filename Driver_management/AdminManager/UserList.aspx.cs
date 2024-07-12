using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class UserList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			///חיבור מאגר המוצרים לרפיטר
			List<Drivers> LstUser = (List<Drivers>)Application["Drivers"];
			RptUser.DataSource = LstUser;
			RptUser.DataBind();
		}
	}
}