<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HEMASaw._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h2>QR Code Details</h2>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label for="Date" class="control-label">Date:</label>
                            <asp:TextBox ID="txtDate" class="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Time">WO#:</label>
                            <asp:TextBox ID="txtWO" class="fixed-size-input" runat="server" />
                        </div>

                        <div class="form-group">
                            <label for="LegacyPartId">Block#</label>
                            <asp:TextBox ID="txtBlock" class="fixed-size-input" runat="server" />
                        </div>

                        <div class="form-group">
                            <label for="EmpID">Badge#</label>
                            <asp:TextBox ID="txtBadge" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="EmpID">Slice#</label>
                            <asp:TextBox ID="txtSlice" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="EmpID">Saw#</label>
                            <asp:TextBox ID="txtSaw" class="fixed-size-input" runat="server" />
                        </div>

                        <div class="form-group">
                            <label for="MinThickness">Min Thickness</label>
                            <asp:TextBox ID="txtMin" class="fixed-size-input" runat="server" />
                        </div>

                        <div class="form-group">
                            <label for="MaxThickness">Max Thickness</label>
                            <asp:TextBox ID="txtMax" class="fixed-size-input" runat="server" />
                        </div>

                        <div class="form-group">
                            <label for="AvgThickness">Avg Thickness</label>
                            <asp:TextBox ID="txtAvg" class="fixed-size-input" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h2>User Input</h2>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label for="Weight" class="control-label">Weight</label>
                            <asp:TextBox ID="txtWeight" placeholder="Enter weight in Kg" class="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Length">Length</label>
                            <asp:TextBox ID="txtLength" type="text" placeholder="Enter length in Cm" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Width">Width</label>
                            <asp:TextBox ID="txtWidth" type="text" placeholder="Enter width in Cm" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Target Density">Target Density</label>
                            <asp:TextBox ID="txtTargetDensity" type="text" placeholder="Enter target density" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Blade">Blade</label>
                            <asp:TextBox ID="txtBlade" type="text" placeholder="Enter blade" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Table Speed">Table Speed</label>
                            <asp:TextBox ID="txtTableSpeed" type="text" placeholder="Enter table speed" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Type">Type</label>
                            <asp:TextBox ID="txtType" type="text" placeholder="Enter type" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="Comments">Comments</label>
                            <asp:TextBox ID="txtComments" type="text" mode="multiline" placeholder="Enter comments" class="fixed-size-input" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h2>System</h2>
                    </div>
                    <div class="panel-body">
                         <div class="form-group">
                            <label for="Status" class="control-label">Status</label>
                            <asp:TextBox ID="txtStatus" class="form-control" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="ChangeLog">Change Log:</label>
                            <asp:TextBox ID="txtChangeLog" class="fixed-size-input" runat="server" />
                        </div>
                        <div class="form-group">
                            <label for="SlicingBatch">Slicing Batch</label>
                            <asp:TextBox ID="txtSlicingBatch" class="fixed-size-input" runat="server" />
                        </div>

                        <div class="form-group">
                            <label for="Description">Description</label>
                            <asp:TextBox ID="txtDescription" class="fixed-size-input" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
            </div>
        </div>
    </div>
</asp:Content>
