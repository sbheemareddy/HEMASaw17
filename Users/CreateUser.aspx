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
            <td style="width:15px;">
                <asp:TextBox ID="txtTermDate" class="fixed-size-input-long" type="date" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Role</td>
            <td style="td-spacing">
                <asp:DropDownList ID="ddlEmployeeRole" runat="server">
                    <asp:ListItem Text="Operator" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Supervisor" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="changePassword" style="display: none;" ClientIDMode="Static" runat="server">
            <td>Change Password</td>
            <td >
                <asp:CheckBox ID="chkChangePassword" runat="server" onchange="togglePasswordVisibility()"></asp:CheckBox>
            </td>
        </tr>
        <tr id="passwordRow" style="display: none;" ClientIDMode="Static" runat="server">
            <td>Password</td>
            <td style="td-spacing">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="fixed-size-input-long" placeholder="Enter your new password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
               <%-- <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password must be at least 8 characters long and contain at least one letter and one number" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" CssClass="text-danger"></asp:RegularExpressionValidator>--%>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div class="buttons">
                <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click"
                    ValidationGroup="SaveValidationGroup" />
                <asp:Button ID="btnBack" runat="server" CssClass="btn" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                </div>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
        function togglePasswordVisibility() {
            var passwordRow = document.getElementById("passwordRow");
            var chkChangePassword = document.getElementById("<%= chkChangePassword.ClientID %>");

            if (chkChangePassword.checked) {
                passwordRow.style.display = "table-row";
            } else {
                passwordRow.style.display = "none";
            }
        }
    </script>
</asp:Content>
