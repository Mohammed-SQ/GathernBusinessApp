<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="Lab2_CIT216.Feedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCC Feedback Page</title>
    <h1>DCC Feedback Page</h1>
    <p>Enter User ID and Message</p>
    <p>___________________________________________</p>
</head>
<body>
    <form id="form2" runat="server" action="ThankPage.aspx" method="post">
        <p>User ID: </p>
        <div>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
        <br />
        <p>Message: </p>
        <div>
            <textarea id="TextArea2" cols="20" rows="2"></textarea>
        </div>
        <br />
        <div>
            <asp:Button ID="Button2" runat="server" Text="Submit" />
        </div>
    </form>
</body>
</html>
