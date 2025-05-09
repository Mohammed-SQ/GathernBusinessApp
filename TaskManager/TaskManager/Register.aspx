<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TaskManager.Register" %>
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
            text-align: center;
            color: #4a4a4a;
        }
        label {
            display: block;
            margin-bottom: 8px;
            font-weight: bold;
            position: relative;
        }
        .validation-icon {
            position: absolute;
            right: -25px;
            top: 0;
        }
        input, textarea {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .black-box {
            background-color: black;
            color: white;
            padding: 5px;
            border-radius: 5px;
            font-size: 12px;
            display: none;
            margin-top: -8px;
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
            <h1>Welcome to Study Buddy!</h1>
            <label for="txtUsername">
                Username: <span id="usernameIcon" class="validation-icon"></span>
            </label>
            <input type="text" id="txtUsername" placeholder="Enter your username" />
            <div id="usernameRequirement" class="black-box">
                4 to 24 characters. Must begin with a letter. Letters, numbers, underscores, hyphens allowed.
            </div>

            <label for="txtEmail">
                Email: <span id="emailIcon" class="validation-icon"></span>
            </label>
            <input type="text" id="txtEmail" placeholder="Enter your email" />
            <div id="emailRequirement" class="black-box">Please enter a valid email.</div>

            <label for="txtPassword">
                Password: <span id="passwordIcon" class="validation-icon"></span>
            </label>
            <input type="password" id="txtPassword" placeholder="Enter your password" />
            <div id="passwordRequirement" class="black-box">
                8 to 24 characters. Must include uppercase, lowercase, a number, and a special character.
            </div>

            <label for="txtConfirmPassword">
                Confirm Password: <span id="confirmPasswordIcon" class="validation-icon"></span>
            </label>
            <input type="password" id="txtConfirmPassword" placeholder="Confirm your password" />
            <div id="confirmPasswordRequirement" class="black-box">
                Must match the first password input field.
            </div>

            <label for="txtBirthday">Birthday:</label>
            <input type="date" id="txtBirthday" placeholder="mm/dd/yyyy" />

            <label for="txtAboutYourself">Tell us about yourself!</label>
            <textarea id="txtAboutYourself" rows="4" placeholder="Write something about yourself..."></textarea>

            <button id="btnSignUp" class="submit-button" disabled>Sign Up</button>

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

        const usernameIcon = document.getElementById("usernameIcon");
        const emailIcon = document.getElementById("emailIcon");
        const passwordIcon = document.getElementById("passwordIcon");
        const confirmPasswordIcon = document.getElementById("confirmPasswordIcon");

        const usernameRequirement = document.getElementById("usernameRequirement");
        const emailRequirement = document.getElementById("emailRequirement");
        const passwordRequirement = document.getElementById("passwordRequirement");
        const confirmPasswordRequirement = document.getElementById("confirmPasswordRequirement");

        function validateUsername() {
            const value = usernameInput.value;
            const isValid = /^[a-zA-Z][a-zA-Z0-9_-]{3,23}$/.test(value);
            usernameIcon.textContent = isValid ? "✔" : "✖";
            usernameIcon.style.color = isValid ? "green" : "red";
            usernameRequirement.style.display = isValid ? "none" : "block";
            return isValid;
        }

        function validateEmail() {
            const value = emailInput.value;
            const isValid = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
            emailIcon.textContent = isValid ? "✔" : "✖";
            emailIcon.style.color = isValid ? "green" : "red";
            emailRequirement.style.display = isValid ? "none" : "block";
            return isValid;
        }

        function validatePassword() {
            const value = passwordInput.value;
            const isValid = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,24}$/.test(value);
            passwordIcon.textContent = isValid ? "✔" : "✖";
            passwordIcon.style.color = isValid ? "green" : "red";
            passwordRequirement.style.display = isValid ? "none" : "block";
            return isValid;
        }

        function validateConfirmPassword() {
            const isValid = passwordInput.value === confirmPasswordInput.value;
            confirmPasswordIcon.textContent = isValid ? "✔" : "✖";
            confirmPasswordIcon.style.color = isValid ? "green" : "red";
            confirmPasswordRequirement.style.display = isValid ? "none" : "block";
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

        usernameInput.addEventListener("input", () => {
            validateUsername();
            toggleSignUpButton();
        });

        emailInput.addEventListener("input", () => {
            validateEmail();
            toggleSignUpButton();
        });

        passwordInput.addEventListener("input", () => {
            validatePassword();
            toggleSignUpButton();
        });

        confirmPasswordInput.addEventListener("input", () => {
            validateConfirmPassword();
            toggleSignUpButton();
        });
    </script>
</body>
</html>
