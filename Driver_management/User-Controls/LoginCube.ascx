<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginCube.ascx.cs" Inherits="Driver_management.User_Controls.LoginCube" %>

<div class="container">
    <div class="header">
        <div class="logo">
            <img src="uploads/pics/Azar-removebg-preview.png" alt="Logo" class="logo-img">
        </div>
        <div class="login-container">
            <h2 class="form-title">התחברות / הרשמה</h2>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="LoginView" runat="server">
                    <div class="mb-3">
                        <label for="TxtEmail" class="form-label">שם משתמש:</label>
                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" placeholder="הכנס שם משתמש" />
                    </div>
                    <div class="mb-3">
                        <label for="TxtPassword" class="form-label">סיסמא:</label>
                        <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="הכנס סיסמא" />
                    </div>
                    <div class="d-grid gap-2">
                        <asp:Button ID="BtnLogin" runat="server" OnClick="BtnLogin_Click" CssClass="btn btn-primary" Text="התחברות" />
                    </div>
                    <div class="mt-3">
                        <asp:Literal ID="LtlMsg" runat="server" />
                    </div>
                    <div class="mt-3 text-center">
                        <asp:LinkButton ID="LinkToRegister" runat="server" OnClick="LinkToRegister_Click" CssClass="link-button">הרשמה כלקוח חדש</asp:LinkButton>
                    </div>
                </asp:View>
                <asp:View ID="RegisterView" runat="server">
                    <div class="mb-3">
                        <label for="TxtRegEmail" class="form-label">אימייל:</label>
                        <asp:TextBox ID="TxtRegEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="הכנס אימייל" />
                    </div>
                    <div class="mb-3">
                        <label for="TxtRegPassword" class="form-label">סיסמא:</label>
                        <asp:TextBox ID="TxtRegPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="הכנס סיסמא" />
                    </div>
                    <div class="mb-3">
                        <label for="TxtRegFullName" class="form-label">שם מלא:</label>
                        <asp:TextBox ID="TxtRegFullName" runat="server" CssClass="form-control" placeholder="הכנס שם מלא" />
                    </div>
                    <div class="form-group position-relative">
                        <label for="TxtRegPhone">פלאפון:</label>
                        <i class="fa fa-phone form-icon"></i>
                        <asp:TextBox ID="TxtRegPhone" runat="server" CssClass="form-control" placeholder="הכנס פלאפון" />
                    </div>
                    <div class="form-group position-relative">
                        <label for="TxtRegAdd">כתובת:</label>
                        <i class="fa fa-home form-icon"></i>
                        <asp:TextBox ID="TxtRegAdd" runat="server" CssClass="form-control" placeholder="הכנס כתובת" />
                    </div>
                    <div class="form-group">
                        <label for="DDLRegCity">עיר:</label>
                        <asp:DropDownList ID="DDLRegCity" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group position-relative">
                        <label for="TxtRegCompanyName">שם החברה:</label>
                        <i class="fa fa-building form-icon"></i>
                        <asp:TextBox ID="TxtRegCompanyName" runat="server" CssClass="form-control" placeholder="הכנס שם החברה" />
                    </div>
                    <div class="form-group position-relative">
                        <label for="TxtRegPass">סיסמא:</label>
                        <i class="fa fa-lock form-icon"></i>
                        <asp:TextBox ID="TxtRegPass" runat="server" TextMode="Password" CssClass="form-control" placeholder="הכנס סיסמא" />
                    </div>
                    <div class="form-group position-relative">
                        <label for="TxtpassValidation">אימות סיסמא:</label>
                        <i class="fa fa-lock form-icon"></i>
                        <asp:TextBox ID="TxtpassValidation" runat="server" TextMode="Password" CssClass="form-control" placeholder="אמת סיסמא" />
                        <asp:Literal ID="LtlPassMsg" runat="server" />
                    </div>
                    <div class="d-grid gap-2">
                        <asp:Button ID="BtnRegister" runat="server" OnClick="BtnRegister_Click" CssClass="btn btn-primary btn-block" Text="הרשמה" />
                    </div>
                    <div class="mt-3">
                        <asp:Literal ID="LtlRegMsg" runat="server" />
                    </div>
                    <div class="mt-3 text-center">
                        <asp:LinkButton ID="LinkToLogin" runat="server" OnClick="LinkToLogin_Click" CssClass="link-button">התחברות כבר רשום</asp:LinkButton>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</div>

<!-- Bootstrap CSS -->
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

<!-- Font Awesome for Icons -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />

<!-- Bootstrap Bundle with Popper -->
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
<!-- Google Fonts -->
<link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet">

<style>
    body {
        background-image: url('https://www.example.com/path-to-your-background-image.jpg'); /* החלף לכתובת של התמונה הרצויה */
        background-size: cover;
        background-position: center;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }
    
    .header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        max-width: 1200px;
        margin: 80px auto;
        padding: 30px;
        background-color: rgba(255, 255, 255, 0.85);
        border-radius: 12px;
        box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
    }

    .logo {
        flex: 1;
        text-align: left;
    }

    .logo-img {
        width: 500px;
        height:300px;
        
    }

    .login-container {
        flex: 1;
        max-width: 450px;
        padding: 30px;
        border-radius: 12px;
        background-color: rgba(255, 255, 255, 0.85);
        backdrop-filter: blur(5px);
    }

.form-title {
    text-align: center;
    margin-bottom: 20px;
    font-size: 24px;
    color: #333;
    font-family: 'Roboto', sans-serif; /* שימוש בגופן Roboto */
}


    .link-button {
        color: #007bff;
        font-weight: bold;
    }

    .link-button:hover {
        color: #0056b3;
        text-decoration: none;
    }

    .form-control {
        border-radius: 25px;
        transition: all 0.3s;
    }

    .form-control:focus {
        border-color: #007bff;
        box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
    }

    .btn-primary {
        background-color: #007bff;
        border-color: #007bff;
        border-radius: 25px;
        transition: background-color 0.3s, border-color 0.3s;
    }

    .btn-primary:hover {
        background-color: #0056b3;
        border-color: #0056b3;
    }

    .form-icon {
        position: absolute;
        left: 10px;
        top: 50%;
        transform: translateY(-50%);
        color: #aaa;
    }

    .form-group.position-relative {
        position: relative;
    }
</style>
