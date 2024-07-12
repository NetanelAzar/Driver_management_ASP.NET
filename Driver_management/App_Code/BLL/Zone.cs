using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace BLL
{
	public class Zone
	{

		public int ZoneId { get; set; }
		public string ZoneName { get; set; }
		public string CityCodes { get; set; }

		public static List<Zone> GetAll()
		{
			return ZoneDAL.GetAll();
		}

		public static Zone GetById(int id)
		{
			return ZoneDAL.GetById(id);
		}

		public void Save()
		{
			ZoneDAL.Save(this);
		}

		public static string GetZoneNameById(int zoneId)
		{
			return ZoneDAL.GetZoneNameById(zoneId);
		}

		public List<int> GetCityCodeList()
		{
			List<int> cityCodeList = new List<int>();
			if (!string.IsNullOrEmpty(CityCodes))
			{
				foreach (var code in CityCodes.Split(','))
				{
					if (int.TryParse(code, out int cityCode))
					{
						cityCodeList.Add(cityCode);
					}
				}
			}
			return cityCodeList;
		}

		public void SetCityCodeList(List<int> cityCodes)
		{
			CityCodes = string.Join(",", cityCodes);
		}
	}
}