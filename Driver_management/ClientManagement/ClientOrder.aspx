<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="ClientOrder.aspx.cs" Inherits="Driver_management.ClientManagement.ClientOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
    <style>
        .card-modern {
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .card-header-modern {
            background: linear-gradient(to right, #0062E6, #33AEFF);
            border-radius: 15px 15px 0 0;
        }
        .btn-modern {
            background: linear-gradient(to right, #33AEFF, #0062E6);
            border: none;
            transition: background 0.3s ease;
        }
        .btn-modern:hover {
            background: linear-gradient(to right, #0062E6, #33AEFF);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-lg-6 offset-lg-3">
                <div class="card card-modern">
                    <div class="card-header card-header-modern text-white text-center">
                        <h2 class="card-title mb-0">פרטי הזמנה</h2>
                    </div>
                    <div class="card-body p-4">
                        <div class="form-group">
                            <asp:Label ID="LblDestinationAddress" runat="server" AssociatedControlID="TxtDestinationAddress" Text="כתובת יעד:" CssClass="font-weight-bold"></asp:Label>
                            <asp:TextBox ID="TxtDestinationAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblNumberOfPackages" runat="server" AssociatedControlID="TxtNumberOfPackages" Text="מספר חבילות:" CssClass="font-weight-bold"></asp:Label>
                            <asp:TextBox ID="TxtNumberOfPackages" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblOrderDate" runat="server" AssociatedControlID="TxtOrderDate" Text="תאריך הזמנה:" CssClass="font-weight-bold"></asp:Label>
                            <asp:TextBox ID="TxtOrderDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblCityDestination" runat="server" AssociatedControlID="DdlCityDestination" Text="עיר יעד:" CssClass="font-weight-bold"></asp:Label>
                            <asp:DropDownList ID="DdlCityDestination" runat="server" CssClass="form-control">
                                <asp:ListItem Text="בחר עיר..." Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group text-center">
                            <asp:Button ID="BtnSubmit" runat="server" Text="שלח" OnClick="BtnSubmit_Click" CssClass="btn btn-modern btn-lg text-white" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<%--  --%>