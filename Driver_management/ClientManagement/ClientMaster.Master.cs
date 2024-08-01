using BLL;
using DAL;
using Stripe;
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

				if (!IsPostBack)
				{
					SyncClientsWithStripe();
				}
			}
		}

		private void SyncClientsWithStripe()
		{
			try
			{
				StripeConfiguration.ApiKey = "sk_test_51PdnnDCGGvtn75QCMzNS6MyqelYDtwVcloq4UWvnaucr0nS4iVjnK1drXlXFNhXbwZWMP4ABw07ZgTrTYTmoATTC00i6f3nHbM";
				var customerService = new CustomerService();

				var clients = Client.GetAll(); // מקבל את כל הלקוחות מה-DAL
				var stripeCustomerIds = new Dictionary<int, string>();

				foreach (var client in clients)
				{
					// חיפוש לקוח קיים ב-Stripe לפי Email
					var existingCustomer = FindStripeCustomerByEmail(client.ClientMail);

					if (existingCustomer == null)
					{
						// יצירת לקוח חדש ב-Stripe אם הוא לא קיים
						var customerOptions = new CustomerCreateOptions
						{
							Name = client.ClientName,
							Email = client.ClientMail,
							Phone = client.ClientPhone,
							Description = $"Client ID: {client.ClientID}"
						};

						var customer = customerService.Create(customerOptions);

						// שמירה במילון של מזהים
						stripeCustomerIds[client.ClientID] = customer.Id;
					}
					else
					{
						// לקוח קיים - עדכון מזהה בנתוני הלקוח שלך (אם נדרש)
						stripeCustomerIds[client.ClientID] = existingCustomer.Id;
					}
				}

				// שמור את מזהי הלקוחות בסשן או בצורה אחרת אם דרוש
				Session["StripeCustomerIds"] = stripeCustomerIds;
			}
			catch (Exception ex)
			{
				Response.Write($"שגיאה במהלך סנכרון לקוחות עם Stripe: {ex.Message}");
			}
		}

		private Customer FindStripeCustomerByEmail(string email)
		{
			var customerService = new CustomerService();
			var customers = customerService.List(new CustomerListOptions
			{
				Email = email
			});

			// החזרת הלקוח הראשון שנמצא אם קיים
			return customers.Data.Count > 0 ? customers.Data[0] : null;
		}
	}
}
