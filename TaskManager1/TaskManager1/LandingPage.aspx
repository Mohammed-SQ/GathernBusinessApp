<%@ Page Title="Landing Page" Language="C#" MasterPageFile="~/LandingPage.Master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="TaskManager1.LandingPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
            color: #333;
        }

        .header {
            background: linear-gradient(to right, #6a0dad, #7a003c);
            color: white;
            padding: 60px 20px;
            text-align: center;
            position: relative;
            border-bottom-left-radius: 50% 20px;
            border-bottom-right-radius: 50% 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .header img {
            width: 150px;
            display: block;
            margin: 0 auto 10px;
        }

        .header h1 {
            font-size: 42px;
            margin: 0;
            font-weight: bold;
            letter-spacing: 1px;
            text-shadow: 2px 2px 5px rgba(0, 0, 0, 0.3);
        }

        .intro {
            text-align: center;
            padding: 60px 20px;
            background-color: white;
            margin: 20px auto;
            max-width: 900px;
            border-radius: 20px;
            box-shadow: 0 8px 15px rgba(0, 0, 0, 0.1);
        }

        .intro h1 {
            font-size: 36px;
            color: #7a003c;
            margin-bottom: 20px;
        }

        .intro p {
            font-size: 18px;
            color: #555;
            line-height: 1.8;
        }

        .team {
            background-color: #f9f9f9;
            padding: 50px 20px;
            text-align: center;
        }

        .team h2 {
            font-size: 32px;
            color: #6a0dad;
            margin-bottom: 30px;
        }

        .team ul {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 20px;
            list-style: none;
            padding: 0;
            margin: 0;
        }

        .team li {
            background: linear-gradient(to bottom, #ffffff, #f2f2f2);
            padding: 20px;
            border-radius: 15px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
            font-size: 16px;
            color: #333;
            width: 180px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .team li:hover {
            transform: translateY(-10px);
            box-shadow: 0 6px 15px rgba(0, 0, 0, 0.15);
        }

        .buttons {
            text-align: center;
            margin: 0px 0;
        }

        .buttons a {
            display: inline-block;
            text-decoration: none;
            color: white;
            background: #7a003c;
            padding: 15px 40px;
            border-radius: 50px;
            font-size: 18px;
            font-weight: bold;
            margin: 10px;
            transition: background 0.3s ease;
        }

        .buttons a:hover {
            background: #6a0dad;
        }

        .footer {
            background-color: #333;
            color: white;
            text-align: center;
            padding: 15px 0;
            font-size: 14px;
            margin-top: 40px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header">
        <img src="Images/DCC.jpg" alt="DCC Logo">
        <h1>Welcome to TaskManager</h1>
    </div>

    <div class="intro">
        <h1>Manage Your Tasks Effortlessly</h1>
        <p>
            TaskManager is your ultimate tool for staying organized and productive. With features to track, manage, and prioritize tasks, it helps you meet your goals efficiently.
        </p>
    </div>

    <div class="team">
        <h2>Meet Our Team</h2>
        <ul>
            <li>Faisal Alsobaie<br>ID: 202110115</li>
            <li>Ali Attyah<br>ID: 202110581</li>
            <li>Mohammed Alluqman<br>ID: 202210391</li>
            <li>Suliman Alotibe<br>ID: 202210221</li>
            <li>Faisal Salem<br>ID: 202210170</li>
        </ul>
    </div>

    <div class="buttons">
        <a href="Login.aspx">Go to Login</a>
        <a href="Register.aspx">Register Now</a>
    </div>

  
</asp:Content>
