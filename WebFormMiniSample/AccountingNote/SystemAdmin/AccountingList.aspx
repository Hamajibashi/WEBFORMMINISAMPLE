<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControl/ucPager2.ascx" TagPrefix="uc1" TagName="ucPager2" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx">流水帳管理</a>
                </td>
                <td>
                    <!--這裡放主要內容-->
                    <asp:Button ID="btnCreate" runat="server" Text="Add Accounting" OnClick="btnCreate_Click" />
                    
                    <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <Columns>
                            <asp:BoundField HeaderText="標題" DataField="Caption" />
                            <asp:BoundField HeaderText="金額" DataField="Amount" />
                            <asp:TemplateField HeaderText="In / Out">
                                <ItemTemplate>
                                    <%--<%# ((int)Eval("ActType")==0) ? "支出":"收入" %>--%>
                                    <%--<%#((int)Eval("ActType")==0)
                                            ?"支出"
                                            :((int)Eval("ActType")==1)
                                            ?"收入":""
                                    %>--%>
                                    <asp:Literal runat="server" ID="ltActType"></asp:Literal>
                                    <asp:Label runat="server" ID="lblActType"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="
                                {0:yyyy/MM/dd}" />
                            <asp:TemplateField HeaderText="Act">
                                <ItemTemplate>
                                    <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%#Eval("ID") %>">Edit</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>

                    <%--<asp:Literal  runat="server" ID="ltPager">
                    </asp:Literal>--%>

                    <div>
                    <uc1:ucPager2 runat="server" id="ucPager2" PageSize="10" CurrentPage="1" TotalSize="10"  Url="/SystemAdmin/AccountingList.aspx" />
                        </div>
                    <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                        <p style="color: red; background-color: cornflowerblue">
                            No Data in your Accounting Note.
                        </p>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
