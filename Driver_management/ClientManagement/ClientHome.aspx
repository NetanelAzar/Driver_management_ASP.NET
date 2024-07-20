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
        .chat-panel {
            position: fixed;
            bottom: 0;
            right: 20px;
            width: 300px;
            z-index: 1000;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 15px 15px 0 0;
            display: none;
        }
        .chat-panel .panel-heading {
            border-radius: 15px 15px 0 0;
            background: linear-gradient(to right, #007bff, #6610f2);
            color: white;
            padding: 10px 15px;
        }
        .chat-panel .panel-body {
            max-height: 300px;
            overflow-y: auto;
            padding: 15px;
        }
        .chat-panel .chat {
            list-style: none;
            padding: 0;
            margin: 0;
        }
        .chat-panel .chat li {
            margin-bottom: 10px;
        }
        .chat-panel .chat li .chat-img {
            width: 50px;
            height: 50px;
        }
        .chat-panel .chat li .chat-body {
            margin-left: 60px;
        }
        .panel-footer {
            padding: 10px 15px;
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
                         <button id="btnOpenChat" class="btn btn-warning btn-custom" type="button">צור קשר</button>
                    </div>
                </div>
            </div>
        </div>
    </div>







    <!-- חלון הצ'אט -->
    <div id="chatWindow" class="chat-panel panel panel-default">
        <div class="panel-heading">
            <i class="fa fa-comments fa-fw"></i> צ'אט
            <button type="button" class="btn btn-default btn-xs pull-left" id="btnCloseChat">
                <i class="fa fa-times"></i>
            </button>
        </div>
        <div class="panel-body">
            <ul class="chat" id="chatMessages">
                <li class="left clearfix">
                    <span class="chat-img pull-left">
                        <img src="http://placehold.it/50/55C1E7/fff" alt="User Avatar" class="img-circle" />
                    </span>
                    <div class="chat-body clearfix">
                        <div class="header">
                            <strong class="primary-font"> <asp:Label ID="lblUsername" runat="server" CssClass="nav-link"></asp:Label></strong>
                            <small class="pull-right text-muted">
                                <asp:Label ID="lblLastLogin" runat="server" CssClass="text-muted"></asp:Label>

                            </small>
                        </div>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                        </p>
                    </div>
                </li>
            </ul>
        </div>
        <div class="panel-footer">
            <div class="input-group">
                <input id="btn-input" type="text" class="form-control input-sm" placeholder="Type your message here..." />
                <span class="input-group-btn">
                    <button class="btn btn-warning btn-sm" id="btn-chat" type="button">שלח</button>
                </span>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Footer" ContentPlaceHolderID="Footer" runat="server">

</asp:Content>
<asp:Content ID="underFooter" ContentPlaceHolderID="underFooter" runat="server">
    <script>
        window.onload = function () {
            document.getElementById('btnOpenChat').addEventListener('click', function () {
                document.getElementById('chatWindow').style.display = 'block';
            });

            document.getElementById('btnCloseChat').addEventListener('click', function () {
                document.getElementById('chatWindow').style.display = 'none';
            });

            document.getElementById('btn-chat').addEventListener('click', function () {
                var message = document.getElementById('btn-input').value;
                var chatMessages = document.getElementById('chatMessages');
                var newMessage = document.createElement('li');
                newMessage.classList.add('right', 'clearfix');
                newMessage.innerHTML = `
                    <span class="chat-img pull-right">
                        <img src="http://placehold.it/50/FA6F57/fff" alt="User Avatar" class="img-circle" />
                    </span>
                    <div class="chat-body clearfix">
                        <div class="header">
                            <strong class="primary-font pull-right">אתה</strong>
                            <small class="pull-left text-muted">
                                <i class="fa fa-clock-o fa-fw"></i> עכשיו
                            </small>
                        </div>
                        <p>${message}</p>
                    </div>`;
                chatMessages.appendChild(newMessage);
                document.getElementById('btn-input').value = '';
                
            });
        }
    </script>
</asp:Content>
