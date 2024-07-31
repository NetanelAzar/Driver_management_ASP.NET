using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using DATA;
using System.Data.SqlClient;

namespace DAL
{
	public class ShipmentDAL
	{
		// בקוד אחר שבו מתבצעת המרה של DateTime
		public static List<Shipment> GetAll()
		{
			DbContext Db = new DbContext();
			string Sql = "SELECT * FROM Shipments";
			DataTable Dt = Db.Execute(Sql);
			List<Shipment> shipments = new List<Shipment>();

			foreach (DataRow row in Dt.Rows)
			{
				Shipment shipment = new Shipment
				{
					ShipmentID = Convert.ToInt32(row["ShipmentID"]),
					CustomerID = row["CustomerID"] != DBNull.Value ? Convert.ToInt32(row["CustomerID"]) : 0,
					SourceAddress = row["SourceAddress"].ToString(),
					SourceCity = row["SourceCity"].ToString(),
					DestinationAddress = row["DestinationAddress"].ToString(),
					DestinationCity = row["DestinationCity"] != DBNull.Value ? Convert.ToInt32(row["DestinationCity"]) : 0,
					CustomerPhone = row["CustomerPhone"].ToString(),
					PickupDate = row["PickupDate"] != DBNull.Value ? DateTime.Parse(row["PickupDate"].ToString()) : new DateTime(1, 1, 1),
					DeliveryDate = row["DeliveryDate"] != DBNull.Value ? DateTime.Parse(row["DeliveryDate"].ToString()) : new DateTime(1, 1, 1),
					ShipmentDate = row["ShipmentDate"] != DBNull.Value ? DateTime.Parse(row["ShipmentDate"].ToString()) : new DateTime(1, 1, 1),
					NumberOfPackages = row["NumberOfPackages"] != DBNull.Value ? Convert.ToInt32(row["NumberOfPackages"]) : 0,
					DriverID = row["DriverID"] != DBNull.Value ? Convert.ToInt32(row["DriverID"]) : 0,
					ShippingStatus = row["ShippingStatus"].ToString(),
					OrderDate = row["OrderDate"] != DBNull.Value ? DateTime.Parse(row["OrderDate"].ToString()) : new DateTime(1, 1, 1),
					OrderNumber = row["OrderNumber"] != DBNull.Value ? Convert.ToInt32(row["OrderNumber"]) : 0,
					Payment = row["Payment"] != DBNull.Value ? Convert.ToInt32(row["Payment"]) : 0,
				};
				shipments.Add(shipment);
			}

			Db.Close();
			return shipments;
		}


		public static Shipment GetById(int id)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM Shipments WHERE ShipmentID={id}";
			DataTable Dt = Db.Execute(Sql);
			Shipment shipment = null;

			if (Dt.Rows.Count > 0)
			{
				DataRow row = Dt.Rows[0];
				shipment = new Shipment
				{
					ShipmentID = Convert.ToInt32(row["ShipmentID"]),
					CustomerID = row["CustomerID"] != DBNull.Value ? Convert.ToInt32(row["CustomerID"]) : 0,
					SourceAddress = row["SourceAddress"].ToString(),
					SourceCity = row["SourceCity"].ToString(),
					DestinationAddress = row["DestinationAddress"].ToString(),
					DestinationCity = row["DestinationCity"] != DBNull.Value ? Convert.ToInt32(row["DestinationCity"]) : 0,
					CustomerPhone = row["CustomerPhone"].ToString(),
					PickupDate = row["PickupDate"] != DBNull.Value ? DateTime.Parse(row["PickupDate"].ToString()) : new DateTime(1, 1, 1),
					DeliveryDate = row["DeliveryDate"] != DBNull.Value ? DateTime.Parse(row["DeliveryDate"].ToString()) : new DateTime(1, 1, 1),
					ShipmentDate = row["ShipmentDate"] != DBNull.Value ? DateTime.Parse(row["ShipmentDate"].ToString()) : new DateTime(1, 1, 1),
					NumberOfPackages = row["NumberOfPackages"] != DBNull.Value ? Convert.ToInt32(row["NumberOfPackages"]) : 0,
					DriverID = row["DriverID"] != DBNull.Value ? Convert.ToInt32(row["DriverID"]) : 0,
					ShippingStatus = row["ShippingStatus"].ToString(),
					OrderDate = row["OrderDate"] != DBNull.Value ? DateTime.Parse(row["OrderDate"].ToString()) : new DateTime(1, 1, 1),
					OrderNumber = row["OrderNumber"] != DBNull.Value ? Convert.ToInt32(row["OrderNumber"]) : 0,
					Payment = row["Payment"] != DBNull.Value ? Convert.ToInt32(row["Payment"]) : 0,
				};
			}

			Db.Close();
			return shipment;
		}



		public static void Save(Shipment shipment)
		{
			DbContext Db = new DbContext();
			string Sql;

			if (shipment.ShipmentID <= 0)
			{
				Sql = $"INSERT INTO Shipments (CustomerID, SourceAddress, SourceCity, DestinationAddress, " +
					  $"DestinationCity, CustomerPhone, PickupDate, DeliveryDate, ShipmentDate, NumberOfPackages, DriverID, ShippingStatus, OrderDate, OrderNumber, Payment) " +
					  $"VALUES ({shipment.CustomerID}, N'{shipment.SourceAddress}', N'{shipment.SourceCity}', " +
					  $"N'{shipment.DestinationAddress}', N'{shipment.DestinationCity}', " +
					  $"N'{shipment.CustomerPhone}', " +
					  $"{(shipment.PickupDate.Year > 1 ? $"'{shipment.PickupDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"{(shipment.DeliveryDate.Year > 1 ? $"'{shipment.DeliveryDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"{(shipment.ShipmentDate.Year > 1 ? $"'{shipment.ShipmentDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"{shipment.NumberOfPackages}, {shipment.DriverID}, " +
					  $"N'{shipment.ShippingStatus}', " +
					  $"{(shipment.OrderDate.Year > 1 ? $"'{shipment.OrderDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"{shipment.OrderNumber}, " +
					  $"{shipment.Payment})";
			}
			else
			{
				Sql = $"UPDATE Shipments SET " +
					  $"CustomerID = {shipment.CustomerID}, " +
					  $"SourceAddress = N'{shipment.SourceAddress}', " +
					  $"SourceCity = N'{shipment.SourceCity}', " +
					  $"DestinationAddress = N'{shipment.DestinationAddress}', " +
					  $"DestinationCity = N'{shipment.DestinationCity}', " +
					  $"CustomerPhone = N'{shipment.CustomerPhone}', " +
					  $"PickupDate = {(shipment.PickupDate.Year > 1 ? $"'{shipment.PickupDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"DeliveryDate = {(shipment.DeliveryDate.Year > 1 ? $"'{shipment.DeliveryDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"ShipmentDate = {(shipment.ShipmentDate.Year > 1 ? $"'{shipment.ShipmentDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"NumberOfPackages = {shipment.NumberOfPackages}, " +
					  $"DriverID = {shipment.DriverID}, " +
					  $"ShippingStatus = N'{shipment.ShippingStatus}', " +
					  $"OrderDate = {(shipment.OrderDate.Year > 1 ? $"'{shipment.OrderDate:yyyy-MM-dd HH:mm:ss}'" : "NULL")}, " +
					  $"OrderNumber = {shipment.OrderNumber}, " +
					  $"Payment = {shipment.Payment} " + // הוספתי את השדה Payment בעדכון
					  $"WHERE ShipmentID = {shipment.ShipmentID}";
			}

			Db.Execute(Sql);
			Db.Close();
		}


		public static List<Shipment> GetShipmentsByUserId(int userId)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM Shipments WHERE DriverID = {userId}";
			DataTable Dt = Db.Execute(Sql);
			List<Shipment> shipments = new List<Shipment>();

			foreach (DataRow row in Dt.Rows)
			{
				Shipment shipment = new Shipment
				{
					ShipmentID = Convert.ToInt32(row["ShipmentID"]),
					CustomerID = Convert.ToInt32(row["CustomerID"]),
					SourceAddress = row["SourceAddress"].ToString(),
					SourceCity = row["SourceCity"].ToString(),
					DestinationAddress = row["DestinationAddress"].ToString(),
					DestinationCity = Convert.ToInt32(row["DestinationCity"]),
					CustomerPhone = row["CustomerPhone"].ToString(),
					PickupDate = row["PickupDate"] != DBNull.Value ? Convert.ToDateTime(row["PickupDate"]) : new DateTime(1, 1, 1),
					DeliveryDate = row["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(row["DeliveryDate"]) : new DateTime(1, 1, 1),
					ShipmentDate = row["ShipmentDate"] != DBNull.Value ? Convert.ToDateTime(row["ShipmentDate"]) : new DateTime(1, 1, 1),
					NumberOfPackages = Convert.ToInt32(row["NumberOfPackages"]),
					DriverID = Convert.ToInt32(row["DriverID"]),
					ShippingStatus = row["ShippingStatus"].ToString(),
					OrderDate = row["OrderDate"] != DBNull.Value ? Convert.ToDateTime(row["OrderDate"]) : new DateTime(1, 1, 1),
					OrderNumber = Convert.ToInt32(row["OrderNumber"]) , // Added OrderNumber
					Payment = row["Payment"] != DBNull.Value ? Convert.ToInt32(row["Payment"]) : 0,
				};
				shipments.Add(shipment);
			}

			Db.Close();
			return shipments;
		}

		public static void AssignShipmentToCustomer(int shipmentId, int customerId)
		{
			DbContext Db = new DbContext();
			string Sql = $@"
                UPDATE Shipments
                SET CustomerID = {customerId}
                WHERE ShipmentID = {shipmentId}";

			Db.Execute(Sql);
			Db.Close();
		}

		public static void RemoveShipmentFromCustomer(int shipmentId, int customerId)
		{
			DbContext Db = new DbContext();
			string Sql = $@"
                UPDATE Shipments
                SET CustomerID = NULL
                WHERE ShipmentID = {shipmentId} AND CustomerID = {customerId}";

			Db.Execute(Sql);
			Db.Close();
		}
		public static List<Shipment> GetShipmentsByCustomerId(int customerId)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM Shipments WHERE CustomerID = {customerId}";
			DataTable Dt = Db.Execute(Sql);
			List<Shipment> shipments = new List<Shipment>();

			foreach (DataRow row in Dt.Rows)
			{
				Shipment shipment = new Shipment
				{
					ShipmentID = Convert.ToInt32(row["ShipmentID"]),
					CustomerID = Convert.ToInt32(row["CustomerID"]),
					SourceAddress = row["SourceAddress"].ToString(),
					SourceCity = row["SourceCity"].ToString(),
					DestinationAddress = row["DestinationAddress"].ToString(),
					DestinationCity = row["DestinationCity"] != DBNull.Value ? Convert.ToInt32(row["DestinationCity"]) : 0,
					CustomerPhone = row["CustomerPhone"].ToString(),
					PickupDate = row["PickupDate"] != DBNull.Value ? Convert.ToDateTime(row["PickupDate"]) : DateTime.MinValue,
					DeliveryDate = row["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(row["DeliveryDate"]) : DateTime.MinValue,
					ShipmentDate = row["ShipmentDate"] != DBNull.Value ? Convert.ToDateTime(row["ShipmentDate"]) : DateTime.MinValue,
					NumberOfPackages = Convert.ToInt32(row["NumberOfPackages"]),
					DriverID = Convert.ToInt32(row["DriverID"]),
					ShippingStatus = row["ShippingStatus"].ToString(),
					OrderDate = row["OrderDate"] != DBNull.Value ? Convert.ToDateTime(row["OrderDate"]) : DateTime.MinValue,
					OrderNumber = Convert.ToInt32(row["OrderNumber"]), // Assuming OrderNumber exists in your schema
					Payment = row["Payment"] != DBNull.Value ? Convert.ToInt32(row["Payment"]) : 0,
				};
				shipments.Add(shipment);
			}

			Db.Close();
			return shipments;
		}









		public static void RestoreDriverAvailableSpace(int shipmentId, int driverId)
		{
			try
			{
				DbContext Db = new DbContext();

				// שליפת מספר החבילות במשלוח
				string sqlShipment = $"SELECT NumberOfPackages FROM Shipments WHERE ShipmentID = {shipmentId} AND DriverID = {driverId}";
				DataTable dtShipment = Db.Execute(sqlShipment);

				if (dtShipment.Rows.Count > 0)
				{
					int numberOfPackages = Convert.ToInt32(dtShipment.Rows[0]["NumberOfPackages"]);

					// שליפת פרטי הנהג
					string sqlDriver = $"SELECT MaxDeliveries, CurrentDeliveries FROM Drivers WHERE DriverID = {driverId}";
					DataTable dtDriver = Db.Execute(sqlDriver);

					if (dtDriver.Rows.Count > 0)
					{
						int currentDeliveries = Convert.ToInt32(dtDriver.Rows[0]["CurrentDeliveries"]);
						int updatedDeliveries = currentDeliveries + numberOfPackages; // הפחתת המקום המנוצל

						// עדכון המקום הפנוי של הנהג
						string sqlUpdate = $"UPDATE Drivers SET CurrentDeliveries = {updatedDeliveries} WHERE DriverID = {driverId}";
						Db.ExecuteNonQuery(sqlUpdate);
					}
				}

				Db.Close();
			}
			catch (Exception ex)
			{
				// טיפול או רישום השגיאה
				Console.WriteLine($"Error restoring driver available space: {ex.Message}");
			}
		}










		public static void UpdateDeliveryStatus(int shipmentId, string shippingStatus)
		{
			// יצירת אובייקט DbContext
			DbContext Db = new DbContext();

			try
			{
				// שימוש בפרמטרים לשמירה על הטקסט בצורה נכונה
				string Sql = @"
        UPDATE Shipments
        SET ShippingStatus = @ShippingStatus
        WHERE ShipmentID = @ShipmentID";

				// יצירת אובייקט SqlCommand והגדרת פרמטרים
				SqlCommand Cmd = new SqlCommand(Sql, Db.Conn);
				Cmd.Parameters.AddWithValue("@ShippingStatus", shippingStatus);
				Cmd.Parameters.AddWithValue("@ShipmentID", shipmentId);

				// ביצוע השאילתה
				Cmd.ExecuteNonQuery();

				// שליפת המשלוח מהנתונים הקודמים כדי למצוא את ה-DriverID
				string sqlGetDriverId = "SELECT DriverID, ShippingStatus FROM Shipments WHERE ShipmentID = @ShipmentID";
				SqlCommand getDriverCmd = new SqlCommand(sqlGetDriverId, Db.Conn);
				getDriverCmd.Parameters.AddWithValue("@ShipmentID", shipmentId);

				using (SqlDataReader reader = getDriverCmd.ExecuteReader())
				{
					if (reader.Read())
					{
						int driverId = reader.GetInt32(reader.GetOrdinal("DriverID"));
						string oldStatus = reader.GetString(reader.GetOrdinal("ShippingStatus"));

						// בדוק אם הסטטוס השתנה ל"נמסר" 
						if (oldStatus != "נמסר" && shippingStatus == "נמסר")
						{
							// קריאה לפונקציה להחזרת המקום הפנוי לנהג המסוים
							RestoreDriverAvailableSpace(shipmentId, driverId);
						}
					}
				}
			}
			finally
			{
				// סגירת החיבור
				Db.Close();
			}
		}

















		public static List<Shipment> GetShipmentsByCustomerIdAndMonth(int customerId, int month)
		{
			DbContext Db = new DbContext();
			string Sql = $@"
                SELECT * FROM Shipments
                WHERE CustomerID = {customerId}
                AND MONTH(DeliveryDate) = {month}";

			DataTable Dt = Db.Execute(Sql);
			List<Shipment> shipments = new List<Shipment>();

			foreach (DataRow row in Dt.Rows)
			{
				Shipment shipment = new Shipment
				{
					ShipmentID = Convert.ToInt32(row["ShipmentID"]),
					CustomerID = Convert.ToInt32(row["CustomerID"]),
					SourceAddress = row["SourceAddress"].ToString(),
					SourceCity = row["SourceCity"].ToString(),
					DestinationAddress = row["DestinationAddress"].ToString(),
					DestinationCity = Convert.ToInt32(row["DestinationCity"]),
					CustomerPhone = row["CustomerPhone"].ToString(),
					PickupDate = Convert.ToDateTime(row["PickupDate"]),
					DeliveryDate = Convert.ToDateTime(row["DeliveryDate"]),
					ShipmentDate = Convert.ToDateTime(row["ShipmentDate"]),
					NumberOfPackages = Convert.ToInt32(row["NumberOfPackages"]),
					DriverID = Convert.ToInt32(row["DriverID"]),
					ShippingStatus = row["ShippingStatus"].ToString(),
					OrderDate = row["OrderDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["OrderDate"]),
					OrderNumber = Convert.ToInt32(row["OrderNumber"]),  // Added OrderNumber
					Payment = row["Payment"] != DBNull.Value ? Convert.ToInt32(row["Payment"]) : 0,
				};
				shipments.Add(shipment);
			}

			Db.Close();
			return shipments;
		}

		public static void AssignShipmentsByCityToUser(string city, int userId)
		{
			DbContext Db = new DbContext();
			string Sql = $@"
                UPDATE Shipments
                SET DriverID = {userId}
                WHERE DestinationCity = '{city}'";

			Db.Execute(Sql);
			Db.Close();
		}








		public static bool UpdatePickupDate(int shipmentId, DateTime pickupDate)
		{
			try
			{
				DbContext Db = new DbContext();
				string Sql = $"UPDATE Shipments SET PickupDate = '{pickupDate:yyyy-MM-dd HH:mm:ss}' WHERE ShipmentID = {shipmentId}";
				Db.Execute(Sql);
				Db.Close();
				return true;
			}
			catch (Exception ex)
			{
				// Handle exception (logging, etc.)
				return false;
			}
		}
		public static bool UpdateDeliveryDate(int shipmentId, DateTime deliveryDate)
		{
			try
			{
				DbContext Db = new DbContext();
				string Sql = $"UPDATE Shipments SET DeliveryDate = '{deliveryDate:yyyy-MM-dd HH:mm:ss}' WHERE ShipmentID = {shipmentId}";
				Db.Execute(Sql);
				Db.Close();
				return true;
			}
			catch (Exception ex)
			{
				// Handle exception (logging, etc.)
				return false;
			}
		}


		public static void Delete(int shipmentId)
		{
			DbContext Db = new DbContext();
			string Sql = $"DELETE FROM Shipments WHERE ShipmentID = {shipmentId}";
			Db.Execute(Sql);
			Db.Close();
		}
	}
}
