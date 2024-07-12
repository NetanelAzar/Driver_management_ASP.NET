<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="ClientOrder.aspx.cs" Inherits="Driver_management.ClientManagement.ClientOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-lg-6 offset-lg-3">
                <h2>פרטי הזמנה</h2>

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
                <div class="form-group">
                    <asp:Button ID="BtnSubmit" runat="server" Text="שלח" OnClick="BtnSubmit_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
