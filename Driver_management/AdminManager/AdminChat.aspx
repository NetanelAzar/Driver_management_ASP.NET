<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AdminChat.aspx.cs" Inherits="Driver_management.AdminManager.AdminChat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .header-title {
            margin-bottom: 20px;
            text-align: center;
        }

        .list-group-item {
            cursor: pointer;
        }

        .chat-panel {
            border: 1px solid #ddd;
            border-radius: 8px;
            overflow: hidden;
            height: 500px;
            display: flex;
            flex-direction: column;
        }

        .chat-panel .panel-body {
            flex: 1;
            overflow-y: auto;
            padding: 15px;
            background-color: #f9f9f9;
        }

        .chat {
            list-style-type: none;
            padding: 0;
            margin: 0;
        }

        .chat li {
            padding: 10px;
            border-radius: 8px;
            margin-bottom: 10px;
            position: relative;
        }

        .customer-message {
            background-color: #e1f5fe;
            text-align: left;
        }

        .admin-message {
            background-color: #f1f8e9;
            text-align: right;
        }

        .message-time {
            display: block;
            font-size: 0.75rem;
            color: #999;
        }

        .form-control {
            border-radius: 8px;
        }

        .btn-primary {
            border-radius: 8px;
        }

        .list-group-item-action:hover {
            background-color: #f1f1f1;
        }

        #messagePanel {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-5">
        <div class="header-title">
            <h1>שיחות עם לקוחות</h1>
        </div>

        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <asp:Repeater ID="rptCustomers" runat="server">
                        <ItemTemplate>
                            <a href="javascript:void(0);" onclick="loadChat('<%# Eval("ClientID") %>');" class="list-group-item list-group-item-action">
                                <%# Eval("ClientName") %>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="col-md-9">
                <div class="chat-panel">
                    <div class="panel-body">
                        <ul id="adminChatMessages" class="chat">
                            <asp:Repeater ID="rptMessages" runat="server">
                                <ItemTemplate>
                                    <li class='<%# (bool)Eval("IsFromCustomer") ? "customer-message" : "admin-message" %>'>
                                        <strong><%# (bool)Eval("IsFromCustomer") ? "לקוח:" : "נציג:" %></strong> <%# Eval("MessageText") %>
                                        <span class="message-time"><%# Eval("SentDate", "{0:dd/MM/yyyy HH:mm}") %></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div id="messagePanel" class="mt-2">
                            <asp:TextBox ID="txtNewMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="כתוב הודעה..." />
                            <asp:Button ID="btnSendMessage" runat="server" CssClass="btn btn-primary mt-2" Text="שלח הודעה" OnClick="btnSendMessage_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfSelectedCustomerID" runat="server" />
    <asp:Button ID="btnPostBack" runat="server" Style="display:none;" OnClick="btnPostBack_Click" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function loadChat(customerId) {
            document.getElementById('<%= hfSelectedCustomerID.ClientID %>').value = customerId;
            document.getElementById('<%= btnPostBack.ClientID %>').click(); // Trigger the postback
        }

        // Show the message panel when a customer is selected
        function showMessagePanel() {
            document.getElementById('messagePanel').style.display = 'block';
        }

        // Automatically show message panel when page loads if there is a selected customer
        $(document).ready(function() {
            if (document.getElementById('<%= hfSelectedCustomerID.ClientID %>').value) {
                showMessagePanel();
            }
        });
    </script>
</asp:Content>

<asp:Content ID="underFooter" ContentPlaceHolderID="underFooter" runat="server">
</asp:Content>
