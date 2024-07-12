using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DATA
{
	public class DbContext
	{
		public string ConnStr { get; set; }
		public SqlConnection Conn { get; set; }
		public DbContext()
		{
			//טעינת הגדרות מחרוזת התחברות , יצירת אןבייקט צינור ופתיחת צינור
			ConnStr = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
			Conn = new SqlConnection(ConnStr);
			Conn.Open();


		}
		public void Close()
		{
			Conn.Close();
		}
		public int ExecuteNonQuery(string Sql)
		{

			SqlCommand Cmd = new SqlCommand(Sql, Conn);//יצירת אובייקט תותח פקודות
			return Cmd.ExecuteNonQuery();
		}

		public DataTable Execute(string Sql) //הפונקצייה משמשת לשליפת נתונים מבסיס הננתונים
		{
			SqlCommand Cmd = new SqlCommand(Sql, Conn);//יצירת אובייקט תותח פקודות
			SqlDataAdapter Da = new SqlDataAdapter(Cmd);//יצירת אובייקט מסוג מתאם נתנונים
			DataTable Dt = new DataTable();///יצירת אובייקט מסוג טבלת נתונים
			Da.Fill(Dt);//מילוי טבלת הנתונים בתוצאות הפעלת השאילתה
			return Dt;//החזרת טבלת הנתונים
		}
	}
}