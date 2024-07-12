<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="TrackingOrders.aspx.cs" Inherits="Driver_management.ClientManagement.TrackingOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <!-- DataTables CSS -->
    <link href="https://cdn.datatables.net/1.10.22/css/dataTables.bootstrap4.min.css" rel="stylesheet">
    <style>
        /* Custom styles for right-to-left layout */

        .page-header {
            margin-top: 20px;
            margin-bottom: 20px;
            text-align: right;
        }
        .panel-heading {
            font-size: 18px;
            font-weight: bold;
            text-align: right;
        }
        .table {
            text-align: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">מעקב אחר הזמנות</h1>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <h5 class="card-header">רשימת הזמנות</h5>
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="MainTbl">
                                <thead class="thead-dark">
                                    <tr>
                                        <th>מספר משלוח</th>
                                        <th>מספר הזמנה</th>
                                        <th>תאריך הזמנה</th>
                                        <th>כתובת יעד</th>
                                        <th>עיר יעד</th>
                                        <th>מספר חבילות</th>
                                        <th>תאריך איסוף</th>
                                        <th>תאריך משלוח</th>
                                        <th>תאריך מסירה</th>
                                        <th>סטטוס משלוח</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RptOrders" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("ShipmentID") %></td>
                                                <td><%# Eval("OrderNumber") %></td>
                                                <td><%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></td>
                                                <td><%# Eval("DestinationAddress") %></td>
                                                <td><%# GetCityNameById(Eval("DestinationCity")) %></td>
                                                <td><%# Eval("NumberOfPackages") %></td>
                                                <td><%# Eval("PickupDate", "{0:yyyy-MM-dd}") %></td>
                                                <td><%# Eval("ShipmentDate", "{0:yyyy-MM-dd}") %></td>
                                                <td><%# Eval("DeliveryDate", "{0:yyyy-MM-dd}") %></td>
                                                <td><%# Eval("ShippingStatus") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="underFooter" runat="server">
    <!-- jQuery, Bootstrap, DataTables JavaScript -->
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.22/js/dataTables.bootstrap4.min.js"></script>
    <!-- DataTables Hebrew language file -->
    <script src="//cdn.datatables.net/plug-ins/2.0.7/i18n/he.json"></script>
    <!-- Initialize DataTables -->
    <script>
        $(document).ready(function () {
            $('#MainTbl').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/2.0.7/i18n/he.json"
                }
            });
        });
    </script>
</asp:Content>
