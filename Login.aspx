<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HEMASaw.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<%--<div class="content">
    <h2>Login</h2>
    <asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="txtEmployeeID"></asp:Label>
    <asp:TextBox ID="txtEmployeeID" runat="server" placeholder="Employee ID" CssClass="form-control"></asp:TextBox>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
</div>--%>

    <div class="content">
        <h2>Login</h2>
        <div class="form-group">
            <asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="txtEmployeeID"></asp:Label>
            <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control" placeholder="Enter your username"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtEmployeeID" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
        <div class="form-group">
            <asp:HyperLink ID="lnkChangePassword" runat="server" NavigateUrl="~/ChangePassword.aspx" Text="Change Password"></asp:HyperLink>
        </div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
    </div>
</asp:Content>
