using DAL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
	public class Shipment
	{
		public int ShipmentID { get; set; }
		public int CustomerID { get; set; }
		public string SourceAddress { get; set; }
		public string SourceCity { get; set; }
		public string DestinationAddress { get; set; }
		public int DestinationCity { get; set; }
		public string CustomerPhone { get; set; }
		public DateTime PickupDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		public DateTime ShipmentDate { get; set; }
		public int NumberOfPackages { get; set; }
		public int DriverID { get; set; }
		public string ShippingStatus { get; set; } // New field
		public DateTime OrderDate { get; set; }    // New field
		public int OrderNumber { get; set; }  // New property for order number
		public int Payment {  get; set; }

		public static List<Shipment> GetAll()
		{
			return ShipmentDAL.GetAll();
		}

		public static Shipment GetById(int id)
		{
			return ShipmentDAL.GetById(id);
		}

		public void Save()
		{
			ShipmentDAL.Save(this);
		}

		public static List<Shipment> GetShipmentsByUserId(int userId)
		{
			return ShipmentDAL.GetShipmentsByUserId(userId);
		}

		public static void AssignShipmentToCustomer(int shipmentId, int customerId)
		{
			ShipmentDAL.AssignShipmentToCustomer(shipmentId, customerId);
		}

		public static void RemoveShipmentFromCustomer(int shipmentId, int customerId)
		{
			ShipmentDAL.RemoveShipmentFromCustomer(shipmentId, customerId);
		}

		public static List<Shipment> GetShipmentsByCustomerId(int customerId)
		{
			return ShipmentDAL.GetShipmentsByCustomerId(customerId);
		}

		public static void UpdateDeliveryStatus(int shipmentId, string isDelivered)
		{
			ShipmentDAL.UpdateDeliveryStatus(shipmentId, isDelivered);
		}

		public static List<Shipment> GetShipmentsByCustomerIdAndMonth(int customerId, int month)
		{
			return ShipmentDAL.GetShipmentsByCustomerIdAndMonth(customerId, month);
		}

		public static void AssignShipmentsByCityToUser(string city, int userId)
		{
			ShipmentDAL.AssignShipmentsByCityToUser(city, userId);
		}

		public static void Delete(int shipmentId)
		{
			ShipmentDAL.Delete(shipmentId);
		}

		public static void UpdatePickupDate(int shipmentId, DateTime pickupDate)
		{

			ShipmentDAL.UpdatePickupDate(shipmentId, pickupDate); 
		}
		public static void UpdateDeliveryDate(int shipmentId, DateTime deliveryDate)
		{
			ShipmentDAL.UpdateDeliveryDate(shipmentId, deliveryDate);
		}


		public static List<Shipment> GetByMonth(int month)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM Shipments WHERE MONTH(OrderDate) = {month}";
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
					DestinationCity = Convert.ToInt32(row["DestinationCity"]),
					CustomerPhone = row["CustomerPhone"].ToString(),
					PickupDate = row["PickupDate"] != DBNull.Value ? Convert.ToDateTime(row["PickupDate"]) : DateTime.MinValue,
					DeliveryDate = row["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(row["DeliveryDate"]) : DateTime.MinValue,
					ShipmentDate = row["ShipmentDate"] != DBNull.Value ? Convert.ToDateTime(row["ShipmentDate"]) : DateTime.MinValue,
					NumberOfPackages = Convert.ToInt32(row["NumberOfPackages"]),
					DriverID = row["DriverID"] != DBNull.Value ? Convert.ToInt32(row["DriverID"]) : 0,
					ShippingStatus = row["ShippingStatus"].ToString(),
					OrderDate = row["OrderDate"] != DBNull.Value ? Convert.ToDateTime(row["OrderDate"]) : DateTime.MinValue,
					OrderNumber = row["OrderNumber"] != DBNull.Value ? Convert.ToInt32(row["OrderNumber"]) : 0,
					Payment =row["Payment"] != DBNull.Value ? Convert.ToInt32(row["Payment"]) : 0,
				};
				shipments.Add(shipment);
			}

			Db.Close();
			return shipments;
		}








		public static List<Shipment> GetShipmentsByUserIdSortedByOrderDateDesc(int userId)
		{
			DbContext Db = new DbContext();
			string Sql = $"SELECT * FROM Shipments WHERE DriverID = {userId} ORDER BY OrderDate DESC";
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
					PickupDate = row["PickupDate"] != DBNull.Value ? Convert.ToDateTime(row["PickupDate"]) : DateTime.MinValue,
					DeliveryDate = row["DeliveryDate"] != DBNull.Value ? Convert.ToDateTime(row["DeliveryDate"]) : DateTime.MinValue,
					ShipmentDate = row["ShipmentDate"] != DBNull.Value ? Convert.ToDateTime(row["ShipmentDate"]) : DateTime.MinValue,
					NumberOfPackages = Convert.ToInt32(row["NumberOfPackages"]),
					DriverID = Convert.ToInt32(row["DriverID"]),
					ShippingStatus = row["ShippingStatus"].ToString(),
					OrderDate = row["OrderDate"] != DBNull.Value ? Convert.ToDateTime(row["OrderDate"]) : DateTime.MinValue,
					OrderNumber = Convert.ToInt32(row["OrderNumber"]),
					Payment = row["Payment"] != DBNull.Value ? Convert.ToInt32(row["Payment"]) : 0,
				};
				shipments.Add(shipment);
			}

			Db.Close();
			return shipments;
		}

	}
}
