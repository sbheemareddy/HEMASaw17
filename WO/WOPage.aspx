<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WOPage.aspx.cs" Inherits="HEMASaw.WO.WOPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="buttons">
            <asp:Button ID="btnQRCodeScan" runat="server" CausesValidation="false" Text="QR Code Scan" CssClass="btn" OnClick="btnQRCodeScan_Click" />
            <asp:Button ID="btnClearData" runat="server" CausesValidation="false" Text="Clear Data" CssClass="btn" OnClick="btnClearData_Click" />
            <asp:Button ID="btnAcceptData" runat="server" ClientIDMode="Static" CausesValidation="true" Text="Accept Data" CssClass="btn" OnClientClick="return showModal();acceptClick();" OnClick="btnAcceptData_Click"  />
            <asp:Button ID="btnCheckDensity" runat="server" CausesValidation="true" Text="Check Density" CssClass="btn" OnClick="btnCheckDensity_Click" />
<%--            <asp:Button ID="btnSearchWO" runat="server" CausesValidation="false" Text="Search" CssClass="btn" OnClick="btnSearchWO_Click" />--%>
            <div id="divpass"  runat="server" visible="false">
                <asp:Button ID="btnPrintSliceLabel" runat="server" Text="Print Slice Tag" CssClass="btn" OnClientClick="redirectToSliceLabel(); return false;" />
                <asp:Button ID="btnPrintSummaryLabel" runat="server" Text="Print Summary Tag" CssClass="btn" OnClientClick="redirectToSummaryLabel(); return false;" />
            </div>
        </div>

        <div class="buttons">
            <asp:TextBox ID="txtTargetDensity"   runat="server" Visible="false" />
            <asp:TextBox ID="txtDensityTol" runat="server" Visible="false" />
            <asp:TextBox ID="txtTargetCellCount"  runat="server" Visible="false" />
            <asp:TextBox ID="txtMinCellCount"   runat="server" Visible="false" />
            <asp:TextBox ID="txtMaxCellCount"   runat="server" Visible="false" />
            <asp:HiddenField runat="server" ID="hidDataChanged" ClientIDMode="Static" />
         </div>
        <!-- Popup Modal -->
        <div id="myModal" class="modal">
          <div class="modal-content">
            <span id="closeButton" class="close" onclick="closeModal()">&times;</span>
            <p id="popupMessage">Are you sure you want to change the length/width/weight?</p>
             <asp:Button ID="acceptButton" runat="server"  Text="Accept" CssClass="btn" Style="margin-bottom: 20px; background-color: rgb(15, 161, 15);"  OnClick="btnAcceptData_Click"  />
              <asp:Button ID="cancelButton" CausesValidation="false" runat="server"  Text="Cancel" CssClass="btn" Style="background-color: rgb(15, 161, 15);" />
          </div>
        </div>
        <div class="container">
            <div class="card">
                <h6>WO Details</h6>
                <div class="field">
                    <label class="fixed-size-label">Work Order</label>
                    <asp:TextBox ID="txtWO" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Saw Number</label>
                    <asp:TextBox ID="txtSaw" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
                <div class="field">
                    <label class="fixed-size-label">Expander</label>
                        <asp:DropDownList ID="ddlOptions" runat="server" CssClass="fixed-size-Select">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                        </asp:DropDownList>
                </div>
                <div class="field">
                    <label for="Date" class="fixed-size-label">QR Date</label>
                    <asp:TextBox ID="txtQRCodeDate" ReadOnly="true" class="fixed-size-input-Readonly" runat="server" />
                </div>
            </div>
            <div class="card">
                <h6>Part Details</h6>
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
                <h6>Block Details</h6>
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
                    <label class="fixed-size-label">Length</label>
                    <asp:TextBox ID="txtLength" CssClass="fixed-size-input" runat="server" OnTextChanged="txtLength_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLength" ControlToValidate="txtLength" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revLength" ControlToValidate="txtLength" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="field">
                    <label class="fixed-size-label">Width</label>
                    <asp:TextBox ID="txtWidth" CssClass="fixed-size-input" runat="server" OnTextChanged="txtWidth_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvWidth" ControlToValidate="txtWidth" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revWidth" ControlToValidate="txtWidth" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="field">
                    <label class="fixed-size-label">Weight</label>
                    <asp:TextBox ID="txtWeight" CssClass="fixed-size-input" runat="server" OnTextChanged="txtWeight_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvWeight" ControlToValidate="txtWeight" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revWeight" ControlToValidate="txtWeight" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
                <div class="field">
                    <label class="fixed-size-label">Cell Count</label>
                    <asp:TextBox ID="txtCellCount" CssClass="fixed-size-input" runat="server"></asp:TextBox>
                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCellCount" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtCellCount" runat="server"
                        ErrorMessage="<span class='error-message'>*</span>" ValidationExpression="^\d+(\.\d+)?$" Display="Dynamic"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="card" id ="densityDiv" runat="server" >
                <h6>Density</h6>
                <div class="field">
                    <label class="fixed-size-label-long" id="lblTargetDensity" runat="server">Target Density : Density Tol :</label>
                </div>
                <div class="field">
                    <label class="fixed-size-label-long"  id="lblTgtCellCount" runat="server">Target Cell Count : Min : Max : </label>
                </div>
                <div class="field">
                    <label class="fixed-size-label">Actual Density</label>
                    <label class="fixed-size-input-Readonly" id="lblPCFCalculated" runat="server"></label>
                </div>
                <%--<div class="field">
                    <h4><label class="fixed-size-label-long" style="font-weight:900" id="lblAcceptanceMsg" runat="server">Acceptance Passed</label></h4>
                </div>--%> 
                 <h4 style="padding-left:50px"><label class="fixed-size-label-long" style="font-weight:900" id="lblAcceptanceMsg" runat="server">Acceptance Passed</label></h4>
            </div>
        </div>
        <div id="divfail" class="catchy-red" visible="false" runat="server">
            <h2>Acceptance Failed</h2>
        </div>

        <div class="buttons">
            <asp:Button ID="btnFirst" runat="server" Enabled="false" CausesValidation="false" Text="First" CssClass="btn" OnClick="btnFirst_Click" />
            <asp:Button ID="btnPrevious" runat="server" Enabled="false" CausesValidation="false" Text="Previous" CssClass="btn" OnClick="btnPrevious_Click" />
            <asp:Button ID="btnNext" runat="server" Enabled="false" ClientIDMode="Static" CausesValidation="false" Text="Next" CssClass="btn" OnClientClick="return OpenModalPopUp();" OnClick="btnNext_Click"  />
            <asp:Button ID="btnLast" LastSliceNum="1" runat="server" Enabled="false" CausesValidation="false" Text="Last" CssClass="btn" OnClick="btnLast_Click" />
        </div>

    </div>
    <script type="text/javascript">
        function redirectToSliceLabel() {
            window.open('/report/SliceLabelReport.aspx?reportName=SliceLabel', '_blank');
        }

        function redirectToSummaryLabel() {
            window.open('/report/SliceLabelReport.aspx?reportName=SummaryLabel', '_blank');
        }

        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // Get modal element and buttons
        var modal = document.getElementById('myModal');
        var acceptButton = document.getElementById('acceptButton');
        var cancelButton = document.getElementById('cancelButton');
        var closeButton = document.getElementsByClassName("close")[0];

        // Function to show the modal
        function showModal() {
            var bDataChanged = document.getElementById('hidDataChanged').value;
             if (bDataChanged == '1') {
                event.preventDefault(); // Prevent default form submission
                // Show the modal
                modal.style.display = "block";
                }
            if (bDataChanged == '0') {
                return true;
            }
            return true;
        }

        // Function to close the modal
        function closeModal() {
            modal.style.display = 'none';
}

        // Function to handle accept button click
        function handleAccept() {
            document.getElementById('hidDataChanged').value = 0;
            // Get the reference to the button
            closeModal(); // Close the modal
            acceptClick();
           
        }

        // Function to handle cancel button click
        function handleCancel() {
            console.log('Cancelled');
        // Add your custom code here for the cancel action
        closeModal(); // Close the modal
}

        // Add event listeners for button clicks
        acceptButton.addEventListener('click', handleAccept);
        cancelButton.addEventListener('click', handleCancel);
        closeButton.addEventListener('click', closeModal);

    </script>
</asp:Content>
