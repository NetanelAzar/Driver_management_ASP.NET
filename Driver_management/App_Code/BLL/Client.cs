using System.Collections.Generic;
using DAL;

namespace BLL
{
	public class Client
	{
		public int ClientID { get; set; }
		public string ClientName { get; set; }
		public string ClientMail { get; set; }
		public string ClientPhone { get; set; }
		public string CompanyName { get; set; }
		public string ClientPassword {  get; set; }
		public string CityCode { get; set; }
		public string Address { get; set; }

		public static List<Client> GetAll()
		{
			return ClientDAL.GetAll();
		}

		public static Client GetById(int id)
		{
			return ClientDAL.GetById(id);
		}

		public void Save()
		{
			ClientDAL.Save(this);
		}


		public static void Delete(int clientId)
		{
			ClientDAL.Delete(clientId);
		}
	}
}
