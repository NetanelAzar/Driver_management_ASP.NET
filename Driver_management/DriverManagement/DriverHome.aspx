<%@ Page Title="" Language="C#" MasterPageFile="~/DriverManagement/DriverMaster.Master" AutoEventWireup="true" CodeBehind="DriverHome.aspx.cs" Inherits="Driver_management.DriverManagement.DriverHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <style>
        .news-item {
            border-radius: 10px;
            border: 1px solid #ddd;
            padding: 15px;
            background: #f8f9fa;
            transition: background 0.3s, box-shadow 0.3s;
        }
        .news-item:hover {
            background: #e9ecef;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .btn-custom {
            border-radius: 20px;
        }
        .card-header {
            background: #007bff;
            color: white;
            font-weight: bold;
        }
        .card-body {
            background: #f1f1f1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <h2 class="text-primary">דף הבית של נהג</h2>
        <h3 class="text-muted">שלום, <asp:Label ID="lblUsername" runat="server"></asp:Label></h3>
        
        <div class="row">
            <!-- Today's Deliveries -->
            <div class="col-md-6 mt-4">
                <div class="card">
                    <div class="card-header">
                        <i class="fas fa-truck"></i> משלוחים להיום
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="RptDeliveriesToday" runat="server">
                            <ItemTemplate>
                                <div class="alert alert-info d-flex justify-content-between align-items-center">
                                    <div>
                                        <strong>כתובת: </strong><%# Eval("DestinationAddress") %> <br />
                                        <strong>תאריך: </strong><%# Eval("OrderDate", "{0:yyyy-MM-dd}") %>
                                    </div>
                                    <i class="fas fa-map-marker-alt"></i>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

            <!-- News and Updates -->
            <div class="col-md-6 mt-4">
                <div class="card">
                    <div class="card-header">
                        <i class="fas fa-bullhorn"></i> חדשות ועדכונים
                    </div>
                    <div class="card-body">
                        <asp:Repeater ID="RptNews" runat="server">
                            <ItemTemplate>
                                <div class="news-item mb-3">
                                    <h5 class="font-weight-bold"><%# Eval("NewsTitle") %></h5>
                                    <p><%# Eval("NewsSummary") %></p>
                                    <a href="NewsDetails.aspx?NewsID=<%# Eval("NewsID") %>" class="btn btn-info btn-sm btn-custom">קרא עוד</a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>

        <!-- Deliveries This Month -->
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <i class="fas fa-calendar-day"></i> מספר המשלוחים החודש
                    </div>
                    <div class="card-body text-center">
                        <asp:Label ID="LblDeliveriesThisMonth" runat="server" CssClass="h4 text-primary"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>
