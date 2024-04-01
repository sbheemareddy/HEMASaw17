<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HEMASaw._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div style="margin-top: 10px; min-height: 75vh;">
            <h2>Search Workorder</h2>
            <div class="row">
                <div class="col-md-3">
                    <label for="txtWorkOrder">QR Scan</label>
                    <asp:TextBox ID="txtQRScanData" runat="server" ClientIDMode="Static" OnTextChanged="txtQRScanData_TextChanged" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label for="txtWorkOrder">Work Order #</label>
                    <asp:TextBox ID="txtWorkOrder" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvWorkOrder" ControlToValidate="txtWorkOrder" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revWorkOrder" ControlToValidate="txtWorkOrder" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-3">
                    <label for="txtSliceBatch">Slice Order #</label>
                    <asp:TextBox ID="txtSliceBatch" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label for="txtBlockBatch">Block Batch #</label>
                    <asp:TextBox ID="txtBlockBatch" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div class="row mt-3">
                <div class="col-md-12">
                    <asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" 
                        AllowPaging="True" PageSize="5" PagerStyle-HorizontalAlign="Center" 
                        OnPageIndexChanging="gvSearchResults_PageIndexChanging" OnRowCommand="gvSearchResults_RowCommand" 
                        OnDataBound="gvSearchResults_DataBound" DataKeyNames ="Workorder, Slice_Batch, Block_Batch, SliceNum,ID">
                        <Columns>
                            <asp:BoundField DataField="Material" HeaderText="Material" />
                            <asp:BoundField DataField="Slice_Batch" HeaderText="Slice Order" />
                            <asp:BoundField DataField="VisualPartID" HeaderText="VisualPartID" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="SliceNum" HeaderText="SliceNum" />
                            <asp:BoundField DataField="Thickness" HeaderText="Thickness" />
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
<%--                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
                        <PagerStyle CssClass="pagination justify-content-center" />--%>
                    </asp:GridView>
             </div>
            </div>
        </div>
    </div>
    <style>
        body {
            display: flex;
            flex-direction: column;
            min-height: 100vh;
        }

        .container {
            flex: 1;
        }

        .error-message {
            color: red;
        }
    </style>
     <script>
        document.getElementById('txtQRScanData').addEventListener('input', function (event) {
            // This function will be called whenever the value of the textbox changes
            var scannedData = event.target.value;
            console.log("Scanned data: " + scannedData);
            __doPostBack('<%= txtQRScanData.ClientID %>', '');
               // Trigger postback
               // You can add further processing logic here, such as triggering an AJAX request81572011640
           });
    </script>
</asp:Content>
