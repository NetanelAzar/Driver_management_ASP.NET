<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="ClientHome.aspx.cs" Inherits="Driver_management.ClientManagement.ClientHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="~/css/customerHome.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container">
       

<div class="row">
    <div class="col-lg-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                ההזמנות שלך
            </div>
            <!-- /.panel-heading -->
            <div class="panel-body">
                <div class="table-responsive">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>מספר הזמנה</th>
                                <th>תאריך</th>
                                <th>סטטוס</th>
                                <th>פעולות</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RptOrders" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("OrderNumber") %></td>
                                        <td><%# FormatDate(Eval("OrderDate")) %></td>
                                        <td><%# Eval("ShippingStatus") %></td>
                                        <td><a href='<%# "OrderDetails.aspx?OrderID=" + Eval("ShipmentID") %>' class="btn btn-primary">צפה בפרטים</a></td>
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


            <div class="col-md-4">
                <h2>חדשות ועדכונים</h2>
                <asp:Repeater ID="rptNews" runat="server">
                    <ItemTemplate>
                        <div class="news-item">
                            <h3><%# Eval("NewsTitle") %></h3>
                            <p><%# Eval("NewsSummary") %></p>
                            <a href="NewsDetails.aspx?NewsID=<%# Eval("NewsID") %>" class="btn btn-info">קרא עוד</a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-md-4">
                <h2>צור קשר</h2>
                <p>יש לך שאלה או בעיה? צור איתנו קשר:</p>
                <a href="ContactUs.aspx" class="btn btn-warning">צור קשר</a>
            </div>
        </div>
    </div>
</asp:Content>
