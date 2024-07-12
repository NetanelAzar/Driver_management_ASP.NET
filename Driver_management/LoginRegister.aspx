<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginRegister.aspx.cs" Inherits="Driver_management.LoginRegister" %>
<%@ Register Src="~/User-Controls/LoginCube.ascx" TagPrefix="UC" TagName="LoginCube" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Register</title>
    <style>
        .login-container {
            max-width: 400px; /* הגבלת רוחב מקסימלי של הקונטיינר */
            margin: 50px auto; /* מרכז את הקונטיינר עם מרווח עליון ותחתון */
            padding: 15px; /* ריפוד פנימי */
            border-radius: 10px; /* פינות מעוגלות */
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); /* צל קל */
            background-color: #ffffff; /* צבע רקע לבן */
        }
        .form-title {
            text-align: center; /* ממרכז את הטקסט */
            margin-bottom: 15px; /* מרווח תחתון */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <UC:LoginCube id="LoginCube" runat="server" />
        </div>
    </form>
</body>
</html>
