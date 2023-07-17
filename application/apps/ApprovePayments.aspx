<%@ Page Language="C#" MasterPageFile="~/NewPayments.master" AutoEventWireup="true" CodeFile="ApprovePayments.aspx.cs" Inherits="ApprovePayments" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td class="InterFaceTableLeftRowUp" style="width: 100%; height: 20px; text-align: center;
                text-decoration: underline">
                <asp:Label ID="lblRole" runat="server" Text=""></asp:Label> ROLE</td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center">
                <asp:Label ID="lblWelcome" runat="server" Font-Bold="True" Text="."></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; height: 2px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 2px; text-align: center">
                <asp:Label ID="lblUsage" runat="server" Font-Bold="True" Text="."></asp:Label></td>
        </tr>
        <hr />
    </table>
</asp:Content>

