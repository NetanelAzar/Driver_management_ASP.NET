using System;
using System.Collections.Generic;
using System.Data;
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
					NewsID = Convert.ToInt32(dr["NewsID"]),
					NewsTitle = dr["NewsTitle"].ToString(),
					NewsSummary = dr["NewsSummary"].ToString(),
					NewsContent = dr["NewsContent"].ToString(),
					NewsDate = Convert.ToDateTime(dr["NewsDate"])
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
					NewsID = Convert.ToInt32(dr["NewsID"]),
					NewsTitle = dr["NewsTitle"].ToString(),
					NewsSummary = dr["NewsSummary"].ToString(),
					NewsContent = dr["NewsContent"].ToString(),
					NewsDate = Convert.ToDateTime(dr["NewsDate"])
				};
			}

			Db.Close();
			return news;
		}

		public void Save()
		{
			DbContext Db = new DbContext();
			string Sql;

			if (this.NewsID < 0)
			{
				Sql = $"INSERT INTO News (NewsTitle, NewsSummary, NewsContent, NewsDate) " +
					  $"VALUES ('{this.NewsTitle}', '{this.NewsSummary}', '{this.NewsContent}', '{this.NewsDate}')";
			}
			else
			{
				Sql = $"UPDATE News SET " +
					  $"NewsTitle = '{this.NewsTitle}', " +
					  $"NewsSummary = '{this.NewsSummary}', " +
					  $"NewsContent = '{this.NewsContent}', " +
					  $"NewsDate = '{this.NewsDate}' " +
					  $"WHERE NewsID = {this.NewsID}";
			}

			Db.Execute(Sql);
			Db.Close();
		}
	}
}
