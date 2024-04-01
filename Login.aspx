<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HEMASaw.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="content">
    <h2>Login</h2>
    <asp:TextBox ID="txtEmployeeID" runat="server" placeholder="Employee ID" CssClass="form-control"></asp:TextBox>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
</div>
</asp:Content>
