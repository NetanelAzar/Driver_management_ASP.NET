using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Driver_management.AdminManager
{
	public partial class AdminChat : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadCustomers();
			}
			else
			{
				// Handle PostBack
				string selectedCustomerID = hfSelectedCustomerID.Value;
				if (!string.IsNullOrEmpty(selectedCustomerID))
				{
					if (int.TryParse(selectedCustomerID, out int customerId))
					{
						LoadMessages(customerId);
					}
				}
			}
		}

		private void LoadCustomers()
		{
			List<Client> clients = new List<Client>();
			DbContext db = new DbContext();

			try
			{
				string sql = @"
            SELECT c.ClientID, c.ClientName
            FROM Client c
            INNER JOIN CustomerMessages cm ON c.ClientID = cm.CustomerID
            GROUP BY c.ClientID, c.ClientName
        ";

				DataTable dt = db.Execute(sql);

				foreach (DataRow row in dt.Rows)
				{
					Client client = new Client
					{
						ClientID = Convert.ToInt32(row["ClientID"]),
						ClientName = row["ClientName"].ToString()
					};

					clients.Add(client);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Error loading customers: " + ex.Message);
			}
			finally
			{
				db.Close(); // Ensure connection is closed
			}

			rptCustomers.DataSource = clients;
			rptCustomers.DataBind();
		}


		private void LoadMessages(int customerId)
{
    List<CustomerMessage> messages = new List<CustomerMessage>();
    DbContext db = new DbContext();

    try
    {
        string sql = "SELECT * FROM CustomerMessages WHERE CustomerID = @CustomerID ORDER BY SentDate";
        using (SqlCommand cmd = new SqlCommand(sql, db.Conn))
        {
            cmd.Parameters.AddWithValue("@CustomerID", customerId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                CustomerMessage message = new CustomerMessage
                {
                    MessageID = Convert.ToInt32(row["MessageID"]),
                    CustomerID = Convert.ToInt32(row["CustomerID"]),
                    MessageText = row["MessageText"].ToString(),
                    SentDate = Convert.ToDateTime(row["SentDate"]),
                    IsFromCustomer = Convert.ToBoolean(row["IsFromCustomer"])
                };

                messages.Add(message);
            }
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine("Error loading messages: " + ex.Message);
    }
    finally
    {
        db.Close(); // Ensure connection is closed
    }

    rptMessages.DataSource = messages;
    rptMessages.DataBind();
}


		protected void btnPostBack_Click(object sender, EventArgs e)
		{
			string selectedCustomerID = hfSelectedCustomerID.Value;
			System.Diagnostics.Debug.WriteLine("Selected CustomerID: " + selectedCustomerID);

			if (!string.IsNullOrEmpty(selectedCustomerID))
			{
				if (int.TryParse(selectedCustomerID, out int customerId))
				{
					LoadMessages(customerId);
				}
			}
		}
	}
}
