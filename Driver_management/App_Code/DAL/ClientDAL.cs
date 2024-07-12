using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL;
using DATA;

namespace DAL
{
	public class ClientDAL
	{
		public static List<Client> GetAll()
		{
			DbContext Db = new DbContext();
			string Sql = "SELECT * FROM Client";
			DataTable Dt = Db.Execute(Sql);
			List<Client> clients = new List<Client>();

			foreach (DataRow row in Dt.Rows)
			{
				Client client = new Client()
				{
					ClientID = Convert.ToInt32(row["ClientID"]),
					ClientName = row["ClientName"].ToString(),
					ClientMail = row["ClientMail"].ToString(),
					ClientPhone = row["ClientPhone"].ToString(),
					CompanyName = row["CompanyName"].ToString(),
					ClientPassword = row["ClientPassword"].ToString(),
					CityCode = row["CityCode"].ToString(),
					Address = row["Address"].ToString()
				};

				clients.Add(client);
			}

			Db.Close();
			return clients;
		}

		public static Client GetById(int id)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM Client WHERE ClientID = {id}";
			DataTable Dt = Db.Execute(Sql);
			Client client = null;

			if (Dt.Rows.Count > 0)
			{
				DataRow row = Dt.Rows[0];
				client = new Client()
				{
					ClientID = Convert.ToInt32(row["ClientID"]),
					ClientName = row["ClientName"].ToString(),
					ClientMail = row["ClientMail"].ToString(),
					ClientPhone = row["ClientPhone"].ToString(),
					CompanyName = row["CompanyName"].ToString(),
					CityCode = row["CityCode"].ToString(),
					ClientPassword = row["ClientPassword"].ToString(),
					Address = row["Address"].ToString()
				};
			}

			Db.Close();
			return client;
		}

		public static void Save(Client client)
		{
			DbContext Db = new DbContext();
			string sql;

			if (client.ClientID <= 0)
			{
				sql = "INSERT INTO Client (ClientName, ClientMail, ClientPhone, CompanyName, CityCode, Address,ClientPassword) " +
					  "VALUES (@ClientName, @ClientMail, @ClientPhone, @CompanyName, @CityCode, @Address,@ClientPassword)";
			}
			else
			{
				sql = "UPDATE Client SET " +
					  "ClientName = @ClientName, " +
					  "ClientMail = @ClientMail, " +
					  "ClientPhone = @ClientPhone, " +
					  "CompanyName = @CompanyName, " +
					  "CityCode = @CityCode, " +
					  "Address = @Address " +
					  "ClientPassword= @ClientPassword, " +
					  "WHERE ClientID = @ClientID";
			}

			SqlCommand cmd = new SqlCommand(sql, Db.Conn);
			cmd.Parameters.AddWithValue("@ClientID", client.ClientID);
			cmd.Parameters.AddWithValue("@ClientName", client.ClientName);
			cmd.Parameters.AddWithValue("@ClientMail", client.ClientMail);
			cmd.Parameters.AddWithValue("@ClientPhone", client.ClientPhone);
			cmd.Parameters.AddWithValue("@CompanyName", client.CompanyName ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("@ClientPassword", client.ClientPassword);
			cmd.Parameters.AddWithValue("@CityCode", client.CityCode);
			cmd.Parameters.AddWithValue("@Address", client.Address);

			cmd.ExecuteNonQuery();
			Db.Close();
		}





		public static void Delete(int clientId)
		{
			DbContext Db = new DbContext();
			string Sql = $"DELETE FROM Client WHERE ClientID = {clientId}";
			Db.Execute(Sql);
			Db.Close();
		}
	}
}
