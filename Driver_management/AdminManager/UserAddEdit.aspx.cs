using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class UserAddEdit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string UserID = Request["UserID"] + "";
				if (string.IsNullOrEmpty(UserID))
				{
					UserID = "-1";
				}
				else
				{
					int userID = int.Parse(UserID);//המרה של המשתנה למספר שלם , לצורך חיפוש במאגר
					Drivers Tmp = Drivers.GetById(userID);
					if (Tmp != null)
					{
						TxtUserFullName.Text = Tmp.DriverName;
						TxtUserPhone.Text = Tmp.DriverPhone;
						TxtUserMail.Text = Tmp.DriverMail;
						TxtAddress.Text =Tmp.Address;
						TxtPassword.Text = Tmp.DriverPassword;
						TxtMax.Text = Tmp.MaxDeliveries.ToString();
						TxtCityCode.Text = Tmp.CityCode.ToString();
						TxtZoneID.Text = Tmp.zoneID.ToString();
						TxtUserCode.Text = Tmp.DriverCode;
						HidDriverID.Value = UserID;
					}
					else
					{
						UserID = "-1";
					}

				}
			}
		}

		protected void BtnSave_Click(object sender, EventArgs e)
		{
		







			Drivers user = new Drivers
			{
				DriverID = int.Parse(HidDriverID.Value),
				DriverName = TxtUserFullName.Text,
				DriverPhone = TxtUserPhone.Text,
				DriverMail = TxtUserMail.Text,
				Address = TxtAddress.Text,
				DriverPassword = TxtPassword.Text,
				zoneID = TxtZoneID.Text,
				DriverCode = TxtUserCode.Text,
				CityCode = int.Parse(TxtCityCode.Text),
				MaxDeliveries = int.Parse(TxtMax.Text),
				CurrentDeliveries = int.Parse(TxtMax.Text),
			};

			user.Save();

			List<Drivers> LstClient = Drivers.GetAll();
			Application["Drivers"] = LstClient;

			Response.Redirect("UserList.aspx");


		}
	}
}