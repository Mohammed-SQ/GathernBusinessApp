<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TaskManager.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>Login</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8d7da;
            margin: 0;
            padding: 0;
        }

        .login-container {
            width: 300px;
            margin: 100px auto;
            padding: 20px;
            background-color: #d9a7a9;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
        }

        h1 {
            text-align: center;
            color: #4a4a4a;
        }

        label {
            font-size: 14px;
            color: #4a4a4a;
        }

        input {
            width: 100%;
            padding: 8px;
            margin: 10px 0;
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
            margin-top: 10px;
        }

        .signup-link a {
            color: #4a4a4a;
            text-decoration: none;
        }

        .signup-link a:hover {
            text-decoration: underline;
        }

        .trust-device {
            display: flex;
            align-items: center;
        }

        .trust-device input {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <div class="login-container">
        <h1>Sign-in to visit your buddy!</h1>
        <form id="loginForm" runat="server">
            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="input-box" placeholder="Enter your email"></asp:TextBox>

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input-box" TextMode="Password" placeholder="Enter your password"></asp:TextBox>

            <asp:Button ID="btnSignIn" runat="server" Text="Sign In" CssClass="submit-button" OnClick="btnSignIn_Click" />

            <div class="trust-device">
                <input type="checkbox" id="trustDevice" />
                <label for="trustDevice">Trust This Device</label>
            </div>

            <div class="signup-link">
                Need an Account? <a href="Register.aspx">Sign Up</a>
            </div>
        </form>
    </div>
</body>
</html>
