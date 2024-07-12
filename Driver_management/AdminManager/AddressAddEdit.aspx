<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AddressAddEdit.aspx.cs" Inherits="Driver_management.AdminManager.AddressAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ck-editor__editable_inline {
            min-height: 400px; /* ניתן להתאים ככל שצריך */
        }
    </style>
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
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול משלוחים</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    הוספה / עריכת משלוח
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:HiddenField ID="HidShipmentID" runat="server" Value="-1" />
                            <div class="form-group">
                                <label>כתובת מקור</label>
                                <asp:TextBox ID="TxtSourceAddress" CssClass="form-control" runat="server" placeholder="הזן כתובת מקור" />
                            </div>
                            <div class="form-group">
                                <label>עיר מקור</label>
                                <asp:TextBox ID="TxtSourceCity" CssClass="form-control" runat="server" placeholder="הזן עיר מקור" />
                            </div>
                            <div class="form-group">
                                <label>כתובת יעד</label>
                                <asp:TextBox ID="TxtDestinationAddress" CssClass="form-control" runat="server" placeholder="הזן כתובת יעד" />
                            </div>
                            <div class="form-group">
                                <label>עיר יעד</label>
                                <asp:DropDownList ID="DdlDestinationCity" CssClass="form-control" runat="server" DataTextField="CityName" DataValueField="CityId" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- בחר עיר יעד --" Value="-1" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>טלפון לקוח</label>
                                <asp:TextBox ID="TxtCustomerPhone" CssClass="form-control" runat="server" placeholder="הזן טלפון לקוח" />
                            </div>
                            <div class="form-group">
                                <label>תאריך איסוף</label>
                                <asp:TextBox ID="TxtPickupDate" CssClass="form-control" runat="server" placeholder="הזן תאריך איסוף" />
                            </div>
                            <div class="form-group">
                                <label>תאריך מסירה</label>
                                <asp:TextBox ID="TxtDeliveryDate" CssClass="form-control" runat="server" placeholder="הזן תאריך מסירה" />
                            </div>
                            <div class="form-group">
                                <label>תאריך משלוח</label>
                                <asp:TextBox ID="TxtShipmentDate" CssClass="form-control" runat="server" placeholder="הזן תאריך משלוח" />
                            </div>
                            <div class="form-group">
                                <label>תאריך הזמנה</label>
                                <asp:TextBox ID="TxtOrderDate" CssClass="form-control" runat="server" placeholder="הזן תאריך הזמנה" />
                            </div>
                            <div class="form-group">
                                <label>מספר חבילות</label>
                                <asp:TextBox ID="TxtNumberOfPackages" CssClass="form-control" runat="server" placeholder="הזן מספר חבילות" />
                            </div>
                            <div class="form-group">
                                <label>מספר נהג</label>
                                <asp:DropDownList ID="DdlDriverID" CssClass="form-control" runat="server" DataTextField="DriverNameWithID" DataValueField="DriverID" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- בחר נהג --" Value="-1" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>סטאטוס משלוח</label>
                                <asp:DropDownList ID="DdlShipmentStatus" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- בחר סטאטוס משלוח --" Value="-1" />
                                    <asp:ListItem Text="התקבל במערכת" Value="התקבל במערכת" />
                                    <asp:ListItem Text="בדרך ליעד" Value="בדרך ליעד" />
                                    <asp:ListItem Text="נמסר" Value="נמסר" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>מספר לקוח</label>
                                <asp:TextBox ID="TxtCustomerID" CssClass="form-control" runat="server" placeholder="הזן מספר לקוח" />
                            </div>
                            <div class="form-group">
                                <label>מספר הזמנה</label>
                                <asp:TextBox ID="TxtOrderNumber" CssClass="form-control" runat="server" placeholder="הזן מספר הזמנה" />
                            </div>
                            <asp:Button ID="BtnSave" class="btn btn-primary" OnClick="BtnSave_Click" runat="server" Text="שמירה" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>