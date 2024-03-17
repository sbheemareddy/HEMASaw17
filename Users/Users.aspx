<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HEMASaw.Users.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1>User Management</h1>
        <div class="table-responsive">
            <asp:GridView ID="GridViewUsers" runat="server" CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False" DataKeyNames="EmployeeID" 
                OnRowEditing="GridViewUsers_RowEditing" OnRowDeleting="GridViewUsers_RowDeleting"
                AllowPaging="True" PageSize="10" PagerStyle-HorizontalAlign="Center"
                OnPageIndexChanging="GridViewUsers_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Active" HeaderText="Active" />
                    <asp:BoundField DataField="TermDate" HeaderText="Term Date" DataFormatString="{0:yyyy-MM-dd}" />
                     <asp:BoundField DataField="employeeRoleDesc" HeaderText="Role" />
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnCreateUser" runat="server" Text="Create User" OnClick="btnCreateUser_Click" CssClass="btn btn-primary" />
        </div>
        </div>
</asp:Content>
