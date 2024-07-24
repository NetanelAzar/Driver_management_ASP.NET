<%@ Page Title="" Language="C#" MasterPageFile="~/DriverManagement/DriverMaster.Master" AutoEventWireup="true" CodeBehind="MyShipments.aspx.cs" Inherits="Driver_management.DriverManagement.MyShipments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap 5 CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/5.1.3/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .page-header {
            margin-bottom: 30px;
            font-size: 2.5rem;
            color: #004085;
            font-weight: bold;
        }

        .panel-heading {
            background-color: #f8f9fa;
            color: #333;
            padding: 15px;
            border-bottom: 2px solid #e0e0e0;
            text-align: center;
        }

        .table-responsive {
            margin-top: 20px;
        }

        .table th, .table td {
            text-align: center;
            vertical-align: middle;
            white-space: nowrap;
        }

        .table th {
            background-color: #e9ecef;
            color: #495057;
        }

        .btn {
            margin: 2px;
            border-radius: 20px;
            transition: all 0.3s ease;
        }

        .btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
        }

        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
        }

        .btn-success {
            background-color: #28a745;
            border-color: #28a745;
        }

        .btn-info {
            background-color: #17a2b8;
            border-color: #17a2b8;
        }

        /* Responsive adjustments */
        @media (max-width: 768px) {
            .table th, .table td {
                font-size: 0.875rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header text-center">המשלוחים שלי</h1>
               
                    רשימת המשלוחים שלי
                </div>
                <div class="table-responsive">
                    <table class="table table-striped table-hover" id="MyShipmentsTbl" style="width: 100%;">
                        <thead>
                            <tr>
                                <th style="width: 12%;">עיר יעד</th>
                                <th style="width: 18%;">כתובת יעד</th>
                                <th style="width: 15%;">טלפון לקוח</th>
                                <th style="width: 15%;">תאריך הזמנה</th>
                                <th style="width: 15%;">תאריך איסוף</th>
                                <th style="width: 15%;">תאריך מסירה</th>
                                <th style="width: 10%;">פעולות</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptMyShipments" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("DestinationCity") %></td>
                                        <td><%# Eval("DestinationAddress") %></td>
                                        <td><%# Eval("CustomerPhone") %></td>
                                        <td><%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></td>
                                        <td>
                                            <%# Eval("PickupDate") != DBNull.Value ? Eval("PickupDate", "{0:yyyy-MM-dd}") : "לא אוסף עדיין" %>
                                            <asp:Button ID="BtnConfirmPickup" runat="server" Text="אשר איסוף" CommandArgument='<%# Eval("ShipmentID") %>' OnClick="BtnConfirmPickup_Click" CssClass="btn btn-primary btn-sm" Visible='<%# Eval("PickupDate") == DBNull.Value || (DateTime)Eval("PickupDate") == DateTime.MinValue %>' />
                                        </td>
                                        <td>
                                            <%# Eval("DeliveryDate") != DBNull.Value ? Eval("DeliveryDate", "{0:yyyy-MM-dd}") : "לא נמסר עדיין" %>
                                            <asp:Button ID="BtnConfirmDelivery" runat="server" Text="אשר מסירה" CommandArgument='<%# Eval("ShipmentID") %>' OnClick="BtnConfirmDelivery_Click" CssClass="btn btn-success btn-sm" Visible='<%# Eval("DeliveryDate") == DBNull.Value || (DateTime)Eval("DeliveryDate") == DateTime.MinValue %>' />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSendWhatsApp" runat="server" Text="WhatsApp" CommandArgument='<%# Eval("CustomerPhone") %>' OnClick="btnSendWhatsApp_Click" CssClass="btn btn-info btn-sm" />
                                            <asp:Button ID="btnNavigateWaze" runat="server" Text="Waze" CommandArgument='<%# Eval("DestinationAddress") + ";" + Eval("DestinationCity") %>' OnClick="btnNavigateWaze_Click" CssClass="btn btn-success btn-sm" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
    <!-- Bootstrap 5 JavaScript and DataTables JavaScript -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/dataTables.bootstrap5.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#MyShipmentsTbl').DataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.11.5/i18n/Hebrew.json',
                },
                autoWidth: false
            });
        });
    </script>
</asp:Content>
