<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HEMASaw.ChangePassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-label {
            min-width: 150px;
            /*font-weight: bold;*/
         /*   font-size: 1.5rem;*/
        }
        .form-control {
            min-width: 300px; /* Adjust as needed to prevent wrapping */
            font-size: 1.25rem; /* Increase font size for better readability */
        }
    </style>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-8"> <!-- Increased width for better layout -->
                <div class="">
                    <div class="card-body">
                      <%--  <h2 class="card-title text-center">Change Password</h2>--%>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                        <div class="form-group">
                            <asp:Label ID="lblEmployeeId" runat="server" Text="Employee ID" AssociatedControlID="txtEmployeeId" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control" placeholder="Enter your Employee ID"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmployeeId" runat="server" ControlToValidate="txtEmployeeId" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblCurrentPassword" runat="server" Text="Current Password" AssociatedControlID="txtCurrentPassword" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your current password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ControlToValidate="txtCurrentPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblNewPassword" runat="server" Text="New Password" AssociatedControlID="txtNewPassword" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your new password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblConfirmNewPassword" runat="server" Text="Confirm Password" AssociatedControlID="txtConfirmNewPassword" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm your new password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmNewPassword" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" CausesValidation="false" />
                            </div>
                            <div class="col text-center">
                                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
