using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DATA;

namespace BLL
{
	public class News
	{
		public int NewsID { get; set; }
		public string NewsTitle { get; set; }
		public string NewsSummary { get; set; }
		public string NewsContent { get; set; }
		public DateTime NewsDate { get; set; }

		public static List<News> GetAll()
		{
			DbContext Db = new DbContext();
			string Sql = "SELECT * FROM News";
			DataTable Dt = Db.Execute(Sql);
			List<News> LstNews = new List<News>();

			foreach (DataRow dr in Dt.Rows)
			{
				News news = new News()
				{
					NewsID = dr["NewsID"] != DBNull.Value ? Convert.ToInt32(dr["NewsID"]) : 0,
					NewsTitle = dr["NewsTitle"] != DBNull.Value ? dr["NewsTitle"].ToString() : string.Empty,
					NewsSummary = dr["NewsSummary"] != DBNull.Value ? dr["NewsSummary"].ToString() : string.Empty,
					NewsContent = dr["NewsContent"] != DBNull.Value ? dr["NewsContent"].ToString() : string.Empty,
					NewsDate = dr["NewsDate"] != DBNull.Value ? Convert.ToDateTime(dr["NewsDate"]) : DateTime.MinValue
				};

				LstNews.Add(news);
			}

			Db.Close();
			return LstNews;
		}


		public static News GetById(int Id)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM News WHERE NewsID = {Id}";
			DataTable Dt = Db.Execute(Sql);
			News news = null;

			if (Dt.Rows.Count > 0)
			{
				DataRow dr = Dt.Rows[0];
				news = new News()
				{
					NewsID = dr["NewsID"] != DBNull.Value ? Convert.ToInt32(dr["NewsID"]) : 0,
					NewsTitle = dr["NewsTitle"] != DBNull.Value ? dr["NewsTitle"].ToString() : string.Empty,
					NewsSummary = dr["NewsSummary"] != DBNull.Value ? dr["NewsSummary"].ToString() : string.Empty,
					NewsContent = dr["NewsContent"] != DBNull.Value ? dr["NewsContent"].ToString() : string.Empty,
					NewsDate = dr["NewsDate"] != DBNull.Value ? Convert.ToDateTime(dr["NewsDate"]) : DateTime.MinValue
				};
			}

			Db.Close();
			return news;
		}






		public static void Save(News news)
		{
			DbContext Db = new DbContext();
			string Sql;

			if (news.NewsID <= 0) // אם NewsID קטן או שווה ל-0, אז הוספת חדשות חדשות
			{
				Sql = $"INSERT INTO News (NewsTitle, NewsSummary, NewsContent, NewsDate) " +
					  $"VALUES (N'{news.NewsTitle.Replace("'", "''")}', " +
					  $"N'{news.NewsSummary.Replace("'", "''")}', " +
					  $"N'{news.NewsContent.Replace("'", "''")}', " +
					  $"'{news.NewsDate:yyyy-MM-dd HH:mm:ss}')";
			}
			else
			{
				// עדכון חדשות קיימות
				Sql = $"UPDATE News SET " +
					  $"NewsTitle = N'{news.NewsTitle.Replace("'", "''")}', " +
					  $"NewsSummary = N'{news.NewsSummary.Replace("'", "''")}', " +
					  $"NewsContent = N'{news.NewsContent.Replace("'", "''")}', " +
					  $"NewsDate = '{news.NewsDate:yyyy-MM-dd HH:mm:ss}' " +
					  $"WHERE NewsID = {news.NewsID}";
			}

			Db.Execute(Sql);
			Db.Close();
		}




	}
}
