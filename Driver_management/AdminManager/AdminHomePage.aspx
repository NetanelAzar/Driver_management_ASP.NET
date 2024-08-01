<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AdminHomePage.aspx.cs" Inherits="Driver_management.AdminManager.AdminHomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!--גרף משלוחים לפי חודש JS-->
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="js/ShipmentsChart.js"></script>
    <!-- -->



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">


    <div class="row">


        <!-- כותרת -->
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">דף ראשי</h1>
            </div>
        </div>
        <!--  -->

        <!--מידע שמוצג בקוביות -->
        <div class="row">
            <!-- Panel for active users count -->
            <div class="col-lg-3 col-md-6">
                <div class="panel panel-green">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-tasks fa-5x"></i>
                            </div>
                            <div class="col-xs-8 text-right">
                                <div class="huge">
                                    <asp:Label ID="lblActiveUsersCount" runat="server"></asp:Label>
                                </div>
                                <div>לקוחות מחוברים</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


                        <div class="col-lg-3 col-md-6">
                <div class="panel panel-yellow">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-shopping-cart fa-5x"></i>
                            </div>
                            <div class="col-xs-8 text-right">
                                <div class="huge">
                                    <asp:Label ID="lblDailyOrdersCount" runat="server"></asp:Label>
                                </div>
                                <div>הזמנות היום!</div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


            <div class="col-lg-3 col-md-6">
                <div class="panel panel-green">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-tasks fa-5x"></i>
                            </div>
                            <div class="col-xs-8 text-right">
                                <div class="huge">12</div>
                                <div>New Tasks!</div>
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <div class="panel-footer">
                            <span class="pull-left">View Details</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>



            <div class="col-lg-3 col-md-6">
                <div class="panel panel-red">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-support fa-5x"></i>
                            </div>
                            <div class="col-xs-8 text-right">
                                <div class="huge">13</div>
                                <div>Support Tickets!</div>
                            </div>
                        </div>
                    </div>
                    <a href="#">
                        <div class="panel-footer">
                            <span class="pull-left">View Details</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <!-- -->




        <!--הזמנות יומיות -->
        <div class="panel panel-default panel panel-default col-lg-6 col-md-3">
            <div class="panel-heading">
                <i class="fa fa-bar-chart-o fa-fw"></i>משלוחים שהתקבלו היום
                            <div class="pull-left">
                            </div>
            </div>
<div class="panel-body">
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <table class="table table-bordered table-hover table-striped">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Destination Address</th>
                            <th>Destination City</th>
  
                            <th>Number of Packages</th>

                            <th>Order Date</th>

                            <th>Payment</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptShipments" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("ShipmentID") %></td>
                                    
                                   
                                    <td><%# Eval("DestinationAddress") %></td>
                                    <td><%# Eval("DestinationCity") %></td>
                                    
                                
                                    
                                  
                                    <td><%# Eval("NumberOfPackages") %></td>
                                    
                                 
                                    <td><%# Eval("OrderDate", "{0:MM/dd/yyyy}") %></td>
                                    
                                    <td><%# Eval("Payment", "{0:C}") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <!-- /.table-responsive -->
        </div>
        <!-- /.col-lg-4 (nested) -->
        <div class="col-lg-8">
            <div id="morris-bar-chart"></div>
        </div>
        <!-- /.col-lg-8 (nested) -->
    </div>
    <!-- /.row -->
</div>


            <!-- /.panel-body -->
        </div>
        <!-- -->




        <!--הודעות מערככת -->
        <div class="row">
            <!-- Column for the notifications -->
            <div class="col-lg-4 col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <i class="fa fa-bell fa-fw"></i>הודעות מערכת
                    </div>
                    <div class="panel-body">
                        <div class="list-group">
                            <asp:Repeater ID="rptNotifications" runat="server" OnItemCommand="rptNotifications_ItemCommand">
                                <itemtemplate>
                                    <a href="#" class="list-group-item">
                                        <i class="fa fa-user fa-fw"></i>
                                        <%# Container.DataItem %>
                                        <span class="pull-right text-muted small"><em></em></span>
                                    </a>
                                </itemtemplate>
                            </asp:Repeater>
                        </div>
                        <!-- /.list-group -->

                    </div>
                </div>
            </div>
        </div>
        <!-- -->



        <!-- גרף משלוחים לפי חודש -->
        <div class="col-lg-6 col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>משלוחים לחודש
               
                </div>
                <div class="panel-body">
                    <canvas id="shipmentsChart" width="400" height="200"></canvas>
                </div>
            </div>
        </div>



        <!-- גרף נוסף -->
        <!-- גרף Donut נוסף -->
        <div class="col-lg-6 col-md-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-bar-chart-o fa-fw"></i>משלוחים לחודש (סופגנייה)
                </div>
                <div class="panel-body">
                    <canvas id="donutChart" width="400" height="200"></canvas>
                </div>
            </div>
        </div>
        <!-- -->
        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
        <script type="text/javascript">
            function loadChart(months, shipmentCounts) {
                // גרף עמודות
                var ctx1 = document.getElementById('shipmentsChart').getContext('2d');
                new Chart(ctx1, {
                    type: 'bar',
                    data: {
                        labels: months.map(month => `Month ${month}`),
                        datasets: [{
                            label: 'Shipments per Month',
                            data: shipmentCounts,
                            backgroundColor: 'rgba(75, 192, 192, 0.2)',
                            borderColor: 'rgba(75, 192, 192, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });

                // גרף Donut
                var ctx2 = document.getElementById('donutChart').getContext('2d');
                new Chart(ctx2, {
                    type: 'doughnut',
                    data: {
                        labels: months.map(month => `Month ${month}`),
                        datasets: [{
                            label: 'Shipments per Month',
                            data: shipmentCounts,
                            backgroundColor: [
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(153, 102, 255, 0.2)',
                                'rgba(255, 159, 64, 0.2)',
                                'rgba(255, 99, 132, 0.2)',
                                'rgba(54, 162, 235, 0.2)'
                            ],
                            borderColor: [
                                'rgba(75, 192, 192, 1)',
                                'rgba(153, 102, 255, 1)',
                                'rgba(255, 159, 64, 1)',
                                'rgba(255, 99, 132, 1)',
                                'rgba(54, 162, 235, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
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
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="underFooter" runat="server">
</asp:Content>
