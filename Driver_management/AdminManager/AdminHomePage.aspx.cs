using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Driver_management.AdminManager
{
	public partial class AdminHomePage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Update active users count only if not a postback
			if (!IsPostBack)
			{
				// Initialize session if needed
				if (Session["Login"] == null)
				{
					Session["Login"] = new List<string>();
				}

				// Update label with active users count
				lblActiveUsersCount.Text = GetActiveUsersCount().ToString();
			}
		}

		public void UserLogin(string username)
		{
			var loggedInUsers = Session["Login"] as List<string>;

			if (loggedInUsers == null)
			{
				loggedInUsers = new List<string>();
				Session["Login"] = loggedInUsers;
			}

			if (!loggedInUsers.Contains(username))
			{
				loggedInUsers.Add(username);
			}

			// Debugging output to trace issue
			System.Diagnostics.Debug.WriteLine($"User logged in: {username}. Total active users: {loggedInUsers.Count}");
		}

		public void UserLogout(string username)
		{
			var loggedInUsers = Session["Login"] as List<string>;

			if (loggedInUsers != null)
			{
				loggedInUsers.Remove(username);
			}

			// Debugging output to trace issue
			System.Diagnostics.Debug.WriteLine($"User logged out: {username}. Total active users: {loggedInUsers?.Count ?? 0}");
		}

		public int GetActiveUsersCount()
		{
			var loggedInUsers = Session["Login"] as List<string>;
			return loggedInUsers != null ? loggedInUsers.Count : 0;
		}
	}
}
