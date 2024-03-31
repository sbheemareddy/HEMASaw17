<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HEMASaw.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .login-container {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
    }
    body {
 
    min-height: 100vh;

}
</style>

<div class="login-container">
    <h2>Login</h2>
    <asp:TextBox ID="txtEmployeeID" runat="server" placeholder="Employee ID" CssClass="form-control"></asp:TextBox>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
</div>
</asp:Content>
