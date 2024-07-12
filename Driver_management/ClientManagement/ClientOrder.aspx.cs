using BLL;
using System;
using System.Linq;
using System.Web.UI;

namespace Driver_management.ClientManagement
{
    public partial class ClientOrder : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				// Populate dropdown with cities from database
				var cities = BLL.City.GetAll().OrderBy(c => c.CityName).ToList();
				DdlCityDestination.DataSource = cities;
				DdlCityDestination.DataTextField = "CityName";
				DdlCityDestination.DataValueField = "CityID";
				DdlCityDestination.DataBind();


				// Set current date on the Order Date field
				TxtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // פענוח מספר החבילות מתוך הטקסט בתיבת הטקסט
                int numberOfPackages;
                if (!int.TryParse(TxtNumberOfPackages.Text, out numberOfPackages) || numberOfPackages <= 0)
                {
                    // טיפול בשגיאת פענוח או ערך לא חוקי
                    Response.Write("שגיאה: מספר החבילות חייב להיות מספר שלם חיובי חוקי.");
                    return;
                }

                // אימות תאריך הזמנה
                DateTime orderDate;
                if (!DateTime.TryParse(TxtOrderDate.Text, out orderDate))
                {
                    // טיפול בשגיאת פענוח או תאריך לא חוקי
                    Response.Write("שגיאה: תאריך הזמנה אינו חוקי.");
                    return;
                }

                // איחזור פרטי הלקוח המחובר מהסשן או מקום אחר שמאוחסן במערכת
                Client loggedInClient = Session["Login"] as Client; // הנחה: Client הוא הסוג

                if (loggedInClient == null)
                {
                    // טיפול במקרה בו לא נמצא הלקוח המחובר
                    Response.Write("שגיאה: מידע על הלקוח המחובר לא נמצא.");
                    return;
                }

                // יצירה ושמירת המשלוח
                Shipment shipment = new Shipment()
                {
                    DestinationAddress = TxtDestinationAddress.Text,
                    NumberOfPackages = numberOfPackages,
                    OrderDate = orderDate,
                    DestinationCity = int.Parse(DdlCityDestination.SelectedValue),
                    CustomerID = loggedInClient.ClientID, // השימוש במזהה הלקוח של הלקוח המחובר
                    CustomerPhone = loggedInClient.ClientPhone, // שמירת מספר הטלפון של הלקוח
					ShippingStatus = "התקבל במערכת"
				};

                shipment.Save(); // הנחה: ישנה שיטה כמו Save() במחלקת Shipment לשמירת פרטי המשלוח במסד הנתונים

                // הצגת הודעת אישור או הפניה לדף אחר
                Response.Write("הזמנה בוצעה בהצלחה.");

            }
            catch (Exception ex)
            {
                // טיפול בחריגה
                Response.Write($"שגיאה: {ex.Message}");
            }
        }
    }
}
