<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quick Receipt DMZ Redirect</title>
</head>
<body>
<%
    try
    {
        var code = Request.QueryString["qrcode"].ToString();
        if (code == null || code == "") { throw new Exception(); }
        Response.Write("Redirecting to the site behind firewall with QRCode: " + code);
        Response.Write("If the redirect fails please check your VPN connection");
        Response.Redirect("http://breynolds-8540w/QuickReceipt/PurchaseOrder/Associate?QRCode=" + code);
    }
    catch (Exception ex)
    {
        Response.Write("This webpage will only function if a \"qrcode\" parameter is passed.");
    }

%>
</body>
</html>
