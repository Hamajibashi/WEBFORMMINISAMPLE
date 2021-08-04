<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserList" %>

<%@ Register Src="~/UserControl/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <uc1:ucPager runat="server" id="ucPager" />
    <uc1:ucPager runat="server" id="ucPager1" />
    <uc1:ucPager runat="server" id="ucPager2" />
</asp:Content>
