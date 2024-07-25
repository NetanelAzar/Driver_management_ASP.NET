<%@ Page Title="פרטי משלוח" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="Driver_management.ClientManagement.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <style>
        body {
            background-color: #f8f9fa;
        }

        .order-details-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 40px;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
            direction: rtl; /* כיוון מימין לשמאל */
            text-align: right; /* יישור תוכן לימין */
        }

        .order-title {
            font-size: 36px; /* גודל פונט גדול לבלטות הכותרת */
            font-weight: 700; /* עובי פונט מודגש */
            color: #007bff; /* צבע כחול בהיר */
            text-align: center; /* יישור מרכזי */
            margin-bottom: 30px; /* רווח תחתון להפרדת הכותרת מהתוכן מתחת */
            position: relative; /* הכנה לאפקטים נוספים */
            padding-bottom: 10px; /* רווח פנימי בתחתית */
        }

        .order-title::before {
            content: ''; /* תוכן ריק ליצירת אפקט */
            position: absolute; /* מיקום מוחלט */
            left: 50%; /* מיקום במרכז */
            bottom: 0; /* בתחתית הכותרת */
            transform: translateX(-50%); /* מיקום מרכזי מדויק */
            width: 80px; /* רוחב קו קישוט */
            height: 4px; /* גובה קו קישוט */
            background-color: #007bff; /* צבע קו הקישוט */
            border-radius: 2px; /* עגלות פינות הקו */
        }

        .order-item {
            font-size: 20px;
            margin-bottom: 15px;
            display: flex;
            align-items: center;
            padding: 10px;
            border-bottom: 1px solid #e9ecef;
        }

        .order-item i {
            margin-left: 15px;
            color: #007bff; /* צבע האייקונים */
        }

        .order-label {
            font-weight: bold;
            color: #333;
            flex: 1;
        }

        .order-value {
            color: #555;
            flex: 2;
        }

    .btn-custom {
        background-color: #007bff; /* צבע רקע כחול בהיר */
        color: #ffffff; /* צבע טקסט לבן */
        border: 2px solid #007bff; /* צבע גבול תואם לצבע הרקע */
        border-radius: 25px; /* עיגול פינות הכפתור */
        padding: 12px 25px; /* ריפוד פנימי */
        font-size: 18px; /* גודל פונט */
        font-weight: bold; /* עובי פונט */
        text-decoration: none; /* הסרת קו תחתון */
        display: inline-block; /* הצגת כפתור בשורה */
        transition: background-color 0.3s ease, border-color 0.3s ease; /* מעבר חלק בצבעים */
    }

    .btn-custom:hover {
        background-color: #0056b3; /* צבע רקע כהה יותר בעת ריחוף */
        border-color: #0056b3; /* שינוי צבע הגבול בעת ריחוף */
    }

    .btn-custom:focus {
        outline: none; /* הסרת קו מתאר בעת מיקוד */
        box-shadow: 0 0 0 2px rgba(38, 143, 255, 0.5); /* הוספת אפקט של צל קל בעת מיקוד */
    }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="order-details-container">
        <div class="order-title">פרטי הזמנה</div>
        <div class="order-item"><i class="fas fa-receipt"></i> 
            <span class="order-label">מספר הזמנה:</span>
            <span class="order-value"><asp:Label ID="OrderNumber" runat="server" CssClass="order-value"></asp:Label></span>
        </div>
        <div class="order-item"><i class="fas fa-calendar-day"></i>
            <span class="order-label">תאריך הזמנה:</span>
            <span class="order-value"> <asp:Label ID="OrderDate" runat="server" CssClass="order-value"></asp:Label></span>
        </div>
        <div class="order-item"><i class="fas fa-truck"></i>
            <span class="order-label">סטטוס:</span>
            <span class="order-value"> <asp:Label ID="ShippingStatus" runat="server" CssClass="order-value"></asp:Label></span>
        </div>
        <div class="order-item"><i class="fas fa-map-marker-alt"></i>
            <span class="order-label">כתובת משלוח:</span>
            <span class="order-value"> <asp:Label ID="ShippingAddress" runat="server" CssClass="order-value"></asp:Label></span>
        </div>
        <div class="order-item"><i class="fas fa-city"></i>
            <span class="order-label">עיר משלוח:</span>
            <span class="order-value"> <asp:Label ID="DestinationCity" runat="server" CssClass="order-value"></asp:Label></span>
        </div>
        <div class="order-item"><i class="fas fa-box"></i>
            <span class="order-label">מספר החבילות:</span>
            <span class="order-value"> <asp:Label ID="NumberOfPackages" runat="server" CssClass="order-value"></asp:Label></span>
        </div>
        <div class="order-item"><i class="fas fa-dollar-sign"></i>
            <span class="order-label">עלות ההזמנה/משלוח: ₪ </span>
            <span class="order-value"> <asp:Label ID="payment" runat="server" CssClass="order-value"></asp:Label></span>
        </div>
<div class="text-center">
    <a href="ClientHome.aspx" class="btn btn-custom mt-4">דף הבית</a>
</div>

    </div>
</asp:Content>
