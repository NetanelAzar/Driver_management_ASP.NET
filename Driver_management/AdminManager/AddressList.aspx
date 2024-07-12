<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AddressList.aspx.cs" Inherits="Driver_management.AdminManager.AddressList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables CSS -->
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול משלוחים</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    רשימת משלוחים
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                                        <div class="form-group">
                        <label for="ddlMonths">בחר חודש:</label>
                        <asp:DropDownList ID="ddlMonths" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            <asp:ListItem Text="ינואר" Value="1"></asp:ListItem>
                            <asp:ListItem Text="פברואר" Value="2"></asp:ListItem>
                            <asp:ListItem Text="מרץ" Value="3"></asp:ListItem>
                            <asp:ListItem Text="אפריל" Value="4"></asp:ListItem>
                            <asp:ListItem Text="מאי" Value="5"></asp:ListItem>
                            <asp:ListItem Text="יוני" Value="6"></asp:ListItem>
                            <asp:ListItem Text="יולי" Value="7"></asp:ListItem>
                            <asp:ListItem Text="אוגוסט" Value="8"></asp:ListItem>
                            <asp:ListItem Text="ספטמבר" Value="9"></asp:ListItem>
                            <asp:ListItem Text="אוקטובר" Value="10"></asp:ListItem>
                            <asp:ListItem Text="נובמבר" Value="11"></asp:ListItem>
                            <asp:ListItem Text="דצמבר" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </div>




                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="MainTbl">
                            <thead>
                                <tr>
                                    <th>מספר משלוח</th>
                                    <th>מספר הזמנה</th> <!-- הוספת השדה OrderNumber -->
                                    <th>לקוח</th>
                                    <th>פלאפון לקוח</th>
                                    <th>תאריך הזמנה</th>
                                    <th>כתובת יעד</th>
                                    <th>עיר יעד</th>
                                    <th>מספר חבילות</th>
                                    <th>כתובת מקור</th>
                                    <th>עיר מקור</th>
                                    <th>נהג</th>
                                    <th>תאריך איסוף</th>   
                                    <th>תאריך משלוח</th>
                                    <th>תאריך מסירה</th>                                    
                                    <th>סטטוס משלוח</th> <!-- שדה חדש -->
                                    <th>ניהול</th> <!-- פעולות חדשות -->
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RptAddresses" runat="server" OnItemCommand="RptAddresses_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("ShipmentID") %></td>
                                            <td><%# Eval("OrderNumber") %></td> <!-- תיקון: הוספת השדה OrderNumber -->
                                            <td><%# Eval("CustomerID") %></td>
                                            <td><%# Eval("CustomerPhone") %></td>
                                            <td><%# FormatDate(Eval("OrderDate")) %></td>
                                            <td><%# Eval("DestinationAddress") %></td>
                                            <td><%# GetCityNameById(Eval("DestinationCity")) %></td>
                                            <td><%# Eval("NumberOfPackages") %></td>
                                            <td><%# Eval("SourceAddress") %></td>
                                            <td><%# GetCityNameById(Eval("SourceCity")) %></td>
                                            <td><%# Eval("DriverID") %></td>
                                            <td><%# FormatDate(Eval("PickupDate")) %></td>
                                            <td><%# FormatDate(Eval("ShipmentDate")) %></td>
                                            <td><%# FormatDate(Eval("DeliveryDate")) %></td>
                                            <td><%# Eval("ShippingStatus") %></td>
                                            <!-- Binding to new field -->
                                            <td class="center">
                                                <a href="AddressAddEdit.aspx?ShipmentID=<%# Eval("ShipmentID") %>">ערוך <span class="fa fa-pencil" /></a> |
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ShipmentID") %>' Text="מחק" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק משלוח זה?');"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="underFooter" runat="server">
    <!-- DataTables JavaScript -->
    <script src="js/jquery/jquery.dataTables.min.js"></script>
    <script src="js/bootstrap/dataTables.bootstrap.min.js"></script>
    <!-- Page-Level Demo Scripts - Tables - Use for reference -->
    <script>
        $(document).ready(function () {
            $('#MainTbl').dataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/2.0.7/i18n/he.json',
                }
            });
        });
    </script>
</asp:Content>
