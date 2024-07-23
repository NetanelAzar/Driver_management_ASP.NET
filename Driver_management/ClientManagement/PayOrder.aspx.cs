using BLL;
using Stripe;
using System;
using System.Globalization;
using System.Web.UI;

namespace Driver_management.ClientManagement
{
	public partial class PayOrder : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string numberOfPackages = Request.Form["numberOfPackages"];
				string totalAmount = Request.Form["totalAmount"];
				string destinationAddress = Request.Form["destinationAddress"];
				string orderDate = Request.Form["orderDate"];
				string destinationCity = Request.Form["destinationCity"];

				if (string.IsNullOrWhiteSpace(numberOfPackages) || string.IsNullOrWhiteSpace(totalAmount) ||
					string.IsNullOrWhiteSpace(destinationAddress) || string.IsNullOrWhiteSpace(orderDate) ||
					string.IsNullOrWhiteSpace(destinationCity))
				{
					Response.Write("שגיאה: פרטי ההזמנה חסרים.");
					return;
				}

				// הצגת הערכים בתוויות
				LblNumberOfPackages.Text = numberOfPackages;
				LblTotalAmount.Text = totalAmount + " ₪";
			}
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

				// ניקוי הסמל "₪" והמרת הסכום למספר שלם
				string totalAmount = Request.Form["totalAmount"].Replace("₪", "").Trim();
				decimal totalAmountDecimal;
				if (!decimal.TryParse(totalAmount, NumberStyles.Any, CultureInfo.InvariantCulture, out totalAmountDecimal))
				{
					Response.Write("שגיאה: סכום התשלום אינו תקין.");
					return;
				}

				var chargeOptions = new ChargeCreateOptions
				{
					Amount = Convert.ToInt64(totalAmountDecimal * 100), // amount in cents
					Currency = "ILS",
					Description = "תשלום עבור הזמנה",
					Source = stripeToken,
				};

				var chargeService = new ChargeService();
				Charge charge = chargeService.Create(chargeOptions);

				if (charge.Status == "succeeded")
				{
					Client loggedInClient = Session["Login"] as Client;

					Shipment shipment = new Shipment()
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

					Response.Redirect("OrderConfirmation.aspx");
				}
				else
				{
					Response.Write("שגיאה בתשלום: " + charge.FailureMessage);
				}
			}
			catch (Exception ex)
			{
				Response.Write($"שגיאה: {ex.Message}");
			}
		}
	}
}
