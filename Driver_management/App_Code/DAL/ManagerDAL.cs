using System;
using System.Collections.Generic;
using System.Data;
using BLL;
using DATA;

namespace DAL
{
	public class ManagerDAL
	{
		public static List<Manager> GetAll()
		{
			DbContext Db = new DbContext();
			string Sql = "SELECT * FROM Managers";
			DataTable Dt = Db.Execute(Sql);
			List<Manager> managers = new List<Manager>();

			foreach (DataRow row in Dt.Rows)
			{
				Manager manager = new Manager
				{
					ManagerID = Convert.ToInt32(row["ManagerID"]),
					Name = row["Name"].ToString(),
					Email = row["Email"].ToString(),
					Password = row["Password"].ToString(),
					Phone = row["Phone"].ToString()
				};
				managers.Add(manager);
			}

			Db.Close();
			return managers;
		}

		public static Manager GetById(int id)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM Managers WHERE ManagerID={id}";
			DataTable Dt = Db.Execute(Sql);
			Manager manager = null;

			if (Dt.Rows.Count > 0)
			{
				DataRow row = Dt.Rows[0];
				manager = new Manager
				{
					ManagerID = Convert.ToInt32(row["ManagerID"]),
					Name = row["Name"].ToString(),
					Email = row["Email"].ToString(),
					Password = row["Password"].ToString(),
					Phone = row["Phone"].ToString()
				};
			}

			Db.Close();
			return manager;
		}

		public static void Save(Manager manager)
		{
			DbContext Db = new DbContext();
			string Sql;

			if (manager.ManagerID <= 0)
			{
				Sql = $"INSERT INTO Managers (Name, Email, Password, Phone) " +
					  $"VALUES (N'{manager.Name}', '{manager.Email}', '{manager.Password}', '{manager.Phone}')";
			}
			else
			{
				Sql = $"UPDATE Managers SET " +
					  $"Name = N'{manager.Name}', " +
					  $"Email = '{manager.Email}', " +
					  $"Password = '{manager.Password}', " +
					  $"Phone = '{manager.Phone}' " +
					  $"WHERE ManagerID = {manager.ManagerID}";
			}

			Db.Execute(Sql);
			Db.Close();
		}

		public static void Delete(int managerId)
		{
			DbContext Db = new DbContext();
			string Sql = $"DELETE FROM Managers WHERE ManagerID = {managerId}";
			Db.Execute(Sql);
			Db.Close();
		}
	}
}
