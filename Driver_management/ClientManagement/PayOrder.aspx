<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="PayOrder.aspx.cs" Inherits="Driver_management.ClientManagement.PayOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
    <script src="https://js.stripe.com/v3/"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <h2>תשלום עבור הזמנה</h2>
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <div class="card">
                    <div class="card-header">
                        פרטי ההזמנה
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label>מספר חבילות:</label>
                            <asp:Label ID="LblNumberOfPackages" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>סכום לתשלום:</label>
                            <asp:Label ID="LblTotalAmount" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>מספר כרטיס אשראי:</label>
                            <div id="card-element" class="form-control"></div>
                        </div>
                        <div class="form-group text-center">
                            <asp:Button ID="BtnPayNow" runat="server" Text="שלם עכשיו" CssClass="btn btn-success" OnClientClick="payNow(); return false;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    </asp:Content>
    <asp:Content ID="Footer" ContentPlaceHolderID="Footer" runat="server">

      <script src="/js/PayOrder.js"></script>

      

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="underFooter" runat="server">
</asp:Content>
