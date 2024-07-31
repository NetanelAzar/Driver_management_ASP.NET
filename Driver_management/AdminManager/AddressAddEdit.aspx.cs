using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace Driver_management.AdminManager
{
	public partial class AddressAddEdit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// Populate dropdowns with data
				DdlDestinationCity.DataSource = City.GetAll();
				DdlDestinationCity.DataTextField = "CityName";
				DdlDestinationCity.DataValueField = "CityId";
				DdlDestinationCity.DataBind();

				DdlDriverID.DataSource = Drivers.GetAll();
				DdlDriverID.DataTextField = "DriverName";
				DdlDriverID.DataValueField = "DriverID";
				DdlDriverID.DataBind();

				// Populate shipment status dropdown
				DdlShipmentStatus.Items.Clear();
				DdlShipmentStatus.Items.Add(new ListItem("-- בחר סטאטוס משלוח --"));
				DdlShipmentStatus.Items.Add(new ListItem("התקבל במערכת"));
				DdlShipmentStatus.Items.Add(new ListItem("נאסף על ידי השליח"));
				DdlShipmentStatus.Items.Add(new ListItem("בדרך ליעד"));
				DdlShipmentStatus.Items.Add(new ListItem("נמסר"));

				// Check if there's a shipment ID in query string
				string shipmentID = Request["ShipmentID"];
				if (!string.IsNullOrEmpty(shipmentID))
				{
					if (int.TryParse(shipmentID, out int parsedShipmentID))
					{
						// Load shipment details
						Shipment shipment = Shipment.GetById(parsedShipmentID);
						if (shipment != null)
						{
							HidShipmentID.Value = shipmentID;
							TxtSourceAddress.Text = shipment.SourceAddress;
							TxtSourceCity.Text = shipment.SourceCity;
							TxtDestinationAddress.Text = shipment.DestinationAddress;
							DdlDestinationCity.SelectedValue = shipment.DestinationCity.ToString();
							TxtCustomerPhone.Text = shipment.CustomerPhone;
							TxtPickupDate.Text = shipment.PickupDate.ToString("yyyy-MM-dd");
							TxtDeliveryDate.Text = shipment.DeliveryDate.ToString("yyyy-MM-dd");
							TxtShipmentDate.Text = shipment.ShipmentDate.ToString("yyyy-MM-dd");
							TxtOrderDate.Text = shipment.OrderDate.ToString("yyyy-MM-dd");
							TxtNumberOfPackages.Text = shipment.NumberOfPackages.ToString();
							DdlDriverID.SelectedValue = shipment.DriverID.ToString();
							TxtCustomerID.Text = shipment.CustomerID.ToString();
							DdlShipmentStatus.SelectedValue = shipment.ShippingStatus;
							TxtOrderNumber.Text = shipment.OrderNumber.ToString(); // Adding OrderNumber field
						}
					}
				}
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Shipment shipment = new Shipment();

			// Populate shipment object with form data
			shipment.ShipmentID = int.Parse(HidShipmentID.Value);
			shipment.SourceAddress = TxtSourceAddress.Text;
			shipment.SourceCity = TxtSourceCity.Text;
			shipment.DestinationAddress = TxtDestinationAddress.Text;

			// Parsing DestinationCity to int
			if (int.TryParse(DdlDestinationCity.SelectedValue, out int cityId))
			{
				shipment.DestinationCity = cityId;
			}

			shipment.CustomerPhone = TxtCustomerPhone.Text;
			shipment.PickupDate = DateTime.Parse(TxtPickupDate.Text);
			shipment.DeliveryDate = DateTime.Parse(TxtDeliveryDate.Text);
			shipment.ShipmentDate = DateTime.Parse(TxtShipmentDate.Text);
			shipment.OrderDate = DateTime.Parse(TxtOrderDate.Text);
			shipment.NumberOfPackages = int.Parse(TxtNumberOfPackages.Text);

			// Parsing DriverID to int
			if (int.TryParse(DdlDriverID.SelectedValue, out int driverId))
			{
				shipment.DriverID = driverId;
			}

			shipment.CustomerID = int.Parse(TxtCustomerID.Text);
			shipment.ShippingStatus = DdlShipmentStatus.SelectedValue;
			shipment.OrderNumber = int.Parse(TxtOrderNumber.Text); // Adding OrderNumber field

			// שמור את הסטטוס הקודם של המשלוח
			string previousStatus = shipment.ShippingStatus;

			// קבל את המשלוח הישן עבור סטטוס ונהג
			Shipment oldShipment = Shipment.GetById(shipment.ShipmentID);
			int oldDriverId = oldShipment?.DriverID ?? 0;
			string oldStatus = oldShipment?.ShippingStatus ?? "";

			// Save or update the shipment
			shipment.Save();

			// Update the available space for the driver
			if (oldDriverId != 0 && oldStatus != "נמסר" && shipment.ShippingStatus == "נמסר")
			{
				// Restore available space if the shipment is delivered
				ShipmentDAL.RestoreDriverAvailableSpace(shipment.ShipmentID, oldDriverId);
			}
			else if (oldDriverId == 0 || (oldStatus != "נמסר" && shipment.ShippingStatus != "נמסר"))
			{
				// Update available space if the shipment is no longer delivered
				Drivers.UpdateDriverAvailableSpace(shipment.DriverID);
			}

			// Update application-level variable
			Application["Shipments"] = Shipment.GetAll();

			// Redirect to shipment list page
			Response.Redirect("AddressList.aspx");
		}

	}
}