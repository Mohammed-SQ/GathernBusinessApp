<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" MasterPageFile="~/LandingPage.Master" Inherits="TaskManager.LandingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Landing Page</title>
    <link rel="stylesheet" href="Styles/style.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="welcome-section">
        <h1>Welcome to Study Buddy!</h1>
        <p>This is your personalized landing page.</p>
        <asp:Button ID="btnGetStarted" runat="server" Text="Get Started" OnClick="btnGetStarted_Click" CssClass="start-button" />
    </div>
</asp:Content>
