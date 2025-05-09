<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="TaskManager.Tasks" MasterPageFile="TaskManager.master" %>
<asp:Content ID="TasksContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Tasks</h1>
    <asp:GridView ID="TaskGrid" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="TaskGrid_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Deadline" HeaderText="Deadline" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
    </asp:GridView>
</asp:Content>
