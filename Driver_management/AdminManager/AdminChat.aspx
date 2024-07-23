<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AdminChat.aspx.cs" Inherits="Driver_management.AdminManager.AdminChat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <asp:TextBox ID="txtNewMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="כתוב הודעה..." />
                        <asp:Button ID="btnSendMessage" runat="server" CssClass="btn btn-primary mt-2" Text="שלח הודעה" OnClick="btnSendMessage_Click" />
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
    <script type="text/javascript">
        function loadChat(customerId) {
            document.getElementById('<%= hfSelectedCustomerID.ClientID %>').value = customerId;
            document.getElementById('<%= btnPostBack.ClientID %>').click(); // Trigger the postback
        }
    </script>
</asp:Content>

<asp:Content ID="underFooter" ContentPlaceHolderID="underFooter" runat="server">
</asp:Content>
