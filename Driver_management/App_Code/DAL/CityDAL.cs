using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
	public class CityDAL
	{


		public static List<City> GetAll()
		{
			DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס הנתונים
			string Sql = "Select * from City"; // הגדרת משפט השאילתה
			DataTable Dt = Db.Execute(Sql); // הפעלת השאילתה וקבלת התוצאות לתוך טבלת הנתונים
			List<City> LstCity = new List<City>();

			for (int i = 0; i < Dt.Rows.Count; i++)
			{
				City Cl = new City()
				{
					CityId = int.Parse(Dt.Rows[i]["CityId"] + ""),
					CityCode = Dt.Rows[i]["CityCode"] + "",
					CityName = Dt.Rows[i]["CityName"] + "",
					Status = int.Parse(Dt.Rows[i]["Status"] + ""),
					AddDate = DateTime.Parse(Dt.Rows[i]["AddDate"] + "")
				};
				LstCity.Add(Cl);
			}

			Db.Close();
			return LstCity;
		}

		public static City GetById(int Id)
		{
			DbContext Db = new DbContext(); // יצירת אובייקט מסוג גישה לבסיס הנתונים
			string Sql = $"Select * from City where CityId={Id}"; // הגדרת משפט השאילתה
			DataTable Dt = Db.Execute(Sql); // הפעלת השאילתה וקבלת התוצאות לתוך טבלת הנתונים
			City Tmp = null;

			if (Dt.Rows.Count > 0)
			{
				Tmp = new City()
				{
					CityCode = Dt.Rows[0]["CityCode"] + "",
					CityName = Dt.Rows[0]["CityName"] + "",
					Status = int.Parse(Dt.Rows[0]["Status"] + ""),
					AddDate = DateTime.Parse(Dt.Rows[0]["AddDate"] + "")
				};
			}

			Db.Close();
			return Tmp;
		}

		public static void Save(City city)
		{
			DbContext Db = new DbContext();
			string Sql;

			if (city.CityId == 0)
			{
				// INSERT לקוח חדש
				Sql = $"INSERT INTO City (CityCode, CityName,Status) " +
					  $"VALUES ('{city.CityCode}', '{city.CityName}',{city.Status})";
			}
			else
			{
				// עדכון לקוח
				Sql = $"UPDATE City SET " +
					  $"CityCode = '{city.CityCode}', " +
					  $"CityName = '{city.CityName}', " +
					  $"Status = {city.Status}, " +
					  $"WHERE CityId = {city.CityId}";
			}

			Db.Execute(Sql); // ביצוע השאילתה על בסיס הנתונים
			Db.Close(); // סגירת הגישה לבסיס הנתונים



		}




		public static string GetCityNameById(int cityId)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT CityName FROM City WHERE CityId = {cityId}";
			DataTable Dt = Db.Execute(Sql);
			string cityName = string.Empty;

			if (Dt.Rows.Count > 0)
			{
				cityName = Dt.Rows[0]["CityName"].ToString();
			}

			Db.Close();
			return cityName;
		}




		public static void Delete(int cityId)
		{
			DbContext Db = new DbContext();
			string Sql = $"DELETE FROM City WHERE CityId = {cityId}";
			Db.Execute(Sql);
			Db.Close();
		}
	}
}