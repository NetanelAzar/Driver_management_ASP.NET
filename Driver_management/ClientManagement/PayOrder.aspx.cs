using BLL;
using Stripe;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Diagnostics;

namespace Driver_management.ClientManagement
{
	public partial class PayOrder : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				// הצגת הערכים בתוויות לאחר פוסטבק
				LblNumberOfPackages.Text = ViewState["numberOfPackages"]?.ToString() ?? "לא זמין";
				LblTotalAmount.Text = ViewState["totalAmount"]?.ToString() + " ₪" ?? "לא זמין";
			}
		}

		protected void SomePostBackMethod(object sender, EventArgs e)
		{
			// שמירת הערכים ב-ViewState
			ViewState["numberOfPackages"] = Request.Form["numberOfPackages"];
			ViewState["totalAmount"] = Request.Form["totalAmount"];
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			string stripeToken = Request.Form["stripeToken"];
			if (!string.IsNullOrEmpty(stripeToken))
			{
				ProcessPayment(stripeToken);
			}
		}

		private void ProcessPayment(string stripeToken)
		{
			try
			{
				StripeConfiguration.ApiKey = "sk_test_51PdnnDCGGvtn75QCMzNS6MyqelYDtwVcloq4UWvnaucr0nS4iVjnK1drXlXFNhXbwZWMP4ABw07ZgTrTYTmoATTC00i6f3nHbM";

				string totalAmountStr = Request.Form["totalAmount"].Replace("₪", "").Trim();
				decimal totalAmountDecimal;
				if (!decimal.TryParse(totalAmountStr, NumberStyles.Any, CultureInfo.InvariantCulture, out totalAmountDecimal))
				{
					Response.Write("שגיאה: סכום התשלום אינו תקין.");
					return;
				}

				Client loggedInClient = Session["Login"] as Client;
				if (loggedInClient == null)
				{
					Response.Write("שגיאה: הלקוח לא נמצא.");
					return;
				}

				var customerService = new CustomerService();
				var existingCustomer = customerService.List(new CustomerListOptions { Email = loggedInClient.ClientMail }).FirstOrDefault();
				Customer customer;

				if (existingCustomer != null)
				{
					customer = existingCustomer;
				}
				else
				{
					var customerOptions = new CustomerCreateOptions
					{
						Email = loggedInClient.ClientMail,
						Name = loggedInClient.ClientName,
					};
					customer = customerService.Create(customerOptions);
				}

				// הוספת מקור התשלום ללקוח
				var customerUpdateOptions = new CustomerUpdateOptions
				{
					Source = stripeToken
				};
				customerService.Update(customer.Id, customerUpdateOptions);

				// ביצוע החיוב
				var chargeOptions = new ChargeCreateOptions
				{
					Amount = Convert.ToInt64(totalAmountDecimal * 100), // amount in cents
					Currency = "ILS",
					Description = "תשלום עבור הזמנה",
					Customer = customer.Id, // השיוך ללקוח
				};

				var chargeService = new ChargeService();
				Charge charge = chargeService.Create(chargeOptions);

				if (charge.Status == "succeeded")
				{
					Shipment shipment = new Shipment
					{
						DestinationAddress = Request.Form["destinationAddress"],
						NumberOfPackages = int.Parse(Request.Form["numberOfPackages"]),
						OrderDate = DateTime.Parse(Request.Form["orderDate"]),
						DestinationCity = int.Parse(Request.Form["destinationCity"]),
						CustomerID = loggedInClient.ClientID,
						CustomerPhone = loggedInClient.ClientPhone,
						ShippingStatus = "התקבל במערכת",
						Payment = (int)totalAmountDecimal // כאן נשמר הערך המומר בשדה Payment
					};

					shipment.Save();

					CreateAndSendInvoice(loggedInClient, shipment, totalAmountDecimal);
					SendInvoiceEmail(loggedInClient, shipment, totalAmountDecimal);

					Response.Redirect("PastPayment.aspx");
				}
				else
				{
					Response.Write("שגיאה בתשלום: " + charge.FailureMessage);
				}
			}
			catch (Exception ex)
			{
				Response.Write($"שגיאה בתהליך התשלום: {ex.Message}");
			}
		}


		private void CreateAndSendInvoice(Client client, Shipment shipment, decimal totalAmount)
		{
			try
			{
				var customerService = new CustomerService();
				var invoiceItemService = new InvoiceItemService();
				var invoiceService = new InvoiceService();

				// בדוק אם הלקוח כבר קיים
				var existingCustomer = customerService.List(new CustomerListOptions { Email = client.ClientMail }).FirstOrDefault();
				Customer customer;

				if (existingCustomer != null)
				{
					customer = existingCustomer;
				}
				else
				{
					// יצירת לקוח חדש ב-Stripe אם הוא לא קיים
					var customerOptions = new CustomerCreateOptions
					{
						Email = client.ClientMail,
						Name = client.ClientName,
					};
					customer = customerService.Create(customerOptions);
				}

				// יצירת פריט לחשבונית
				var invoiceItemOptions = new InvoiceItemCreateOptions
				{
					Customer = customer.Id,
					Amount = Convert.ToInt64(totalAmount * 100), // סכום בסנטים
					Currency = "ILS", // ודא שהמטבע תומך על ידי Stripe
					Description = $"תשלום עבור הזמנה עם פרטים: כתובת יעד - {shipment.DestinationAddress}, מספר חבילות - {shipment.NumberOfPackages}",
				};

				// הדפסת פרטי פריט החשבונית לקונסול של Visual Studio
				Debug.WriteLine("Creating Invoice Item with the following details:");
				Debug.WriteLine($"Customer ID: {invoiceItemOptions.Customer}");
				Debug.WriteLine($"Amount: {invoiceItemOptions.Amount}");
				Debug.WriteLine($"Currency: {invoiceItemOptions.Currency}");
				Debug.WriteLine($"Description: {invoiceItemOptions.Description}");

				var invoiceItem = invoiceItemService.Create(invoiceItemOptions);

				// בדוק אם פריט החשבונית נוצר בהצלחה
				if (invoiceItem == null || invoiceItem.Amount <= 0)
				{
					Debug.Write("שגיאה: פריט החשבונית לא נוצר כראוי.");
					return;
				}

				// יצירת החשבונית
				var invoiceOptions = new InvoiceCreateOptions
				{
					Customer = customer.Id,
					CollectionMethod = "send_invoice", // שליחת החשבונית ללקוח בדוא"ל
					DaysUntilDue = 30, // תאריך התשלום עד 30 יום
									   // קישור לפריט החשבונית
					AutoAdvance = true, // מאפשר את התהליך האוטומטי
				};

				var invoice = invoiceService.Create(invoiceOptions);

				// בדוק אם החשבונית נוצרה בהצלחה
				if (invoice == null)
				{
					Debug.Write("שגיאה: החשבונית לא נוצרה.");
					return;
				}

				// סיום החשבונית
				var finalizedInvoice = invoiceService.FinalizeInvoice(invoice.Id);

				// בדוק את מצב החשבונית
				if (finalizedInvoice.Status == "open")
				{
					// שליחת החשבונית ללקוח
					var sentInvoice = invoiceService.SendInvoice(invoice.Id);
					if (sentInvoice.Status == "sent")
					{
						Debug.Write("חשבונית נשלחה ללקוח בהצלחה.");
					}
					else
					{
						Debug.Write($"שגיאה בשליחת החשבונית: מצב החשבונית הוא {sentInvoice.Status}");
					}
				}
				else
				{
					Debug.Write($"שגיאה ביצירת החשבונית: מצב החשבונית הוא {finalizedInvoice.Status}");
				}
			}
			catch (StripeException ex)
			{
				// ניהול שגיאות ספציפיות של Stripe
				Debug.Write($"שגיאה ביצירת החשבונית: {ex.StripeError.Message}");
			}
			catch (Exception ex)
			{
				// ניהול שגיאות כלליות
				Debug.Write($"שגיאה ביצירת החשבונית: {ex.Message}");
			}
		}




		private void SendInvoiceEmail(Client client, Shipment shipment, decimal totalAmount)
		{
			try
			{
				// הגדרת פרטי הדואר האלקטרוני
				var fromAddress = new MailAddress("netanelazar@outlook.com", "AZAR");
				var toAddress = new MailAddress(client.ClientMail, client.ClientName);
				const string subject = "חשבונית עבור הזמנה";

				// יצירת תוכן ה-HTML להודעת הדוא"ל
				string body = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            color: #333;
                            margin: 20px;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: auto;
                            padding: 20px;
                            border: 1px solid #ddd;
                            border-radius: 8px;
                            background-color: #f9f9f9;
                        }}
                        h1 {{
                            color: #007bff;
                        }}
                        .details {{
                            margin-top: 20px;
                        }}
                        .details p {{
                            margin: 5px 0;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 0.9em;
                            color: #777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h1>חשבונית עבור הזמנה</h1>
                        <p>שלום {client.ClientName},</p>
                        <p>תודה על התשלום עבור ההזמנה שלך.</p>
                        <div class='details'>
                            <p><strong>פרטי ההזמנה:</strong></p>
                            <p><strong>כתובת יעד:</strong> {shipment.DestinationAddress}</p>
                            <p><strong>מספר חבילות:</strong> {shipment.NumberOfPackages}</p>
                            <p><strong>תאריך הזמנה:</strong> {shipment.OrderDate.ToShortDateString()}</p>
                            <p><strong>עיר יעד:</strong> {shipment.DestinationCity}</p>
                            <p><strong>סכום לתשלום:</strong> {totalAmount} ₪</p>
                        </div>
                        <div class='footer'>
                            <p>בברכה,</p>
                            <p>צוות התמיכה שלנו</p>
                        </div>
                    </div>
                </body>
                </html>";

				var smtp = new SmtpClient
				{
					Host = "smtp.office365.com", // אם אתה משתמש בשרת אחר, שנה את הכתובת
					Port = 587, // השתמש ב-465 אם השרת שלך דורש את זה
					EnableSsl = true, // אם השרת דורש SSL
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential("netanelazar@outlook.com", "Tikaazar286933")
				};

				using (var message = new MailMessage(fromAddress, toAddress)
				{
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				})
				{
					smtp.Send(message);
				}
			}
			catch (SmtpException smtpEx)
			{
				// טיפול בשגיאות SMTP
				Response.Write($"שגיאה בשליחת דואר אלקטרוני: {smtpEx.Message}");
			}
			catch (Exception ex)
			{
				// טיפול בשגיאות כלליות
				Response.Write($"שגיאה בלתי צפויה: {ex.Message}");
			}
		}
	}
}
