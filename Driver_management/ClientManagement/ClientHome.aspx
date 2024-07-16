<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="ClientHome.aspx.cs" Inherits="Driver_management.ClientManagement.ClientHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
    <style>
        .card-custom {
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }
        .card-header-custom {
            border-radius: 15px 15px 0 0;
            background: linear-gradient(to right, #007bff, #6610f2);
        }
        .btn-custom {
            border: none;
            transition: background 0.3s ease;
        }
        .btn-custom:hover {
            background: linear-gradient(to right, #6610f2, #007bff);
        }
        .header-title {
            text-align: center;
            margin-bottom: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <div class="header-title">
            <h1>ברוך הבא</h1>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <div class="card card-custom">
                    <div class="card-header card-header-custom text-white">
                        <h4 class="card-title mb-0">ההזמנות שלך</h4>
                    </div>
                    <div class="card-body p-4">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered">
                                <thead class="thead-dark">
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
                                                <td>
                                                    <a href='<%# "OrderDetails.aspx?OrderID=" + Eval("ShipmentID") %>' class="btn btn-primary btn-sm btn-custom">
                                                        צפה בפרטים
                                                    </a>
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
            <div class="col-lg-4">
                <div class="card card-custom mb-3">
                    <div class="card-header bg-success text-white">
                        <h4 class="card-title mb-0">חדשות ועדכונים</h4>
                    </div>
                    <div class="card-body p-4">
                        <asp:Repeater ID="rptNews" runat="server">
                            <ItemTemplate>
                                <div class="news-item mb-3">
                                    <h5><%# Eval("NewsTitle") %></h5>
                                    <p><%# Eval("NewsSummary") %></p>
                                    <a href="NewsDetails.aspx?NewsID=<%# Eval("NewsID") %>" class="btn btn-info btn-sm btn-custom">קרא עוד</a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="card card-custom">
                    <div class="card-header bg-warning text-white">
                        <h4 class="card-title mb-0">צור קשר</h4>
                    </div>
                    <div class="card-body p-4">
                        <p>יש לך שאלה או בעיה? צור איתנו קשר:</p>
                        <a href="ContactUs.aspx" class="btn btn-warning btn-custom">צור קשר</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
