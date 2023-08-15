<%@ Page Language="C#" MasterPageFile="~/NewCustomer.master" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Customer" Title="CUSTOMER MANAGEMENT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="section">
        <div class="text-center">
        <h5 class="card-title"><asp:Label ID="lblRole" runat="server" Text="Label"></asp:Label>
                ROLE </h5>
             <asp:Label ID="lblWelcome" runat="server" Font-Bold="True" Text="."></asp:Label>
            <br />
            <asp:Label ID="lblUsage" runat="server" Font-Bold="True" Text="."></asp:Label>
            </div>
    </section>

</asp:Content>

