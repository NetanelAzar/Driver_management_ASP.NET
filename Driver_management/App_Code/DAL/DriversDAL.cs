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
					DriverMail = dr["DriverMail"].ToString(),
					DriverPassword = dr["DriverPassword"].ToString(),
					DriverPhone = dr["DriverPhone"].ToString(),
					Picname = dr["Picname"].ToString(),
					AddDate = Convert.ToDateTime(dr["AddDate"]),
					Status = Convert.ToInt32(dr["Status"]),
					MaxDeliveries = Convert.ToInt32(dr["MaxDeliveries"])
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
				Sql = $"INSERT INTO Drivers (DriverCode, DriverName, Address, CityCode, DriverMail, DriverPassword, Picname, Status, MaxDeliveries) " +
					  $"VALUES ('{driver.DriverCode}', '{driver.DriverName}', '{driver.Address}', {driver.CityCode}, '{driver.DriverMail}', '{driver.DriverPassword}', '{driver.Picname}', {driver.Status}, {driver.MaxDeliveries})";
			}
			else
			{
				Sql = $"UPDATE Drivers SET " +
					  $"DriverCode = '{driver.DriverCode}', " +
					  $"DriverName = '{driver.DriverName}', " +
					  $"Address = '{driver.Address}', " +
					  $"CityCode = {driver.CityCode}, " +
					  $"DriverMail = '{driver.DriverMail}', " +
					  $"DriverPassword = '{driver.DriverPassword}', " +
					  $"Picname = '{driver.Picname}', " +
					  $"Status = {driver.Status}, " +
					  $"MaxDeliveries = {driver.MaxDeliveries} " +
					  $"WHERE DriverID = {driver.DriverID}";
			}

			Db.Execute(Sql);
			Db.Close();
		}
	}
}
