<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="WebApplication2.WebForm7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>My Title</title>
    <style>
        p:last-child {
            color:red;
        }
        span {
            color:blue;
        }
        #span2 {
            color:green;
        }
        .cls1 {
            color:mediumpurple;
        }
        .cls2 {
            color:pink;
        }
        p > span {
            background-color:lightcoral;
        }
    </style>
</head>
<body>
    <div>
        <p class="cls1">P Text1</p>
        <p>
            <span class="cls1">First</span>
            <span id="span2" class="c1s2">Second</span>
            <span class="cls2">Third</span>
        </p>
        <p>P Text3</p>
        <span>p>span是只p之下的span，所以這裡不會被選到。</span>
    </div>
</body>
</html>
