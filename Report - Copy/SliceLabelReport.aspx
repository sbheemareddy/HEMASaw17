﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SliceLabelReport.aspx.cs" Inherits="SliceLabelReport" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=18.0.24.130, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
        <div>
            <iframe id="pdfFrame" src="C:\Srinivas\HEMASaw\Reports\SliceLabel.pdf" width="100%" height="600" frameborder="0" runat="server"></iframe>
        </div>
</body>
</html>
