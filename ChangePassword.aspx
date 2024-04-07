<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HEMASaw.ChangePassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card-changepassword ">
                    <div class="card-body">
                        <h2 class="card-title text-center">Change Password</h2>
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                         <div class="form-group">
                            <asp:Label ID="lblEmployeeId"  runat="server" Text="Employee ID" AssociatedControlID="txtCurrentPassword"></asp:Label>
                            <asp:TextBox ID="txtEmployeeId" runat="server"  CssClass="form-control" placeholder="Enter your Employee ID"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmployeeId" runat="server" ControlToValidate="txtEmployeeId" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblCurrentPassword"  runat="server" Text="Current Password" AssociatedControlID="txtCurrentPassword"></asp:Label>
                            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your current password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ControlToValidate="txtCurrentPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblNewPassword" runat="server" Text="New Password" AssociatedControlID="txtNewPassword"></asp:Label>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your new password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="Password must be at least 8 characters long and contain at least one letter and one number" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" CssClass="text-danger"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblConfirmNewPassword" runat="server" Text="Confirm New Password" AssociatedControlID="txtConfirmNewPassword"></asp:Label>
                            <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm your new password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmNewPassword" runat="server" ControlToValidate="txtConfirmNewPassword" ErrorMessage="*" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvConfirmNewPassword" runat="server" ControlToValidate="txtConfirmNewPassword" ControlToCompare="txtNewPassword" ErrorMessage="New Password and Confirm New Password must match" CssClass="text-danger"></asp:CompareValidator>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" />
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
