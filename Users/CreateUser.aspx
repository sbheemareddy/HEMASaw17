<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="HEMASaw.Users.CreateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <h1>
        <asp:Literal ID="litTitle" runat="server"></asp:Literal></h1>
    <table>
        <tr>
            <td>Employee ID:</td>
            <td>
                <asp:TextBox ID="txtEmployeeID" runat="server"></asp:TextBox></td>
                <asp:RequiredFieldValidator ID="rfvEmployeeID" runat="server" ControlToValidate="txtEmployeeID" ErrorMessage="Employee ID is required." ValidationGroup="SaveValidationGroup"></asp:RequiredFieldValidator>
        </tr>
        <tr>
            <td>First Name:</td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required." ValidationGroup="SaveValidationGroup"></asp:RequiredFieldValidator>
         </tr>
        <tr>
            <td>Last Name:</td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required." ValidationGroup="SaveValidationGroup"></asp:RequiredFieldValidator>
        </tr>
        <tr>
            <td>Active:</td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server"></asp:CheckBox></td>
        </tr>
        <tr>
            <td>Term Date:</td>
            <td>
                <asp:TextBox ID="txtTermDate" type="date" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Employee Role:</td>
            <td>
                <asp:DropDownList ID="ddlEmployeeRole" runat="server">
                    <asp:ListItem Text="Operator" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Supervisor" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="SaveValidationGroup" />
            </td>
        </tr>
    </table>
</div>
</asp:Content>
