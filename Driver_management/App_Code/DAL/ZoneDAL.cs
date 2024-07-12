using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BLL;

namespace DAL
{
	public class ZoneDAL
	{
		public static List<Zone> GetAll()
		{
			DbContext Db = new DbContext();
			string Sql = "Select * from Zone";
			DataTable Dt = Db.Execute(Sql);
			List<Zone> LstZone = new List<Zone>();

			for (int i = 0; i < Dt.Rows.Count; i++)
			{
				Zone zone = new Zone()
				{
					ZoneId = int.Parse(Dt.Rows[i]["ZoneId"].ToString()),
					ZoneName = Dt.Rows[i]["ZoneName"].ToString(),
					CityCodes = Dt.Rows[i]["CityCodes"].ToString()
				};
				LstZone.Add(zone);
			}

			Db.Close();
			return LstZone;
		}

		public static Zone GetById(int id)
		{
			DbContext Db = new DbContext();
			string Sql = $"Select * from Zone where ZoneId={id}";
			DataTable Dt = Db.Execute(Sql);
			Zone zone = null;

			if (Dt.Rows.Count > 0)
			{
				zone = new Zone()
				{
					ZoneId = int.Parse(Dt.Rows[0]["ZoneId"].ToString()),
					ZoneName = Dt.Rows[0]["ZoneName"].ToString(),
					CityCodes = Dt.Rows[0]["CityCodes"].ToString()
				};
			}

			Db.Close();
			return zone;
		}

		public static void Save(Zone zone)
		{
			DbContext Db = new DbContext();
			string Sql;

			if (zone.ZoneId == 0)
			{
				Sql = $"INSERT INTO Zone (ZoneName, CityCodes) VALUES ('{zone.ZoneName}', '{zone.CityCodes}')";
			}
			else
			{
				Sql = $"UPDATE Zone SET " +
					  $"ZoneName = '{zone.ZoneName}', " +
					  $"CityCodes = '{zone.CityCodes}' " +
					  $"WHERE ZoneId = {zone.ZoneId}";
			}

			Db.Execute(Sql);
			Db.Close();
		}

		public static string GetZoneNameById(int zoneId)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT ZoneName FROM Zone WHERE ZoneId = {zoneId}";
			DataTable Dt = Db.Execute(Sql);
			string zoneName = string.Empty;

			if (Dt.Rows.Count > 0)
			{
				zoneName = Dt.Rows[0]["ZoneName"].ToString();
			}

			Db.Close();
			return zoneName;
		}
	}
}