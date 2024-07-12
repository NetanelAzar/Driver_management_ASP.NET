using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Driver_management.AdminManager
{
	public partial class CitiesList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadCities();
			}
		}

		private void LoadCities()
		{
			// קבלת רשימת הערים מה-BLL
			List<City> cities = City.GetAll();

			// מיון הערים לפי שם העיר
			cities = cities.OrderBy(c => c.CityName).ToList();

			// קביעת מקור הנתונים של ה-Repeater
			RptCities.DataSource = cities;
			RptCities.DataBind();
		}

		protected void RptCities_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "Delete")
			{
				int cityId = Convert.ToInt32(e.CommandArgument);
				DeleteCity(cityId);
				LoadCities();
			}
		}

		private void DeleteCity(int cityId)
		{
			City.Delete(cityId);
		}

		// פונקציה לעיצוב תאריך
		public string FormatDate(object date)
		{
			if (date != null && DateTime.TryParse(date.ToString(), out DateTime dt))
			{
				return dt.ToString("dd/MM/yyyy");
			}
			return string.Empty;
		}
	}
}