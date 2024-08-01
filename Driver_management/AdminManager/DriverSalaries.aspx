<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="DriverSalaries.aspx.cs" Inherits="Driver_management.AdminManager.DriverSalaries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
            <style>
        .custom-dropdown {
            width: 100%;
            max-width: 300px;
            margin: 20px 0;
        }

        .custom-dropdown .form-control {
            border-radius: 0.25rem;
            padding: 0.5rem 1rem;
            font-size: 1.3rem;
        }
   
    </style>
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול משכורות נהגים</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    רשימת משכורות
                </div>
                <div class="panel-body">
                 <div class="custom-dropdown">
                        <asp:DropDownList ID="ddlMonthYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="table-responsive">
                        <div id="message" class="alert alert-info text-center" style="display: none;">
                            אין נתונים להציג לתאריכים שנבחרו.
                        </div>
                        <table class="table table-striped table-bordered table-hover" id="SalariesTbl">
                            <thead>
                                <tr>
                                    <th>שם הנהג</th>
                                    <th>חודש</th>
                                    <th>מספר משלוחים</th>
                                    <th>סכום כולל</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RepeaterSalaries" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("DriverName") %></td>
                                            <td><%# Eval("Month") %></td>
                                            <td><%# Eval("NumberOfDeliveries") %></td>
                                            <td><%# Eval("TotalAmount", "{0:C}") %></td>
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

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="underFooter" runat="server">
    <script src="js/jquery/jquery.dataTables.min.js"></script>
    <script src="js/bootstrap/dataTables.bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#SalariesTbl').dataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/2.0.7/i18n/he.json',
                },
                "paging": false,
                "info": false
            });
        });
    </script>
</asp:Content>
