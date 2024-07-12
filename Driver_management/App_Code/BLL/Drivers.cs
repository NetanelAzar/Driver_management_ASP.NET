using System;
using System.Collections.Generic;
using DAL;

namespace BLL
{
	public class Drivers
	{
		public int DriverID { get; set; }
		public string DriverCode { get; set; }
		public string DriverName { get; set; }
		public string Address { get; set; }
		public int CityCode { get; set; }
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

	}
}
