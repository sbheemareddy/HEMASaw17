<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="HEMASaw.Users.CreateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="centered-content">
    <table>
        <tr>
            <td></td>
            <td>
                <h1>
                    <asp:Literal ID="litTitle" runat="server"></asp:Literal>
                </h1>
            </td>
        </tr>
        <tr>
            <td>Employee ID</td>
            <td style="td-spacing">
                <asp:TextBox ID="txtEmployeeID" class="fixed-size-input-long" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmployeeID" runat="server" ControlToValidate="txtEmployeeID"
                    ErrorMessage="Employee ID is required.*" ValidationGroup="SaveValidationGroup" ForeColor="Red"
                    Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>First Name</td>
            <td style="td-spacing">
                <asp:TextBox ID="txtFirstName" class="fixed-size-input-long" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="First Name is required.*" ValidationGroup="SaveValidationGroup" ForeColor="Red"
                    Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td style="td-spacing">
                <asp:TextBox ID="txtLastName" class="fixed-size-input-long" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                    ErrorMessage="Last Name is required.*" ValidationGroup="SaveValidationGroup" ForeColor="Red"
                    Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Active</td>
            <td style="td-spacing">
                <asp:CheckBox ID="chkActive" runat="server" style="width: 20px;"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td>Term Date</td>
            <td style="padding-left: 15px;width:15px;">
                <asp:TextBox ID="txtTermDate" class="fixed-size-input-long" type="date" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Employee Role</td>
            <td style="td-spacing">
                <asp:DropDownList ID="ddlEmployeeRole" runat="server">
                    <asp:ListItem Text="Operator" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Supervisor" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div class="buttons">
                <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click"
                    ValidationGroup="SaveValidationGroup" />
                <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Back" OnClick="btnBack_Click" />
                </div>
            </td>
        </tr>
    </table>
</div>

</asp:Content>
