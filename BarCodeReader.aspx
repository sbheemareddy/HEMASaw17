<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarCodeReader.aspx.cs" Inherits="HEMASaw.BarCodeReader" %>
<!DOCTYPE html>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Barcode Reader</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtBarcode" runat="server" ClientIDMode="Static"></asp:TextBox>
        </div>
    </form>

    <script>
        document.addEventListener('keydown', function (event) {
            var textBox = document.getElementById('txtBarcode');
            textBox.value += event.key;
        });
    </script>
</body>
</html>

