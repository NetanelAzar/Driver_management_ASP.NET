<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="CitiesList.aspx.cs" Inherits="Driver_management.AdminManager.CitiesList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTables CSS -->
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול ערים</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    רשימת ערים
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="MainTbl">
                            <thead>
                                <tr>
                                    <th>מזהה עיר</th>
                                    <th>קוד עיר</th>
                                    <th>שם עיר</th>
                                    <th>סטטוס</th>
                                    <th>תאריך הוספה</th>
                                    <th>ניהול</th> <!-- פעולות ניהול -->
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RptCities" runat="server" OnItemCommand="RptCities_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("CityId") %></td>
                                            <td><%# Eval("CityCode") %></td>
                                            <td><%# Eval("CityName") %></td>
                                            <td><%# Eval("Status") %></td>
                                            <td><%# FormatDate(Eval("AddDate")) %></td>
                                            <td class="center">
                                                <a href="CityAddEdit.aspx?CityId=<%# Eval("CityId") %>">ערוך <span class="fa fa-pencil" /></a> |
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("CityId") %>' Text="מחק" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק עיר זו?');"></asp:LinkButton>
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
