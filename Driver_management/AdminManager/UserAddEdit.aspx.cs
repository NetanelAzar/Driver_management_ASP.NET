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
						TxtFirstName.Text = Tmp.DriverName;
						TxtPhone.Text = Tmp.DriverPhone;
						TxtMail.Text = Tmp.DriverMail;
						TxtPassword.Text = Tmp.DriverPassword;
						ImgPicname.ImageUrl = "/uploads/prods/" + Tmp.Picname;
						HidUser.Value = UserID;
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
			string Picname = "";
			//נבדטק האם נבחקר קובץ תמונה
			if (UploadPic.HasFile)
			{
				//נשמור תחת שם שאנחנו בוחרים באקראי
				Picname = GlobFunk.GetRandStr(8);

				string OriginalFileName = UploadPic.FileName;
				string Ext = OriginalFileName.Substring(OriginalFileName.LastIndexOf('.'));//מהנקודה האחרונה עד הסוף
				Picname += Ext;//השם המלר של הקובץ אחרי ההשינוי
				string FullPath = Server.MapPath("/uploads/prods/");
				UploadPic.SaveAs(FullPath + Picname);

			}
			else
			{
				Picname = ImgPicname.ImageUrl.Substring(ImgPicname.ImageUrl.LastIndexOf('/') + 1);
			}








			Drivers user = new Drivers
			{
				DriverID = int.Parse(HidUser.Value),
				DriverName = TxtFirstName.Text,
				DriverPhone = TxtPhone.Text,
				DriverMail = TxtMail.Text,
				DriverPassword = TxtPassword.Text,
				Picname = Picname
			};

			user.Save();

			List<Drivers> LstClient = Drivers.GetAll();
			Application["Users"] = LstClient;

			Response.Redirect("UserList.aspx");


		}
	}
}