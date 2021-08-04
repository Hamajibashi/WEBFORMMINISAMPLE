<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm9.aspx.cs" Inherits="WebApplication2.WebForm9" %>

<%@ Register Src="~/usAddControl.ascx" TagPrefix="uc1" TagName="usAddControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:usAddControl runat="server" id="usAddControl" />
    </form>
</body>
</html>
