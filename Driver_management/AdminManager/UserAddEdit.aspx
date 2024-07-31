<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="UserAddEdit.aspx.cs" Inherits="Driver_management.AdminManager.UserAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .ck-editor__editable_inline {
            min-height: 400px; /* Adjust as needed */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    
    <script src="https://cdn.tiny.cloud/1/my30q5lf01td1btvxs662qsopn3pcmcm7m8o7nocvalbaf9n/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>

<!-- Place the following <script> and <textarea> tags your HTML's <body> -->
<script>
    tinymce.init({
        selector: 'textarea',
        width: 1000,
        height: 300,
        plugins: [
            'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
            'searchreplace', 'wordcount', 'visualblocks', 'code', 'fullscreen', 'insertdatetime', 'media',
            'table', 'emoticons', 'template', 'codesample'
        ],
        toolbar: 'undo redo | styles | bold italic underline | alignleft aligncenter alignright alignjustify |' +
            'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
            'forecolor backcolor emoticons',
        menu: {
            favs: { title: 'menu', items: 'code visualaid | searchreplace | emoticons' }
        },
        menubar: 'favs file edit view insert format tools table',
        content_style: 'body{font-family:Helvetica,Arial,sans-serif; font-size:16px}',
        directionality: 'rtl'
    });
</script>



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
                            <asp:HiddenField ID="HidDriverID" runat="server" Value="-1" />
                            <div class="form-group">
                                <label>שם לקוח</label>
                                <asp:TextBox ID="TxtUserFullName" CssClass="form-control" runat="server" placeholder="הזן שם נהג" />
                            </div>
                            <div class="form-group">
                                <label>דוא"ל לקוח</label>
                                <asp:TextBox ID="TxtUserMail" CssClass="form-control" runat="server" placeholder="הזן איימיל" />
                            </div>
                            <div class="form-group">
                                <label>פלאפון</label>
                                <asp:TextBox ID="TxtUserPhone" CssClass="form-control" runat="server" placeholder="הזן טלפון נהג" />
                            </div>
                            <div class="form-group">
                                <label>קוד נהג</label>
                                <asp:TextBox ID="TxtUserCode" CssClass="form-control" runat="server" placeholder="הזן שם חברה" />
                            </div>
                            <div class="form-group">
                                <label>עיר</label>
                                <asp:TextBox ID="TxtCityCode" CssClass="form-control" runat="server" placeholder="הזן עיר" />
                            </div>

                            <div class="form-group">
                                <label>אזור</label>
                                <asp:TextBox ID="TxtZoneID" CssClass="form-control" runat="server" placeholder="הזן אזור" />
                            </div>


                            <div class="form-group">
                                <label>מקסימום מקום</label>
                                <asp:TextBox ID="TxtMax" CssClass="form-control" runat="server" placeholder="הזןמקסימום" />
                            </div>

                            <div class="form-group">
                                <label>כתובת</label>
                                <asp:TextBox ID="TxtAddress" CssClass="form-control" runat="server" placeholder="הזן כתובת" />
                            </div>


                            <div class="form-group">
                                <label>סיסמא</label>
                                <asp:TextBox ID="TxtPassword" CssClass="form-control" runat="server" placeholder="הזן סיסמא" />
                            </div>



                            <asp:Button ID="BtnSave" class="btn btn-primary" OnClick="BtnSave_Click" runat="server" Text="שמירה" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="underFooter" runat="server">
</asp:Content>