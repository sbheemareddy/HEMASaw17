<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HEMASaw.Users.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
         <div class="buttons">
             <div class="page-header">Manage Users</div>
            <asp:Button ID="btnQRCodeScan" runat="server" CausesValidation="false" Text="QR Code Scan" CssClass="btn" OnClick="btnQRCodeScan_Click" />
            <asp:Button ID="btnCreateUser" runat="server" Text="Create User" OnClick="btnCreateUser_Click" CssClass="btn btn-primary" />        
         </div>
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
        </div>
        </div>
</asp:Content>
