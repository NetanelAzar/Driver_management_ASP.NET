using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace BLL
{
	public class Manager
	{
		public int ManagerID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }

		public static List<Manager> GetAll()
		{
			return ManagerDAL.GetAll();
		}

		public static Manager GetById(int id)
		{
			return ManagerDAL.GetById(id);
		}

		public void Save()
		{
			ManagerDAL.Save(this);
		}

		public static void Delete(int managerId)
		{
			ManagerDAL.Delete(managerId);
		}
	}
}