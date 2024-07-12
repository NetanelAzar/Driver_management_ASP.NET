<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="ClientsList.aspx.cs" Inherits="Driver_management.AdminManager.ClientsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables CSS -->
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול לקוחות</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    רשימת לקוחות
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="MainTbl">
                            <thead>
                                <tr>
                                    <th>מזהה לקוח</th>
                                    <th>שם לקוח</th>
                                    <th>דוא"ל לקוח</th>
                                    <th>טלפון לקוח</th>
                                    <th>שם חברה</th>
                                    <th>מיקוד</th>
                                    <th>כתובת</th>
                                    <th>ניהול</th> <!-- פעולות עריכה ומחיקה -->
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RptClients" runat="server" OnItemCommand="RptClients_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("ClientID") %></td>
                                            <td><%# Eval("ClientName") %></td>
                                            <td><%# Eval("ClientMail") %></td>
                                            <td><%# Eval("ClientPhone") %></td>
                                            <td><%# Eval("CompanyName") %></td>
                                            <td><%# Eval("CityCode") %></td>
                                            <td><%# Eval("Address") %></td>
                                            <!-- Binding to new field -->
                                            <td class="center">
                                                <a href="ClientsAddEdit.aspx?ClientID=<%# Eval("ClientID") %>">ערוך <span class="fa fa-pencil"></span></a> |
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ClientID") %>' Text="מחק" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק לקוח זה?');"></asp:LinkButton>
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