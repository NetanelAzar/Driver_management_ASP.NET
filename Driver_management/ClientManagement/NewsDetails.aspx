<%@ Page Title="פרטי הודעה" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="NewsDetails.aspx.cs" Inherits="Driver_management.ClientManagement.NewsDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <style>
        .news-details-container {
            max-width: 900px;
            margin: 40px auto;
            padding: 40px;
            background-color: #ffffff;
            border-radius: 12px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            border-left: 6px solid #007bff;
            position: relative;
        }

        .news-title {
            font-size: 32px;
            font-weight: 600;
            color: #007bff;
            margin-bottom: 20px;
            position: relative;
            padding-bottom: 10px;
        }

        .news-title::before {
            content: '';
            position: absolute;
            left: 0;
            bottom: 0;
            width: 60px;
            height: 4px;
            background-color: #007bff;
            border-radius: 2px;
        }

        .news-content {
            font-size: 18px;
            color: #333;
            line-height: 1.6;
        }

        .btn-back {
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 25px;
            padding: 12px 25px;
            font-size: 18px;
            text-decoration: none;
            display: inline-block;
            margin-top: 30px;
            transition: background-color 0.3s ease;
            position: relative;
            text-align: center;
        }

        .btn-back:hover {
            background-color: #0056b3;
        }

        .btn-back:focus {
            outline: none;
        }

        .btn-back i {
            margin-right: 8px;
        }

        .card-custom {
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            background-color: #ffffff;
        }

        .card-header-custom {
            background-color: #007bff;
            color: white;
            border-bottom: 2px solid #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container mt-4">
        <div class="news-details-container">
            <div class="news-title">
                <i class="fas fa-bell"></i> <asp:Label ID="NewsTitle" runat="server"></asp:Label>
            </div>
            <div class="news-content">
                <asp:Label ID="NewsContent" runat="server"></asp:Label>
            </div>
            <a href="News.aspx" class="btn btn-back">
                <i class="fas fa-arrow-left"></i> חזור לחדשות
            </a>
        </div>
    </div>
</asp:Content>
