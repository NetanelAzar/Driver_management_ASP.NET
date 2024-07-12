using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;
using OfficeOpenXml;
using Newtonsoft.Json.Linq;
using BLL;

namespace Driver_management.DriverManagement
{
	public partial class ShowMap : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["Login"] == null)
			{
				Response.Redirect("~/LoginRegister.aspx");
			}
		}

		private static Drivers GetCurrentDriver()
		{
			if (HttpContext.Current.Session["Login"] != null)
			{
				return (Drivers)HttpContext.Current.Session["Login"];
			}
			else
			{
				HttpContext.Current.Response.Redirect("~/LoginRegister.aspx");
				return null;
			}
		}

		[WebMethod]
		public static List<object> GetAddresses()
		{
			Drivers currentUser = GetCurrentDriver();
			if (currentUser == null)
			{
				return new List<object>(); // Return an empty list if no user is logged in
			}

			List<Shipment> shipments = Shipment.GetShipmentsByUserId(currentUser.DriverID);
			List<object> result = new List<object>();

			foreach (var shipment in shipments)
			{
				dynamic location = GetGeoLocation(shipment);
				if (location != null)
				{

					result.Add(new
					{
						City = shipment.DestinationCity,
						Address = shipment.DestinationAddress,
						Latitude = location.Latitude,
						Longitude = location.Longitude
					});
				}
			}
			return result;
		}

		private static dynamic GetGeoLocation(Shipment shipment)
		{
			string city = City.GetCityNameById(shipment.DestinationCity); // Get city name by ID
			string query = $"{shipment.DestinationAddress}, {city}, Israel";
			string url = $"https://nominatim.openstreetmap.org/search?q={HttpUtility.UrlEncode(query)}&format=json&addressdetails=1";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = "GET";
			request.UserAgent = ".NET Application";

			try
			{
				using (WebResponse response = request.GetResponse())
				using (Stream stream = response.GetResponseStream())
				using (StreamReader reader = new StreamReader(stream))
				{
					string jsonResponse = reader.ReadToEnd();
					JArray jsonArray = JArray.Parse(jsonResponse);
					if (jsonArray.Count > 0)
					{
						var firstResult = jsonArray[0];
						var latitude = (string)firstResult["lat"];
						var longitude = (string)firstResult["lon"];
						return new
						{
							Latitude = latitude,
							Longitude = longitude
						};
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return null;
			}
			return null;
		}


		[WebMethod]
		public static string DownloadAddresses()
		{
			List<Shipment> shipments = Shipment.GetAll();
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			using (ExcelPackage package = new ExcelPackage())
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Shipments");

				worksheet.Cells[1, 1].Value = "ShipmentID";
				worksheet.Cells[1, 2].Value = "CustomerID";
				worksheet.Cells[1, 3].Value = "SourceAddress";
				worksheet.Cells[1, 4].Value = "SourceCity";
				worksheet.Cells[1, 5].Value = "DestinationAddress";
				worksheet.Cells[1, 6].Value = "DestinationCity";
				worksheet.Cells[1, 7].Value = "CustomerPhone";
				worksheet.Cells[1, 8].Value = "PickupDate";
				worksheet.Cells[1, 9].Value = "DeliveryDate";
				worksheet.Cells[1, 10].Value = "ShipmentDate";
				worksheet.Cells[1, 11].Value = "NumberOfPackages";
				worksheet.Cells[1, 12].Value = "DriverCode";

				int row = 2;
				foreach (var shipment in shipments)
				{
					worksheet.Cells[row, 1].Value = shipment.ShipmentID;
					worksheet.Cells[row, 2].Value = shipment.CustomerID;
					worksheet.Cells[row, 3].Value = shipment.SourceAddress;
					worksheet.Cells[row, 4].Value = shipment.SourceCity;
					worksheet.Cells[row, 5].Value = shipment.DestinationAddress;
					worksheet.Cells[row, 6].Value = shipment.DestinationCity;
					worksheet.Cells[row, 7].Value = shipment.CustomerPhone;
					worksheet.Cells[row, 8].Value = shipment.PickupDate.ToString("yyyy-MM-dd");
					worksheet.Cells[row, 9].Value = shipment.DeliveryDate.ToString("yyyy-MM-dd");
					worksheet.Cells[row, 10].Value = shipment.ShipmentDate.ToString("yyyy-MM-dd");
					worksheet.Cells[row, 11].Value = shipment.NumberOfPackages;
					worksheet.Cells[row, 12].Value = shipment.DriverID;

					row++;
				}

				var excelData = package.GetAsByteArray();
				var base64 = Convert.ToBase64String(excelData);
				return base64;
			}
		}
	}
}
