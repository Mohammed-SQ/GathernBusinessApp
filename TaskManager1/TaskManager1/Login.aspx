<%@ Page Title="Login" Language="C#" MasterPageFile="~/LandingPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TaskManager1.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-container {
            width: 500px;
            margin: 150px auto;
            padding: 40px;
            background-color: #d9a7a9;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }
        h1 {
            text-align: left;
            font-size: 36px;
            color: #4a4a4a;
        }
        label {
            display: block; 
            font-size: 1rem;
            margin-bottom: 5px;
            color: #4a4a4a;
            text-align: left;
        }
        input {
            width: 100%;
            padding: 8px;
            margin: 10px 0 20px 0; 
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .submit-button {
            width: 100%;
            padding: 10px;
            background-color: #4a4a4a;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }
        .submit-button:hover {
            background-color: #333;
        }
        .signup-link {
            text-align: center;
            display: block;
            margin-top: 20px;
        }
        .signup-link a {
            color: #4a4a4a;
            text-decoration: none;
        }
        .signup-link a:hover {
            text-decoration: underline;
        }
        .button-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 10px;
        }
        .trust-device {
            display: flex;
            align-items: center;
            justify-content: center; 
            gap: 5px;
            font-size: 0.9rem;
        }
        .error-message {
            color: red;
            font-size: 14px;
            text-align: center;
            margin-bottom: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <h1>Welcome Back! Sign In to Continue</h1>

        <label for="txtEmail">Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="input-box" placeholder="Enter your email"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
            ControlToValidate="txtEmail" 
            ErrorMessage="Email is required." 
            Display="Dynamic" 
            ForeColor="Red" />

        <label for="txtPassword">Password:</label>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-box" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
            ControlToValidate="txtPassword" 
            ErrorMessage="Password is required." 
            Display="Dynamic" 
            ForeColor="Red" />

        <!-- Error message label -->
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false" />

        <asp:Button ID="btnSignIn" runat="server" Text="Sign In" CssClass="submit-button" OnClick="btnSignIn_Click" />

        <div class="trust-device">
            <input type="checkbox" id="trustDevice" />
            <label for="trustDevice">Trust This Device</label>
        </div>

        <div class="signup-link">
            Need an Account? <a href="Register.aspx">Sign Up</a>
        </div>
    </div>
</asp:Content>
