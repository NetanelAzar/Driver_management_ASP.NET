using System.Collections.Generic; // ייבוא של רכיבי Collections
using DAL; // ייבוא של השכבה הנתונית (Data Access Layer)

namespace BLL
{
	public class Client // מחלקה המייצגת לקוח
	{
		public int ClientID { get; set; } // מזהה הלקוח
		public string ClientName { get; set; } // שם הלקוח
		public string ClientMail { get; set; } // דוא"ל הלקוח
		public string ClientPhone { get; set; } // טלפון הלקוח
		public string CompanyName { get; set; } // שם החברה
		public string ClientPassword { get; set; } // סיסמת הלקוח
		public string CityCode { get; set; } // קוד עיר
		public string Address { get; set; } // כתובת הלקוח

		// קבלת כל הלקוחות
		public static List<Client> GetAll()
		{
			return ClientDAL.GetAll(); // קריאה לשכבת ה-DAL לקבלת כל הלקוחות
		}

		// קבלת לקוח לפי מזהה
		public static Client GetById(int id)
		{
			return ClientDAL.GetById(id); // קריאה לשכבת ה-DAL לקבלת לקוח לפי מזהה
		}

		// שמירת לקוח חדש או עדכון לקוח קיים
		public void Save()
		{
			ClientDAL.Save(this); // קריאה לשכבת ה-DAL לשמירה או עדכון לקוח
		}

		// מחיקת לקוח לפי מזהה
		public static void Delete(int clientId)
		{
			ClientDAL.Delete(clientId); // קריאה לשכבת ה-DAL למחיקת לקוח
		}
	}
}
