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
					MaxDeliveries = Convert.ToInt32(dr["MaxDeliveries"])
				};

				LstDrivers.Add(driver);
			}

			Db.Close();
			return LstDrivers;
		}

	}
}
