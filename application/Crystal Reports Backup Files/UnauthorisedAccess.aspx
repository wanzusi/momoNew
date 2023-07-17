<%@ Page Language="C#" MasterPageFile="~/NewReports.master" AutoEventWireup="true" CodeFile="UnauthorisedAccess.aspx.cs" Inherits="UnauthorisedAccess" Title="PAGE NOT FOUND" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center; text-decoration: underline">
                <span style="color: #ff0000">
                404.PAGE NOT FOUND.</span></td>
        </tr>
         <tr>
            <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center; text-decoration: underline">
                <span style="color: #ff0000">
                This might be because you typed in the web address incorrectly, or the Page is nolonger Unavailable.</span></td>
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

