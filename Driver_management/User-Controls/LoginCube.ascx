<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginCube.ascx.cs" Inherits="Driver_management.User_Controls.LoginCube" %>

<div class="container">
    <div class="login-container">
        <h2 class="form-title">התחברות / הרשמה</h2>
        
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="LoginView" runat="server">
                <div class="mb-3">
                    <label for="TxtEmail" class="form-label">שם משתמש:</label>
                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtPassword" class="form-label">סיסמא:</label>
                    <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="d-grid gap-2">
                    <asp:Button ID="BtnLogin" runat="server" OnClick="BtnLogin_Click" CssClass="btn btn-primary" Text="התחברות" />
                </div>
                <div class="mt-3">
                    <asp:Literal ID="LtlMsg" runat="server" />
                </div>
                <div class="mt-3 text-center">
                    <asp:LinkButton ID="LinkToRegister" runat="server" OnClick="LinkToRegister_Click">הרשמה כלקוח חדש</asp:LinkButton>
                </div>
            </asp:View>
            <asp:View ID="RegisterView" runat="server">
                <div class="mb-3">
                    <label for="TxtRegEmail" class="form-label">אימייל:</label>
                    <asp:TextBox ID="TxtRegEmail" runat="server" TextMode="Email" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtRegPassword" class="form-label">סיסמא:</label>
                    <asp:TextBox ID="TxtRegPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label for="TxtRegFullName" class="form-label">שם מלא:</label>
                    <asp:TextBox ID="TxtRegFullName" runat="server" CssClass="form-control" />
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
                    <asp:LinkButton ID="LinkToLogin" runat="server" OnClick="LinkToLogin_Click">התחברות כבר רשום</asp:LinkButton>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</div>
<!-- Bootstrap CSS -->
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

<!-- Bootstrap Bundle with Popper -->
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>