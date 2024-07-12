<%@ Page Title="" Language="C#" MasterPageFile="~/DriverManagement/DriverMaster.Master" AutoEventWireup="true" CodeBehind="MyShipments.aspx.cs" Inherits="Driver_management.DriverManagement.MyShipments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables CSS -->
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">המשלוחים שלי</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    רשימת המשלוחים שלי
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="MyShipmentsTbl">
                            <thead>
                                <tr>
                                    <th>עיר יעד</th>
                                    <th>כתובת יעד</th>
                                    <th>טלפון לקוח</th>
                                    <th>תאריך הזמנה</th>
                                    <th>תאריך איסוף</th>
                                    <th>תאריך מסירה</th>
                                    <th>פעולות</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptMyShipments" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("DestinationCity") %></td>
                                            <td><%# Eval("DestinationAddress") %></td>
                                            <td><%# Eval("CustomerPhone") %></td>
                                            <td><%# Eval("OrderDate", "{0:yyyy-MM-dd HH:mm:ss}") %></td>
                                            <td>
                                                <%# Eval("PickupDate") != DBNull.Value ? Eval("PickupDate", "{0:yyyy-MM-dd HH:mm:ss}") : "לא אוסף עדיין" %>
                                                <asp:Button ID="BtnConfirmPickup" runat="server" Text="אשר איסוף" CommandArgument='<%# Eval("ShipmentID") %>' OnClick="BtnConfirmPickup_Click" CssClass="btn btn-primary" Visible='<%# Eval("PickupDate") == DBNull.Value || (DateTime)Eval("PickupDate") == DateTime.MinValue %>' />
                                            </td>
                                            <td>
                                                <%# Eval("DeliveryDate") != DBNull.Value ? Eval("DeliveryDate", "{0:yyyy-MM-dd HH:mm:ss}") : "לא נמסר עדיין" %>
                                                <asp:Button ID="BtnConfirmDelivery" runat="server" Text="אשר מסירה" CommandArgument='<%# Eval("ShipmentID") %>' OnClick="BtnConfirmDelivery_Click" CssClass="btn btn-success" Visible='<%# Eval("DeliveryDate") == DBNull.Value || (DateTime)Eval("DeliveryDate") == DateTime.MinValue %>' />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSendWhatsApp" runat="server" Text="שלח WhatsApp" CommandArgument='<%# Eval("CustomerPhone") %>' OnClick="btnSendWhatsApp_Click" CssClass="btn btn-info" />
                                                <asp:Button ID="btnNavigateWaze" runat="server" Text="נווט ב-Waze" CommandArgument='<%# Eval("DestinationAddress") + ";" + Eval("DestinationCity") %>' OnClick="btnNavigateWaze_Click" CssClass="btn btn-success" />
                                            </td>
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
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
    <!-- DataTables JavaScript -->
    <script src="js/jquery/jquery.dataTables.min.js"></script>
    <script src="js/bootstrap/dataTables.bootstrap.min.js"></script>
    <!-- Page-Level Demo Scripts - Tables - Use for reference -->
    <script>
        $(document).ready(function () {
            $('#MyShipmentsTbl').dataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/2.0.7/i18n/he.json',
                }
            });
        });
    </script>
</asp:Content>

