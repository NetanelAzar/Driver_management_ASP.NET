using System;
using System.Collections.Generic;
using System.Data;
using BLL;
using DATA;

namespace DAL
{
	public class DriversDAL
	{
		public static List<Drivers> GetAll()
		{
			DbContext Db = new DbContext();
			string Sql = "Select * from Drivers";
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
					zoneID=dr["zoneID"].ToString(),
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

		public static Drivers GetById(int Id)
		{
			DbContext Db = new DbContext();
			string Sql = $"Select * from Drivers where DriverID={Id}";
			DataTable Dt = Db.Execute(Sql);
			Drivers driver = null;

			if (Dt.Rows.Count > 0)
			{
				DataRow dr = Dt.Rows[0];
				driver = new Drivers()
				{
					DriverID = Convert.ToInt32(dr["DriverID"]),
					DriverCode = dr["DriverCode"].ToString(),
					DriverName = dr["DriverName"].ToString(),
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
			}

			Db.Close();
			return driver;
		}






		public static void Save(Drivers driver)
		{
			DbContext Db = new DbContext();
			string Sql;

			if (driver.DriverID < 0)
			{
				Sql = $"INSERT INTO Drivers (DriverCode,DriverPhone, DriverName, Address, CityCode, ZoneID, DriverMail, DriverPassword, Picname, Status, MaxDeliveries, CurrentDeliveries) " +
					  $"VALUES ('{driver.DriverCode}','{driver.DriverPhone}', '{driver.DriverName}', '{driver.Address}', {driver.CityCode}, {driver.zoneID}, '{driver.DriverMail}', '{driver.DriverPassword}', '{driver.Picname}', {driver.Status}, {driver.MaxDeliveries}, {driver.CurrentDeliveries})";
			}
			else
			{
				Sql = $"UPDATE Drivers SET " +
					  $"DriverCode = '{driver.DriverCode}', " +
					  $"DriverName = '{driver.DriverName}', " +
					  $"DriverPhone = '{driver.DriverPhone}', " + 
					  $"Address = '{driver.Address}', " +
					  $"CityCode = {driver.CityCode}, " +
					  $"ZoneID = {driver.zoneID}, " +
					  $"DriverMail = '{driver.DriverMail}', " +
					  $"DriverPassword = '{driver.DriverPassword}', " +
					  $"Picname = '{driver.Picname}', " +
					  $"Status = {driver.Status}, " +
					  $"MaxDeliveries = {driver.MaxDeliveries}, " +  // Note the comma here
					  $"CurrentDeliveries = {driver.CurrentDeliveries} " +
					  $"WHERE DriverID = {driver.DriverID}";
			}

			Db.Execute(Sql);
			Db.Close();
		}

	}
}
