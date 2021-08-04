<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCoverImage.ascx.cs" Inherits="WebApplication2.WebUserControl2" %>

<div runat="server" id="divMain" style="background-color:darkkhaki">
    <img runat="server" id="imgCover" src="images/6681438.png" />
<span>
    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
</span>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</div>