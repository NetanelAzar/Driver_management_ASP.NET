using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BLL;

namespace Driver_management.AdminManager
{
	public partial class AddressUserList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				try
				{
					// Bind user IDs to DropDownList
					List<Drivers> users = Drivers.GetAll();
					ddlUserIDs.DataSource = users;
					ddlUserIDs.DataTextField = "DriverName"; // שם המאפיין הקיים במחלקה Drivers
					ddlUserIDs.DataValueField = "DriverID";
					ddlUserIDs.DataBind();

					// Load addresses for the first user ID by default
					if (users.Count > 0)
					{
						int selectedUserId = users[0].DriverID; // בחירת הנהג הראשון כברירת מחדל
						LoadAddresses(selectedUserId);
					}
				}
				catch (Exception ex)
				{
					// טיפול בשגיאה (רישום או הצגת הודעת שגיאה)
					lblErrorMessage.Text = "שגיאה בטעינת הנתונים: " + ex.Message;
				}
			}
		}

		protected void ddlUserIDs_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				int selectedUserId = Convert.ToInt32(ddlUserIDs.SelectedValue);
				LoadAddresses(selectedUserId);
			}
			catch (Exception ex)
			{
				// Handle exception (log or display error message)
				lblErrorMessage.Text = "שגיאה בטעינת הכתובות: " + ex.Message;
			}
		}

		private void LoadAddresses(int userId)
		{
			try
			{
				// קריאה לפעולת BLL לקבלת הכתובות לפי DriverID
				List<Shipment> addresses = Shipment.GetShipmentsByUserId(userId);

				// קישור הכתובות ל-GridView
				GridViewAddresses.DataSource = addresses;
				GridViewAddresses.DataBind();
			}
			catch (Exception ex)
			{
				// טיפול בשגיאה (רישום או הצגת הודעת שגיאה)
				lblErrorMessage.Text = "שגיאה בטעינת הכתובות: " + ex.Message;
			}
		}
	}
}