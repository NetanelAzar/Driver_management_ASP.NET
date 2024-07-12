<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="ClientsAddEdit.aspx.cs" Inherits="Driver_management.AdminManager.ClientsAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ck-editor__editable_inline {
            min-height: 400px; /* ניתן להתאים ככל שצריך */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול לקוחות</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    הוספה / עריכת לקוח
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:HiddenField ID="HidClientID" runat="server" Value="-1" />
                            <div class="form-group">
                                <label>שם לקוח</label>
                                <asp:TextBox ID="TxtClientName" CssClass="form-control" runat="server" placeholder="הזן שם לקוח" />
                            </div>
                            <div class="form-group">
                                <label>דוא"ל לקוח</label>
                                <asp:TextBox ID="TxtClientMail" CssClass="form-control" runat="server" placeholder="הזן איימיל" />
                            </div>
                            <div class="form-group">
                                <label>טלפון לקוח</label>
                                <asp:TextBox ID="TxtClientPhone" CssClass="form-control" runat="server" placeholder="הזן טלפון לקוח" />
                            </div>
                            <div class="form-group">
                                <label>שם חברה</label>
                                <asp:TextBox ID="TxtCompanyName" CssClass="form-control" runat="server" placeholder="הזן שם חברה" />
                            </div>
                            <div class="form-group">
                                <label>מיקוד</label>
                                <asp:TextBox ID="TxtCityCode" CssClass="form-control" runat="server" placeholder="הזן מיקוד" />
                            </div>
                            <div class="form-group">
                                <label>כתובת</label>
                                <asp:TextBox ID="TxtAddress" CssClass="form-control" runat="server" placeholder="הזן כתובת" />
                            </div>
                            <asp:Button ID="BtnSave" class="btn btn-primary" OnClick="BtnSave_Click" runat="server" Text="שמירה" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>