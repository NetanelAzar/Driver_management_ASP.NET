using System; // ייבוא הספרייה הבסיסית של .NET
using System.Web.UI; // ייבוא של רכיבי UI של ASP.NET
using System.Web.UI.WebControls; // ייבוא של רכיבי Web Controls כמו DropDownList
using BLL; // ייבוא של השכבת העסקית (Business Logic Layer)
using DAL; // ייבוא של השכבת הנתונים (Data Access Layer)

namespace Driver_management.AdminManager
{
	public partial class AddressAddEdit : System.Web.UI.Page // הגדרת דף ASP.NET
	{
		protected void Page_Load(object sender, EventArgs e) // אירוע טוען דף
		{
			if (!IsPostBack) // בדיקה אם הדף לא נטען מחדש (כפתור שליחה וכו')
			{
				// הוספת ערכים ל-DropDownList של ערים
				DdlDestinationCity.DataSource = City.GetAll(); // קבלת כל הערים מה-BLL
				DdlDestinationCity.DataTextField = "CityName"; // הגדרת שם השדה שיוצג למשתמש
				DdlDestinationCity.DataValueField = "CityId"; // הגדרת שם השדה שישמש כערך פנימי
				DdlDestinationCity.DataBind(); // הצגת הנתונים ב-DropDownList

				// הוספת ערכים ל-DropDownList של נהגים
				DdlDriverID.DataSource = Drivers.GetAll(); // קבלת כל הנהגים מה-BLL
				DdlDriverID.DataTextField = "DriverName"; // הגדרת שם השדה שיוצג למשתמש
				DdlDriverID.DataValueField = "DriverID"; // הגדרת שם השדה שישמש כערך פנימי
				DdlDriverID.DataBind(); // הצגת הנתונים ב-DropDownList

				// הוספת ערכים ל-DropDownList של סטטוס המשלוח
				DdlShipmentStatus.Items.Clear(); // ניקוי פריטים קיימים
				DdlShipmentStatus.Items.Add(new ListItem("-- בחר סטאטוס משלוח --")); // פריט ראשוני
				DdlShipmentStatus.Items.Add(new ListItem("התקבל במערכת")); // סטטוסים שונים
				DdlShipmentStatus.Items.Add(new ListItem("נאסף על ידי השליח"));
				DdlShipmentStatus.Items.Add(new ListItem("בדרך ליעד"));
				DdlShipmentStatus.Items.Add(new ListItem("נמסר"));

				// בדיקת קיום מזהה משלוח ב-Query String
				string shipmentID = Request["ShipmentID"]; // קבלת מזהה משלוח מה-Query String
				if (!string.IsNullOrEmpty(shipmentID)) // בדיקת אם המזהה אינו ריק
				{
					if (int.TryParse(shipmentID, out int parsedShipmentID)) // המרת המזהה למספר שלם
					{
						// טעינת פרטי המשלוח
						Shipment shipment = Shipment.GetById(parsedShipmentID); // קבלת פרטי המשלוח לפי מזהה
						if (shipment != null) // בדיקת אם המשלוח נמצא
						{
							HidShipmentID.Value = shipmentID; // הגדרת מזהה המשלוח בשדה החבוי
							TxtSourceAddress.Text = shipment.SourceAddress; // הגדרת כתובת מקור
							TxtSourceCity.Text = shipment.SourceCity; // הגדרת עיר מקור
							TxtDestinationAddress.Text = shipment.DestinationAddress; // הגדרת כתובת יעד
							DdlDestinationCity.SelectedValue = shipment.DestinationCity.ToString(); // הגדרת עיר יעד שנבחרה
							TxtCustomerPhone.Text = shipment.CustomerPhone; // הגדרת טלפון הלקוח
							TxtPickupDate.Text = shipment.PickupDate.ToString("yyyy-MM-dd"); // הגדרת תאריך איסוף
							TxtDeliveryDate.Text = shipment.DeliveryDate.ToString("yyyy-MM-dd"); // הגדרת תאריך משלוח
							TxtShipmentDate.Text = shipment.ShipmentDate.ToString("yyyy-MM-dd"); // הגדרת תאריך המשלוח
							TxtOrderDate.Text = shipment.OrderDate.ToString("yyyy-MM-dd"); // הגדרת תאריך ההזמנה
							TxtNumberOfPackages.Text = shipment.NumberOfPackages.ToString(); // הגדרת מספר החבילות
							DdlDriverID.SelectedValue = shipment.DriverID.ToString(); // הגדרת נהג שנבחר
							TxtCustomerID.Text = shipment.CustomerID.ToString(); // הגדרת מזהה הלקוח
							DdlShipmentStatus.SelectedValue = shipment.ShippingStatus; // הגדרת סטטוס המשלוח
							TxtOrderNumber.Text = shipment.OrderNumber.ToString(); // הגדרת מספר ההזמנה
						}
					}
				}
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e) // אירוע לחיצה על כפתור שמירה
		{
			Shipment shipment = new Shipment(); // יצירת אובייקט משלוח חדש

			// הזנת נתוני הטופס לאובייקט המשלוח
			shipment.ShipmentID = int.Parse(HidShipmentID.Value); // הגדרת מזהה המשלוח
			shipment.SourceAddress = TxtSourceAddress.Text; // הגדרת כתובת מקור
			shipment.SourceCity = TxtSourceCity.Text; // הגדרת עיר מקור
			shipment.DestinationAddress = TxtDestinationAddress.Text; // הגדרת כתובת יעד

			// המרת עיר היעד למספר שלם
			if (int.TryParse(DdlDestinationCity.SelectedValue, out int cityId))
			{
				shipment.DestinationCity = cityId; // הגדרת עיר היעד
			}

			shipment.CustomerPhone = TxtCustomerPhone.Text; // הגדרת טלפון הלקוח
			shipment.PickupDate = DateTime.Parse(TxtPickupDate.Text); // הגדרת תאריך איסוף
			shipment.DeliveryDate = DateTime.Parse(TxtDeliveryDate.Text); // הגדרת תאריך משלוח
			shipment.ShipmentDate = DateTime.Parse(TxtShipmentDate.Text); // הגדרת תאריך המשלוח
			shipment.OrderDate = DateTime.Parse(TxtOrderDate.Text); // הגדרת תאריך ההזמנה
			shipment.NumberOfPackages = int.Parse(TxtNumberOfPackages.Text); // הגדרת מספר החבילות

			// המרת מזהה הנהג למספר שלם
			if (int.TryParse(DdlDriverID.SelectedValue, out int driverId))
			{
				shipment.DriverID = driverId; // הגדרת מזהה הנהג
			}

			shipment.CustomerID = int.Parse(TxtCustomerID.Text); // הגדרת מזהה הלקוח
			shipment.ShippingStatus = DdlShipmentStatus.SelectedValue; // הגדרת סטטוס המשלוח
			shipment.OrderNumber = int.Parse(TxtOrderNumber.Text); // הגדרת מספר ההזמנה

			// שמירה של הסטטוס הקודם של המשלוח
			string previousStatus = shipment.ShippingStatus;

			// קבלת פרטי המשלוח הקודם עבור סטטוס ונהג
			Shipment oldShipment = Shipment.GetById(shipment.ShipmentID); // קבלת המשלוח הישן לפי מזהה
			int oldDriverId = oldShipment?.DriverID ?? 0; // קבלת מזהה הנהג הישן (אם קיים)
			string oldStatus = oldShipment?.ShippingStatus ?? ""; // קבלת הסטטוס הישן (אם קיים)

			// שמירה או עדכון של המשלוח
			shipment.Save(); // שמירת המשלוח

			// עדכון המקום הזמין לנהג
			if (oldDriverId != 0 && oldStatus != "נמסר" && shipment.ShippingStatus == "נמסר")
			{
				// החזרת מקום פנוי אם המשלוח נמסר
				ShipmentDAL.RestoreDriverAvailableSpace(shipment.ShipmentID, oldDriverId);
			}
			else if (oldDriverId == 0 || (oldStatus != "נמסר" && shipment.ShippingStatus != "נמסר"))
			{
				// עדכון מקום פנוי אם המשלוח כבר לא נמסר
				Drivers.UpdateDriverAvailableSpace(shipment.DriverID);
			}

			// עדכון משתנה ברמת האפליקציה
			Application["Shipments"] = Shipment.GetAll(); // ריענון רשימת המשלוחים

			// הפניית המשתמש לדף רשימת המשלוחים
			Response.Redirect("AddressList.aspx"); // הפניית המשתמש לדף אחר
		}
	}
}
