<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WOPage.aspx.cs" Inherits="HEMASaw.WO.WOPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="buttons">
        <asp:Button ID="btnQRCodeScan" runat="server" Text="QR Code Scan" CssClass="btn" OnClick="btnQRCodeScan_Click" />
        <asp:Button ID="btnClearData" runat="server" Text="Clear Data" CssClass="btn" onclick="btnClearData_Click"/>
        <asp:Button ID="btnAcceptData" runat="server" Text="Accept Data" CssClass="btn" OnClick="btnAcceptData_Click" />
        <asp:Button ID="btnKeyinWO" runat="server" Text="Keyin WO" CssClass="btn" OnClick="btnKeyinWO_Click" />
        <asp:Button ID="btnReprintTags" runat="server" Text="Reprint Tags" CssClass="btn" OnClick="btnReprintTags_Click" />
    </div>
    
    <div class="container">

        <div class="card">
            <h5>Work Order</h5>
            <div class="field"> 
                <label for="WO">WO# : </label>
                <asp:TextBox for="WO" ID="txtWO" class="fixed-size-input" runat="server" />
            </div>
             <div class="field">                            
                 <label for="EmpID">Saw# : </label>
                 <label for="partId" id="lblSaw" runat="server">Saw #</label>
             </div>
             <div class="field">                            
                 <label for="Expander">Expander# : </label>
                 <select>
                    <option value="2">2</option>
                    <option value="3">3</option>
                </select>
             </div>
             <div class="field">                            
                 <label for="Date" class="control-label">QR Code Date : </label>
                 <label for="Date" id="lblQRCodeDate" runat="server"></label>
             </div>
            <div class="field">
                <label for="Date" class="control-label">Date : </label>
                 <%= DateTime.Now.ToString("MM/dd/yyyy") %></div>
        </div>

        <div class="card">
            <h5>Part ID</h5>
            <div class="field"> 
                <label for="partId">PART ID : </label>
                <label for="partId" id="lblPartId" runat="server">PART ID</label>
            </div>
            <div class="field"> 
                <label for="Description">Description : </label>
                <label for="Description" id="lblDescription" runat="server">Description</label>
            </div>
            <div class="field"> 
                <label for="legacypart">Legacy Part : </label>
                <label for="legacypart" id="lblLegacyPart" runat="server">Legacy Part</label>
            </div>
            <div class="field"> 
                <label for="empid">Emp ID : </label>
                <label for="empid" id="lblEmpID" runat="server">Emp ID</label>
            </div>
            <div class="field"> 
                <label for="empname">Emp Name : </label>
                <label for="empname" id="lblEmpName" runat="server">Emp Name</label>
            </div>
        </div>

        <div class="card">
            <h5>Block Batch#</h5>
            <div class="field"> 
                <label for="blockbatch">Block Batch# : </label>
                <label for="blockbatch" id="lblBlockBatch" runat="server">From itemMaster table</label>
            </div>
            <div class="field"> 
                <label for="cutslice">Cut Slice# : </label>
                <label for="cutslice" id="lblCutSlice" runat="server">From itemMaster table</label>
            </div>
             <div class="field"> 
                <label for="sliceBatch">Slice Batch# : </label>
                <label for="sliceBatch" id="lblSliceBatch" runat="server">From itemMaster table</label>
            </div>
             <div class="field"> 
                <label for="sliceBatch">&nbsp;</label>
                <label for="sliceBatch" id="lblBlank" runat="server">&nbsp;</label>
            </div>
            <div class="field"> 
                <label for="Comments">Comments : </label>
                <asp:TextBox ID="txtComments" class="form-control" runat="server" />
            </div>
        </div>

        <div class="card">
            <h5>Thickness</h5>
            <div class="field"><label for="min">Min :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><label for="min" id="lblMin" runat="server"></label></div>
            <div class="field"><label for="max">Max :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><label for="max" id="lblMax" runat="server"></label></div>
            <div class="field"><label for="avg">Ave :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </label><label for="ave" id="lblAve" runat="server"></label></div>
        </div>

        <div class="card">
            <h5>Dimension</h5>
            <div class="field">
                 <label for="length">Length# : </label>
                <asp:TextBox for="length" ID="txtLength" class="fixed-size-input" runat="server" />
            </div>
            <div class="field">
                 <label for="width">Width : </label>
                <asp:TextBox for="width" ID="txtWidth" class="fixed-size-input" runat="server" />
            </div>
            <div class="field">
                <label for="weight">Weight : </label>
                <asp:TextBox for="weight" ID="txtWeight" class="fixed-size-input" runat="server" />
            </div>
        </div>

        <div class="card">
            <h5>Density</h5>
            <div class="field">
                <label for="targetdensity">Target Density : </label>
                <label for="targetdensity" id="lblTargetDensity" runat="server"></label>
            </div>
            <div class="field">
                <label for="densitytol">Density Tol : </label>
                <label for="densitytol" id="lblDensityTol" runat="server"></label>
            </div>
            <div class="field">
                 <label for="pcfcalculated">Actual Density: Density PCF (Calculate) : </label>
                <label for="pcfcalculated" id="lblPCFCalculated" runat="server"></label>
            </div>
        </div>

    </div>

    <div id="divpass" class="catchy-green" visible="false" runat="server">
              <h2>Acceptance Passed.</h2> 
   </div>

   <div id="divfail" class="catchy-red" visible="false" runat="server">
            <h2>Acceptance Failed</h2> 
   </div>


    <style>
    .container {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-around;
    }

    .card {
        width: calc(30% - 20px);
        margin-bottom: 20px;
        background-color: #f0f0f0;
        padding: 20px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }

    .field {
        margin-bottom: 10px;
    }

    input[type="text"],
    select {
        width: 100%;
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }

    h2 {
        margin-top: 0;
    }

    .buttons {
    display: flex;
    justify-content: center;
    margin-top: 20px;
    }

    .btn {
        margin: 0 10px;
    }

    .catchy-label {
    text-align: center;
    margin-top: 20px;
    font-size: 18px;
    color: #4caf50; /* Green color for the label */
}

    .catchy-green {
    background-color: #4caf50; /* Red button color */
}

.catchy-red {
    background-color: #e64a19; /* Darker red button color on hover */
}

/*.buttons {
    display: flex;
    justify-content: center;
    margin-top: 20px;
}

.btn {
    margin: 0 10px;
    padding: 8px 16px;
    background-color: #007bff;*/ /* Default button color */
    /*color: #fff;*/ /* Text color */
    /*border: none;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.btn:hover {
    background-color: #0056b3;*/ /* Button color on hover */
/*}*/


</style>

</asp:Content>