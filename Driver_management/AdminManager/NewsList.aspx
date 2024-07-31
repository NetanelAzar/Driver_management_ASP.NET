<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="Driver_management.AdminManager.NewsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS של Bootstrap ו-Custom CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom.css" rel="stylesheet" />
    <style>
        .news-card-container {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            justify-content: center;
        }
        .news-card {
            border: 1px solid #e0e0e0;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            overflow: hidden;
            background-color: #ffffff;
            width: 100%;
            max-width: 360px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            margin-bottom: 20px;
        }
        .news-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }
        .news-card-header {
            background-color: #007bff;
            color: #ffffff;
            padding: 15px;
            font-size: 1.25rem;
            font-weight: bold;
            border-bottom: 1px solid #e0e0e0;
        }
        .news-card-body {
            padding: 15px;
        }
        .news-summary {
            margin-bottom: 15px;
            font-size: 1rem;
            color: #333;
        }
        .news-date {
            color: #777;
            font-size: 0.9rem;
            margin-bottom: 10px;
        }
        .btn-action {
            margin-right: 5px;
        }

        /* Responsive design for mobile devices */
        @media (max-width: 767px) {
            .news-card-container {
                flex-direction: column;
                align-items: center;
            }
            .news-card {
                max-width: 100%;
            }
            .news-card-header {
                font-size: 1rem;
            }
            .news-summary {
                font-size: 0.9rem;
            }
            .news-date {
                font-size: 0.8rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container">
        <h1 class="page-header">חדשות ועדכונים</h1>
        <div class="news-card-container">
            <!-- הצגת חדשות ככרטיסים -->
            <asp:Repeater ID="rptNews" runat="server">
                <ItemTemplate>
                    <div class="news-card">
                        <div class="news-card-header">
                            <%# Eval("NewsTitle") %>
                        </div>
                        <div class="news-card-body">
                            <div class="news-summary">
                                <%# Eval("NewsSummary") %>
                            </div>
                            <div class="news-date">
                                <%# Eval("NewsDate", "{0:yyyy-MM-dd HH:mm}") %>
                            </div>
                            <a href="ManageNews.aspx?NewsID=<%# Eval("NewsID") %>" class="btn btn-warning btn-sm btn-action">ערוך</a>
                            <a href="javascript:void(0);" onclick="confirmDelete(<%# Eval("NewsID") %>)" class="btn btn-danger btn-sm btn-action">מחק</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
    <!-- JS של Bootstrap ו-Custom JS -->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/custom.js"></script>
    <script>
        function confirmDelete(newsID) {
            if (confirm('האם אתה בטוח שברצונך למחוק את החדשה הזו?')) {
                window.location.href = 'NewsList.aspx?DeleteID=' + newsID;
            }
        }
    </script>
</asp:Content>
