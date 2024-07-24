<%@ Page Title="" Language="C#" MasterPageFile="~/DriverManagement/DriverMaster.Master" AutoEventWireup="true" CodeBehind="DriverHome.aspx.cs" Inherits="Driver_management.DriverManagement.DriverHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/5.3.0/css/bootstrap.min.css" integrity="sha384-fQ5SO/v5Lo8U5DAxj2ImKgfik2a5hvC4pQe4qQ3R5bD8YKeA1F2Er5T2tkf2u3A" crossorigin="anonymous">
    <!-- Font Awesome CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <style>
        .news-item {
            border-radius: 12px;
            border: 1px solid #ddd;
            padding: 20px;
            background: #ffffff;
            transition: background 0.3s, box-shadow 0.3s;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        .news-item:hover {
            background: #f1f1f1;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
        }
        .btn-custom {
            border-radius: 30px;
            font-size: 0.875rem;
            font-weight: 500;
        }
        .card-header {
            background: #007bff;
            color: white;
            font-weight: 600;
            font-size: 1.25rem;
        }
        .card-body {
            background: #f8f9fa;
            border-radius: 8px;
        }
        .alert-info {
            background-color: #e9f7fd;
            color: #31708f;
        }
        .container {
            max-width: 1200px;
        }
        .h4 {
            font-size: 2rem;
        }
        .text-primary {
            color: #007bff !important;
        }
        .text-muted {
            color: #6c757d !important;
        }











        .panel {
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.panel-heading {
    background-color: #f5f5f5;
    border-bottom: 1px solid #ddd;
    padding: 15px;
    font-size: 16px;
    font-weight: bold;
}

.panel-body {
    padding: 15px;
}

canvas {
    display: block;
    max-width: 80%;
    height: auto;
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
                                <div class="alert alert-info d-flex justify-content-between align-items-center mb-3 p-3 rounded">
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







<!-- גרף Donut נוסף -->
<div class="col-lg-4 col-md-2">
    <div class="panel panel-default">
        <div class="panel-heading">
            <i class="fa fa-bar-chart-o fa-fw"></i> Shipments per Month (Donut)
        </div>
        <div class="panel-body">
            <canvas id="donutChart" width="300" height="300"></canvas>
        </div>
    </div>
</div>
<!-- -->

<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script type="text/javascript">
    function loadChart(months, shipmentCounts) {
        var ctx2 = document.getElementById('donutChart').getContext('2d');
        new Chart(ctx2, {
            type: 'bar',
            data: {
                labels: months.map(month => `Month ${month}`),
                datasets: [{
                    label: 'Shipments per Month',
                    data: shipmentCounts,
                    backgroundColor: [
                        'rgba(75, 192, 192, 0.4)',
                        'rgba(153, 102, 255, 0.4)',
                        'rgba(255, 159, 64, 0.4)',
                        'rgba(255, 99, 132, 0.4)',
                        'rgba(54, 162, 235, 0.4)'
                    ],
                    borderColor: [
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)'
                    ],
                    borderWidth: 2
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'bottom',
                    },
                    tooltip: {
                        callbacks: {
                            label: function (tooltipItem) {
                                return `Month ${tooltipItem.label}: ${tooltipItem.raw} shipments`;
                            }
                        }
                    }
                }
            }
        });
    }
</script>








    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
    <!-- Optionally include scripts or other content for the footer -->
</asp:Content>
