<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Driver_management.AdminManager.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <!-- DataTables CSS -->
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
                        <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">ניהול משתמשים</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            מאגר משתמשים במערכת
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="MainTbl">
                                    <thead>
                                        <tr>
                                            <th>קוד נהג</th>
                                            <th>שם מלא</th>
                                            <th>כתובת</th>
                                            <th>מייל</th>
                                            <th>מקסימום חבילות</th>
                                            <th>פלאפון</th>
                                            <th>ניהול</th>
                                        </tr>
                                    </thead>
                                   <tbody>
                    <asp:Repeater ID="RptUser" runat="server">
                        <ItemTemplate>
                            <tr class="odd gradeX">
                                <td><%#Eval("DriverID")%></td>
                                <td><%#Eval("DriverName")%></td>
                                <td><%#Eval("Address")%></td>
                                <td><%#Eval("DriverMail")%></td>
                                <td><%#Eval("MaxDeliveries")%></td>
                                <td><%#Eval("DriverPhone")%></td>
                                <td class="center"><img src="/uploads/prods/<%#Eval("Picname")%>" width="30" /> </td>
                                <td class="center"><a href="UserAddEdit.aspx?UserID=<%#Eval("DriverID")%>" >ערוך <span class="fa fa-pencil" /></a></td>
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
        $(document).ready(function() {
            $('#MainTbl').dataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/2.0.7/i18n/he.json',
                }
            });
        });
    </script>
</asp:Content>
