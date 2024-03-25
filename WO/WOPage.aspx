<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WOPage.aspx.cs" Inherits="HEMASaw.WO.WOPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
    <div class="buttons">
        <asp:Button ID="btnQRCodeScan" runat="server" Text="QR Code Scan" CssClass="btn" OnClick="btnQRCodeScan_Click" />
        <asp:Button ID="btnClearData" runat="server" Text="Clear Data" CssClass="btn" onclick="btnClearData_Click"/>
        <asp:Button ID="btnAcceptData" runat="server" Text="Accept Data" CssClass="btn" OnClick="btnAcceptData_Click" />
        <asp:Button ID="btnKeyinWO" runat="server" Text="Search" CssClass="btn" OnClick="btnKeyinWO_Click" />
    </div>
    
    <div class="container">

        <div class="card">
            <h6>Work Order</h6>
            <div class="field"> 
                <label class="fixed-size-label">WO#</label>
                <asp:TextBox  ID="txtWO" class="fixed-size-input" runat="server" />
            </div>
             <div class="field">                            
                 <label class="fixed-size-label">Saw#</label>
                 <asp:TextBox ID="txtSaw" ReadOnly="true" class="fixed-size-input-Readonly"   runat="server" />
             </div>
             <div class="field">                            
                 <label class="fixed-size-label">Expander#</label>
                 <select>
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
                <asp:TextBox ID="txtDescription" class="fixed-size-input-Readonly-long" ReadOnly="true" runat="server" />
            </div>
            <div class="field"> 
                <label class="fixed-size-label">Legacy Part</label>
            </div>
            <div class="field"> 
                <asp:TextBox ID="txtLegacyPart" class="fixed-size-input-Readonly-long" ReadOnly="true"  runat="server" />
            </div>
<%--            <div class="field"> 
                <label for="empid">Emp ID</label>
                <label for="empid" id="lblEmpID" runat="server">Emp ID</label>
            </div>
            <div class="field"> 
                <label for="empname">Emp Name</label>
                <label for="empname" id="lblEmpName" runat="server">Emp Name</label>
            </div>--%>
        </div>

        <div class="card">
            <h6>Block Batch#</h6>
            <div class="field"> 
                <label class="fixed-size-label">Block Batch#</label>
                <asp:TextBox ID="txtBlockBatch" class="fixed-size-input" ReadOnly="true" runat="server" />
            </div>
            <div class="field"> 
                <label class="fixed-size-label">Cut Slice#</label>
                <asp:TextBox ID="txtCutSlice" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
            </div>
             <div class="field"> 
                <label class="fixed-size-label">Slice Batch#</label>
                 <asp:TextBox ID="txtSliceBatch" class="fixed-size-input" ReadOnly="true" runat="server" />
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
            <div class="field"><label class="fixed-size-label">Min</label>
                <asp:TextBox ID="txtMin" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
            </div>
            <div class="field"><label class="fixed-size-label">Max</label>
                 <asp:TextBox ID="txtMax" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
            </div>
            <div class="field"><label class="fixed-size-label">Ave</label>
                <asp:TextBox ID="txtAve" class="fixed-size-input-Readonly" ReadOnly="true" runat="server" />
            </div>
        </div>

        <div class="card">
            <h6>Dimension</h6>
            <div class="field">
                 <label class="fixed-size-label">Length#</label>
                <asp:TextBox for="length" ID="txtLength" class="fixed-size-input" runat="server" />
            </div>
            <div class="field">
                 <label class="fixed-size-label">Width</label>
                <asp:TextBox for="width" ID="txtWidth" class="fixed-size-input" runat="server" />
            </div>
            <div class="field">
                <label class="fixed-size-label">Weight</label>
                <asp:TextBox for="weight" ID="txtWeight" class="fixed-size-input" runat="server" />
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

        <div id="divpass" class="catchy-green" visible="false" runat="server">
            <div class="buttons">
                <h2>Acceptance Passed.</h2>
                <asp:Button ID="btnPrintSliceLabel" runat="server" Text="Print Slice Tag" CssClass="btn" OnClick="btnPrintSliceLabel_Click" />
                <asp:Button ID="btnPrintSummaryLabel" runat="server" Text="Print Summary Tag" CssClass="btn" OnClick="btnPrintSummaryLabel_Click" />
            </div>
        </div>

    <div id="divfail" class="catchy-red" visible="false" runat="server">
            <h2>Acceptance Failed</h2> 
   </div>

    </div>
</asp:Content>