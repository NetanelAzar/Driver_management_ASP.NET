<%@ Page Title="תשלום הושלם" Language="C#" MasterPageFile="~/ClientManagement/ClientMaster.Master" AutoEventWireup="true" CodeBehind="PastPayment.aspx.cs" Inherits="Driver_management.ClientManagement.PastPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/canvas-confetti@1.5.3/dist/confetti.browser.min.js"></script>
    <style>
        body, html {
            margin: 0;
            padding: 0;
            height: 100%;
            overflow: hidden;
        }
        
        canvas {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 0;
        }

        .confirmation-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;
            position: relative;
            z-index: 1;
        }
        
        .confirmation-message {
            text-align: center;
            max-width: 600px;
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 30px;
        }

        .confirmation-title {
            font-size: 24px;
            font-weight: bold;
            color: #28a745;
        }
        
        .confirmation-text {
            font-size: 16px;
            color: #6c757d;
            margin: 20px 0;
        }
        
        .back-home-btn {
            margin-top: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <canvas id="canvas"></canvas>
    <div class="confirmation-container">
        <div class="confirmation-message">
            <div class="confirmation-title">תודה! התשלום עבר בהצלחה.</div>
            <div class="confirmation-text">
                ההזמנה שלך התקבלה בהצלחה. תודה על הרכישה שלך! 
                <br /><br />
                אם יש לך שאלות נוספות, אל תהסס לפנות אלינו.
            </div>
            <a href="ClientHome.aspx" class="btn btn-primary back-home-btn">חזור לדף הבית</a>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Footer" ContentPlaceHolderID="Footer" runat="server">

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="underFooter" runat="server">
    <script>
        let W = window.innerWidth;
        let H = window.innerHeight;
        const canvas = document.getElementById("canvas");
        const context = canvas.getContext("2d");
        const maxConfettis = 150;
        const particles = [];

        const possibleColors = [
            "DodgerBlue", "OliveDrab", "Gold", "Pink", "SlateBlue", "LightBlue",
            "Gold", "Violet", "PaleGreen", "SteelBlue", "SandyBrown", "Chocolate", "Crimson"
        ];

        function randomFromTo(from, to) {
            return Math.floor(Math.random() * (to - from + 1) + from);
        }

        function confettiParticle() {
            this.x = Math.random() * W; // x
            this.y = Math.random() * H - H; // y
            this.r = randomFromTo(11, 33); // radius
            this.d = Math.random() * maxConfettis + 11;
            this.color = possibleColors[Math.floor(Math.random() * possibleColors.length)];
            this.tilt = Math.floor(Math.random() * 33) - 11;
            this.tiltAngleIncremental = Math.random() * 0.07 + 0.05;
            this.tiltAngle = 0;

            this.draw = function () {
                context.beginPath();
                context.lineWidth = this.r / 2;
                context.strokeStyle = this.color;
                context.moveTo(this.x + this.tilt + this.r / 3, this.y);
                context.lineTo(this.x + this.tilt, this.y + this.tilt + this.r / 5);
                return context.stroke();
            };
        }

        function Draw() {
            const results = [];

            requestAnimationFrame(Draw);
            context.clearRect(0, 0, W, window.innerHeight);

            for (var i = 0; i < maxConfettis; i++) {
                results.push(particles[i].draw());
            }

            let particle = {};
            let remainingFlakes = 0;
            for (var i = 0; i < maxConfettis; i++) {
                particle = particles[i];
                particle.tiltAngle += particle.tiltAngleIncremental;
                particle.y += (Math.cos(particle.d) + 3 + particle.r / 2) / 2;
                particle.tilt = Math.sin(particle.tiltAngle - i / 3) * 15;

                if (particle.y <= H) remainingFlakes++;

                if (particle.x > W + 30 || particle.x < -30 || particle.y > H) {
                    particle.x = Math.random() * W;
                    particle.y = -30;
                    particle.tilt = Math.floor(Math.random() * 10) - 20;
                }
            }

            return results;
        }

        window.addEventListener("resize", function () {
            W = window.innerWidth;
            H = window.innerHeight;
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;
        }, false);

        for (var i = 0; i < maxConfettis; i++) {
            particles.push(new confettiParticle());
        }

        canvas.width = W;
        canvas.height = H;
        Draw();
    </script>
</asp:Content>
