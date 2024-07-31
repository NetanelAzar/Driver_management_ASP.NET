<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="ManageNews.aspx.cs" Inherits="Driver_management.AdminManager.ManageNews" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS של Bootstrap ו-Custom CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom.css" rel="stylesheet" />
     <script src="https://cdn.tiny.cloud/1/my30q5lf01td1btvxs662qsopn3pcmcm7m8o7nocvalbaf9n/tinymce/7/tinymce.min.js" referrerpolicy="origin"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">




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







    <div class="container">
        <h1 class="page-header">ניהול חדשות</h1>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">הוסף/עדכן חדשות</h3>
                    </div>
                    <div class="panel-body">

                            <div class="form-group">
                                <label for="txtTitle">כותרת</label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label for="txtSummary">סיכום</label>
                                <asp:TextBox ID="txtSummary" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                            </div>
                            <div class="form-group">
                                <label for="txtContent">תוכן</label>
                                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="6" />
                            </div>

                            <div class="form-group">
                                <label for="txtTime">שעה פרסום</label>
                                
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="שמור" OnClick="btnSave_Click" />
                                <a href="NewsList.aspx" class="btn btn-default">חזור לרשימת חדשות</a>
                            </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <!-- JS של Bootstrap ו-Custom JS -->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/custom.js"></script>
</asp:Content>