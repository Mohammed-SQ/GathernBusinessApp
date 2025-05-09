<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TaskManager1.Register" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>Register</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8d7da;
        }
        .register-container {
            max-width: 400px;
            margin: 100px auto;
            padding: 20px;
            background-color: #d9b3b3;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1 {
            text-align: left;
            color: #4a4a4a;
        }
        label {
            display: block;
            margin-bottom: 8px;
            font-weight: bold;
            position: relative;
        }
        input, textarea {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .error {
            color: red;
            font-size: 12px;
            display: none;
        }
        .submit-button {
            width: 100%;
            padding: 10px;
            background-color: #7a003c;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
        }
        .submit-button:disabled {
            background-color: #d9d9d9;
            cursor: not-allowed;
        }
        .login-link {
            text-align: center;
            margin-top: 15px;
        }
        .login-link a {
            color: #007bff;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="registerForm" runat="server">
        <div class="register-container">
            <h1>Create Your Account to Get Started!</h1>

            <label for="txtUsername">Username:</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Enter your username"></asp:TextBox>
            <span id="usernameError" class="error">Username must start with a letter and be 4-24 characters long.</span>

            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your email"></asp:TextBox>
            <span id="emailError" class="error">Please enter a valid email address.</span>

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter your password"></asp:TextBox>
            <span id="passwordError" class="error">Password must be 8-24 characters long and include uppercase, lowercase, a number, and a special character.</span>

            <label for="txtConfirmPassword">Confirm Password:</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Confirm your password"></asp:TextBox>
            <span id="confirmPasswordError" class="error">Passwords must match.</span>

             <label for="txtBirthday">Birthday:</label>
             <asp:TextBox ID="txtBirthday" runat="server" TextMode="Date" CssClass="input-box"></asp:TextBox>


            <label for="txtAboutYourself">Tell us about yourself!</label>
            <asp:TextBox ID="txtAboutYourself" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4" Placeholder="Write something about yourself..."></asp:TextBox>

            <asp:Button ID="btnSignUp" runat="server" CssClass="submit-button" Text="Sign Up" OnClick="btnSignUp_Click" Enabled="false" />

            <div class="login-link">
                Already registered? <a href="/Login.aspx">Sign In</a>
            </div>
        </div>
    </form>

    <script>
        const usernameInput = document.getElementById("txtUsername");
        const emailInput = document.getElementById("txtEmail");
        const passwordInput = document.getElementById("txtPassword");
        const confirmPasswordInput = document.getElementById("txtConfirmPassword");
        const signUpButton = document.getElementById("btnSignUp");

        const usernameError = document.getElementById("usernameError");
        const emailError = document.getElementById("emailError");
        const passwordError = document.getElementById("passwordError");
        const confirmPasswordError = document.getElementById("confirmPasswordError");

        function validateUsername() {
            const value = usernameInput.value;
            const isValid = /^[a-zA-Z][a-zA-Z0-9_-]{3,23}$/.test(value);
            usernameError.style.display = isValid ? "none" : "block";
            return isValid;
        }

        function validateEmail() {
            const value = emailInput.value;
            const isValid = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
            emailError.style.display = isValid ? "none" : "block";
            return isValid;
        }

        function validatePassword() {
            const value = passwordInput.value;
            const isValid = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,24}$/.test(value);
            passwordError.style.display = isValid ? "none" : "block";
            return isValid;
        }

        function validateConfirmPassword() {
            const isValid = passwordInput.value === confirmPasswordInput.value;
            confirmPasswordError.style.display = isValid ? "none" : "block";
            return isValid;
        }

        function toggleSignUpButton() {
            const isFormValid =
                validateUsername() &&
                validateEmail() &&
                validatePassword() &&
                validateConfirmPassword();
            signUpButton.disabled = !isFormValid;
        }

        usernameInput.addEventListener("input", toggleSignUpButton);
        emailInput.addEventListener("input", toggleSignUpButton);
        passwordInput.addEventListener("input", toggleSignUpButton);
        confirmPasswordInput.addEventListener("input", toggleSignUpButton);
    </script>
</body>
</html>
