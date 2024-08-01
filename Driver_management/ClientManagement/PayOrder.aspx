<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="PayOrder.aspx.cs" Inherits="Driver_management.ClientManagement.PayOrder"  Async="true" %>

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

    <script>
        var stripe = Stripe('pk_test_51PdnnDCGGvtn75QC5OTYc7ykFTdwq9feK1PSFQpe4Gl16Pxi0RHtHO3AP7KZyKEWMVS7lQ7hWJDMqX2CjZTtICKh005aryEm8x'); // Public key
        var elements = stripe.elements();
        var cardElement = elements.create('card');
        cardElement.mount('#card-element');

        function payNow() {
            stripe.createToken(cardElement).then(function (result) {
                if (result.error) {
                    alert(result.error.message);
                } else {
                    var token = result.token.id;
                    var numberOfPackages = '<%= Request.QueryString["NumberOfPackages"] %>';
                    var totalAmount = '<%= Request.QueryString["TotalAmount"] %>';
                    var destinationAddress = '<%= Request.QueryString["DestinationAddress"] %>';
                    var orderDate = '<%= Request.QueryString["OrderDate"] %>';
                    var destinationCity = '<%= Request.QueryString["DestinationCity"] %>';

                    var form = document.createElement('form');
                    form.method = 'post';
                    form.action = 'PayOrder.aspx';

                    var hiddenFields = [
                        { name: 'stripeToken', value: token },
                        { name: 'numberOfPackages', value: numberOfPackages },
                        { name: 'totalAmount', value: totalAmount },
                        { name: 'destinationAddress', value: destinationAddress },
                        { name: 'orderDate', value: orderDate },
                        { name: 'destinationCity', value: destinationCity }
                    ];

                    hiddenFields.forEach(function (field) {
                        var input = document.createElement('input');
                        input.type = 'hidden';
                        input.name = field.name;
                        input.value = field.value;
                        form.appendChild(input);
                    });

                    document.body.appendChild(form);
                    form.submit();
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="underFooter" runat="server">
</asp:Content>
