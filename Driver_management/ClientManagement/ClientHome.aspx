<%@ Page Title="" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="ClientHome.aspx.cs" Inherits="Driver_management.ClientManagement.ClientHome" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" integrity="sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z" crossorigin="anonymous">
<style>
    body {
        background-color: #f8f9fa;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .card-custom {
        border-radius: 15px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        transition: transform 0.3s ease;
    }

    .card-custom:hover {
        transform: scale(1.02);
    }

    .card-header-custom {
        border-radius: 15px 15px 0 0;
        background: linear-gradient(to right, #ff7e5f, #feb47b);
        color: white;
        padding: 10px 20px;
    }


    .table-responsive {
        border-radius: 15px;
        overflow: hidden;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }

    table {
        width: 100%;
        border-collapse: collapse;
        margin-bottom: 0;
    }

    thead {
        background-color: #007bff;
        color: white;
    }

    thead th {
        padding: 12px 15px;
        text-align: center;
        font-weight: bold;
    }

    tbody tr:nth-child(even) {
        background-color: #f8f9fa;
    }

    tbody td {
        padding: 10px 15px;
        text-align: center;
        transition: background-color 0.3s ease;
    }

    tbody tr:hover td {
        background-color: #e9ecef;
    }

    .btn-custom {
        border: none;
        padding: 5px 10px;
        border-radius: 15px;
        transition: background 0.3s ease, color 0.3s ease;
        font-size: 14px;
    }

    .btn-custom:hover {
        background: linear-gradient(to right, #6610f2, #007bff);
        color: white;
    }

    .header-title {
        text-align: center;
        margin-bottom: 30px;
        color: #333;
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
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
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

    .news-item h5 {
        color: #007bff;
    }

    .news-item a {
        color: #6610f2;
    }
</style>

</asp:Content>

<asp:Content ID="MainCnt" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <div class="header-title">
            <h1>ברוך הבא</h1>
            <asp:Label ID="lblUsername" runat="server" CssClass="nav-link"></asp:Label>
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
                                                    <a href='<%# "OrderDetails.aspx?OrderID=" + Eval("ShipmentID") %>' class="btn btn-primary btn-sm btn-custom">צפה בפרטים
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
                        <button id="btnOpenChat" class="btn btn-warning btn-custom" type="button">פתח צ'אט</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="chatPanel" class="chat-panel">
        <div class="panel-heading">
            <h4 class="panel-title">שוחח עם שירות הלקוחות</h4>
            <button id="btnCloseChat" class="close" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="panel-body">
            <ul id="chatMessages" class="chat">
                <!-- Messages will be appended here -->
            </ul>
        </div>
        <div class="panel-footer">
            <div class="input-group">
                <input id="messageInput" type="text" class="form-control" placeholder="הקלד את ההודעה שלך כאן..." />
                <div class="input-group-append">
                    <button id="btnSendMessage" class="btn btn-primary">שלח</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="underFooter" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#btnOpenChat').click(function () {
                $('#chatPanel').show();
                loadMessages();
            });

            $('#btnCloseChat').click(function () {
                $('#chatPanel').hide();
            });

            $('#btnSendMessage').click(function (e) {
                e.preventDefault();

                var message = $('#messageInput').val().trim();
                if (message === '') {
                    alert('אנא הקלד הודעה');
                    return;
                }

                $.ajax({
                    type: 'POST',
                    url: 'ClientHome.aspx/SendMessage',
                    data: JSON.stringify({ message: message }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        $('#chatMessages').append('<li><strong>אני:</strong> ' + message + '</li>');
                        $('#messageInput').val('');
                    },
                    error: function (xhr, status, error) {
                        console.error("Error sending message: ", error);
                    }
                });
            });



            function loadMessages() {
                $.ajax({
                    type: "POST",
                    url: "ClientHome.aspx/GetMessages",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        try {
                            var messages = JSON.parse(response.d); // Ensure parsing response correctly
                            if (Array.isArray(messages)) {
                                $('#chatMessages').empty(); // Clear existing messages
                                messages.forEach(function (msg) {
                                    var sender = msg.IsFromCustomer ? 'לקוח' : 'מנהל';
                                    var messageHtml = '<li><strong>' + sender + ':</strong> ' + msg.MessageText + ' <small>(' + new Date(parseInt(msg.SentDate.substr(6))).toLocaleString() + ')</small></li>';
                                    $('#chatMessages').append(messageHtml);
                                });
                            } else {
                                console.error("Unexpected response format: ", response.d);
                            }
                        } catch (e) {
                            console.error("Error parsing JSON: ", e);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("Error loading messages: ", error);
                    }
                });
            }




            function sendMessage(message) {
                $.ajax({
                    type: "POST",
                    url: "ClientHome.aspx/SendMessage",
                    data: JSON.stringify({ message: message }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        try {
                            var result = JSON.parse(response.d); // Ensure parsing response correctly
                            if (result.success) {
                                console.log(result.success);
                            } else {
                                console.error(result.error);
                            }
                        } catch (e) {
                            console.error("Error parsing JSON: ", e);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error("Error sending message: ", error);
                    }
                });
            }

        });
    </script>

</asp:Content>
