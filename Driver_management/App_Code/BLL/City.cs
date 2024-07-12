using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
	public class City
	{
		public int CityId { get; set; }
		public string CityCode { get; set; }
		public string CityName { get; set; }
		public int Status { get; set; }
		public DateTime AddDate { get; set; }

		// פעולה לקבלת כל הערים
		public static List<City> GetAll()
		{
			return CityDAL.GetAll();
		}

		// פעולה לקבלת עיר לפי מזהה
		public static City GetById(int id)
		{
			return CityDAL.GetById(id);
		}

		// פעולה לשמירת עיר
		public void Save()
		{
			CityDAL.Save(this);
		}

		// פעולה למחיקת עיר לפי מזהה
		public static void Delete(int id)
		{
			CityDAL.Delete(id);
		}

		// פעולה לקבלת שם עיר לפי מזהה
		public static string GetCityNameById(int cityId)
		{
			return CityDAL.GetCityNameById(cityId);
		}
	}
}
