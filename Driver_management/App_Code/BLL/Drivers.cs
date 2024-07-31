using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using DATA;

namespace BLL
{
	public class Drivers
	{
		public int DriverID { get; set; }
		public string DriverCode { get; set; }
		public string DriverName { get; set; }
		public string Address { get; set; }
		public int CityCode { get; set; }
		public string zoneID {  get; set; }
		public string DriverMail { get; set; }
		public string DriverPassword { get; set; }
		public string DriverPhone { get; set; }
		public string Picname { get; set; }
		public DateTime AddDate { get; set; }
		public int Status { get; set; }
		public int MaxDeliveries { get; set; }
		public int CurrentDeliveries { get; set; } 

		public static List<Drivers> GetAll()
		{
			return DriversDAL.GetAll();
		}

		public static Drivers GetById(int Id)
		{
			return DriversDAL.GetById(Id);
		}

		public void Save()
		{
			DriversDAL.Save(this);
		}


		public static List<Drivers> GetDriversByZoneId(string zoneId)
		{
			DbContext Db = new DbContext();
			string Sql = $"Select * from Drivers where zoneID='{zoneId}'";
			DataTable Dt = Db.Execute(Sql);
			List<Drivers> LstDrivers = new List<Drivers>();

			foreach (DataRow dr in Dt.Rows)
			{
				Drivers driver = new Drivers()
				{
					DriverID = Convert.ToInt32(dr["DriverID"]),
					DriverName = dr["DriverName"].ToString(),
					DriverCode = dr["DriverCode"].ToString(),
					Address = dr["Address"].ToString(),
					CityCode = Convert.ToInt32(dr["CityCode"]),
					zoneID = dr["zoneID"].ToString(),
					DriverMail = dr["DriverMail"].ToString(),
					DriverPassword = dr["DriverPassword"].ToString(),
					DriverPhone = dr["DriverPhone"].ToString(),
					Picname = dr["Picname"].ToString(),
					AddDate = Convert.ToDateTime(dr["AddDate"]),
					Status = Convert.ToInt32(dr["Status"]),
					MaxDeliveries = Convert.ToInt32(dr["MaxDeliveries"]),
					CurrentDeliveries = Convert.ToInt32(dr["CurrentDeliveries"])
				};

				LstDrivers.Add(driver);
			}

			Db.Close();
			return LstDrivers;
		}




		public static void UpdateDriverAvailableSpace(int driverId)
		{
			try
			{
				DbContext Db = new DbContext();

				// Get all shipments for the driver that are not delivered
				string sqlShipments = $"SELECT * FROM Shipments WHERE DriverID = {driverId} AND ShippingStatus != 'נמסר'";
				DataTable dtShipments = Db.Execute(sqlShipments);

				int undeliveredPackages = 0;

				foreach (DataRow row in dtShipments.Rows)
				{
					undeliveredPackages += Convert.ToInt32(row["NumberOfPackages"]);
				}

				// Get driver details
				string sqlDriver = $"SELECT MaxDeliveries FROM Drivers WHERE DriverID = {driverId}";
				DataTable dtDriver = Db.Execute(sqlDriver);

				if (dtDriver.Rows.Count > 0)
				{
					int maxDeliveries = Convert.ToInt32(dtDriver.Rows[0]["MaxDeliveries"]);
					int availableSpace = maxDeliveries - undeliveredPackages;

					// Update the driver's available space
					string sqlUpdate = $"UPDATE Drivers SET CurrentDeliveries = {availableSpace} WHERE DriverID = {driverId}";
					Db.ExecuteNonQuery(sqlUpdate);
				}

				Db.Close();
			}
			catch (Exception ex)
			{
				// Handle or log the error
				Console.WriteLine($"Error updating driver available space: {ex.Message}");
			}
		}

	}
}
