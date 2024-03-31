<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WOPage.aspx.cs" Inherits="HEMASaw.WO.WOPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="buttons">
            <asp:Button ID="btnQRCodeScan" runat="server" CausesValidation="false" Text="QR Code Scan" CssClass="btn" OnClick="btnQRCodeScan_Click" />
            <asp:Button ID="btnClearData" runat="server" CausesValidation="false" Text="Clear Data" CssClass="btn" OnClick="btnClearData_Click" />
            <asp:Button ID="btnAcceptData" runat="server" CausesValidation="true" Text="Accept Data" CssClass="btn" OnClick="btnAcceptData_Click" />
            <asp:Button ID="btnSearchWO" runat="server" CausesValidation="false" Text="Search" CssClass="btn" OnClick="btnSearchWO_Click" />
        </div>

        <div class="container">
            <div class="card">
                <h6>Work Order</h6>
                <div class="field">
                    <label class="fixed-size-label">WO#</label>
                    <asp:TextBox ID="txtWO" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Saw#</label>
                    <asp:TextBox ID="txtSaw" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Expander#</label>
                    <select class="fixed-size-Select">
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                </div>
                <div class="field">
                    <label for="Date" class="fixed-size-label">QR Date</label>
                    <asp:TextBox ID="txtQRCodeDate" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
            </div>
            <div class="card">
                <h6>Part ID</h6>
                <div class="field">
                    <label class="fixed-size-label">Material</label>
                    <asp:TextBox ID="txtMaterial" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Description</label>
                </div>
                <div class="field">
                    <asp:TextBox ID="txtDescription" class="fixed-size-input-Readonly-long" ReadOnly="true" runat="server"
                        TextMode="MultiLine" Rows="1" Style="resize: vertical;" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Legacy Part</label>
                </div>
                <div class="field">
                    <asp:TextBox ID="txtLegacyPart" class="fixed-size-input-Readonly-long" ReadOnly="true" runat="server"
                        TextMode="MultiLine" Rows="1" Style="resize: vertical;" />
                </div>
            </div>
            <div class="card">
                <h6>Block Batch#</h6>
                <div class="field">
                    <label class="fixed-size-label">Block Batch#</label>
                    <asp:TextBox ID="txtBlockBatch" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Cut Slice#</label>
                    <asp:TextBox ID="txtCutSlice" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Slice Batch#</label>
                    <asp:TextBox ID="txtSliceBatch" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Comments</label>
                </div>
                <div class="field">
                    <asp:TextBox ID="txtComments" class="fixed-size-input-long" runat="server" />
                </div>
            </div>
            <div class="card">
                <h6>Thickness</h6>
                <div class="field">
                    <label class="fixed-size-label">Min</label>
                    <asp:TextBox ID="txtMin" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Max</label>
                    <asp:TextBox ID="txtMax" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Ave</label>
                    <asp:TextBox ID="txtAve" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
                </div>
            </div>
            <div class="card">
                <h6>Dimension</h6>
                <div class="field">
                    <label class="fixed-size-label">Length#</label>
                    <asp:TextBox ID="txtLength" CssClass="fixed-size-input" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLength" ControlToValidate="txtLength" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revLength" ControlToValidate="txtLength" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="field">
                    <label class="fixed-size-label">Width</label>
                    <asp:TextBox ID="txtWidth" CssClass="fixed-size-input" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvWidth" ControlToValidate="txtWidth" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revWidth" ControlToValidate="txtWidth" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="field">
                    <label class="fixed-size-label">Weight</label>
                    <asp:TextBox ID="txtWeight" CssClass="fixed-size-input" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvWeight" ControlToValidate="txtWeight" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revWeight" ControlToValidate="txtWeight" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="card">
                <h6>Density</h6>
                <div class="field">
                    <label class="fixed-size-label">Target Density</label>
                    <asp:TextBox ID="txtTargetDensity" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Density Tol</label>
                    <asp:TextBox ID="txtDensityTol" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label-long">Actual Density: Density PCF (Calculate)</label>

                </div>
                <div class="field">
                    <label for="pcfcalculated" id="lblPCFCalculated" runat="server"></label>
                </div>
            </div>
        </div>

        <div id="divpass" class="catchy-green" runat="server" visible="false">
            <div class="buttons">
                <h2>Acceptance Passed.</h2>
                <asp:Button ID="btnPrintSliceLabel" runat="server" Text="Print Slice Tag" CssClass="btn" OnClientClick="redirectToSliceLabel(); return false;" />
                <asp:Button ID="btnPrintSummaryLabel" runat="server" Text="Print Summary Tag" CssClass="btn" OnClientClick="redirectToSummaryLabel(); return false;" />
            </div>
        </div>
        <div id="divfail" class="catchy-red" visible="false" runat="server">
            <h2>Acceptance Failed</h2>
        </div>
    </div>
<%--    <style>
        .error-message {
            color: red;
        }
    </style>--%>
    <script type="text/javascript">
        function redirectToSliceLabel() {
            window.open('/report/SliceLabelReport.aspx?reportName=SliceLabel', '_blank');
        }

        function redirectToSummaryLabel() {
            window.open('/report/SliceLabelReport.aspx?reportName=SummaryLabel', '_blank');
        }
    </script>
</asp:Content>
