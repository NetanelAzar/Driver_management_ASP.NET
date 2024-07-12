<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AddressUserList.aspx.cs" Inherits="Driver_management.AdminManager.AddressUserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Your CSS styles here */
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            Addresses List
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                <asp:DropDownList ID="ddlUserIDs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserIDs_SelectedIndexChanged" CssClass="form-control">
                </asp:DropDownList>

                <asp:GridView ID="GridViewAddresses" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
                    <Columns>
                        <asp:BoundField DataField="ShipmentID" HeaderText="Shipment ID" />
                        <asp:BoundField DataField="SourceCity" HeaderText="Source City" />
                        <asp:BoundField DataField="SourceAddress" HeaderText="Source Address" />
                        <asp:BoundField DataField="DestinationCity" HeaderText="Destination City" />
                        <asp:BoundField DataField="DestinationAddress" HeaderText="Destination Address" />
                        <asp:BoundField DataField="CustomerPhone" HeaderText="Customer Phone" />
                        <asp:BoundField DataField="PickupDate" HeaderText="Pickup Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ShipmentDate" HeaderText="Shipment Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:dd/MM/yyyy}" /> 
                        <asp:BoundField DataField="NumberOfPackages" HeaderText="Number of Packages" />
                        <asp:BoundField DataField="DriverID" HeaderText="Driver ID" />
                        <asp:BoundField DataField="CustomerID" HeaderText="Customer ID" />
                        <asp:BoundField DataField="ShippingStatus" HeaderText="Shipping Status" /> 
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
</asp:Content>
