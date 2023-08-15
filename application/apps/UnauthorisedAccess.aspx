<%@ Page Language="C#" MasterPageFile="~/NewReports.master" AutoEventWireup="true" CodeFile="UnauthorisedAccess.aspx.cs" Inherits="UnauthorisedAccess" Title="PAGE NOT FOUND" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="text-center">
        <span>  404.PAGE NOT FOUND.</span>
         <p style="color: #ff0000">
                This might be because you typed in the web address incorrectly, or the Page is nolonger Unavailable.</p>
         <asp:Label ID="lblWelcome" runat="server" Font-Bold="True" Text="."></asp:Label>
        <br />
        <asp:Label ID="lblUsage" runat="server" Font-Bold="True" Text="."></asp:Label>
    </div>

</asp:Content>

