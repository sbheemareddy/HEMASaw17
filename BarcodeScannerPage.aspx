<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Barcode Scanner Page</title>
    <script type="text/javascript">
        {
        // Function to handle barcode scanning
            function handleBarcodeScan(barcode) {
                // Assign the scanned barcode value to a hidden field
                document.getElementById('<%= hidWOBarcode.ClientID %>').value = barcode;
            // Trigger the postback to the server
            __doPostBack('<%= btnHidden.ClientID %>', '');
        }
    </script>
</head>
<body onload="startBarcodeScanner()">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidWoBarcode" runat="server" />
        <asp:Button ID="btnHidden" runat="server" Text="HiddenButton" OnClick="btnHidden_Click" Style="display:none" />
    </form>
<script type="text/javascript">
    // JavaScript function to start the barcode scanner
    function startBarcodeScanner() {
        // Function to listen for barcode input
        window.addEventListener('message', function (e) {
            // Check if the message contains the scanned barcode data
            if (e.data && e.data.barcode) {
                // Call handleBarcodeScan with the scanned barcode value
                handleBarcodeScan(e.data.barcode);
            }
        });
    }
</script>
</body>
</html>
