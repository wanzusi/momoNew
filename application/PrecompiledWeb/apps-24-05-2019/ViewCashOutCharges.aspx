<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="ViewVendors, App_Web_txhr_f4q" title="CASHOUT CHARGES" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
                                <tr>
            <td style="width: 98%; height: 5px">
                &nbsp;<table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            MNO CASHOUT CHARGES</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                &nbsp;<asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="#003366" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"  />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="RecordId" HeaderText="RecordId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No.">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Network" HeaderText="Network" Visible="False">
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="RecordId" HeaderText="Edit"
                            Text="RecordId" Visible="false">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="Network" HeaderText="Network">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="StartRange" HeaderText="Min">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="EndRange" HeaderText="Max">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Charge" HeaderText="Charge">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></td>
        </tr>
                        </table>
                        </asp:View>
                </asp:MultiView></td>
        </tr>

    </table>
    <br />
    <br />
</asp:Content>

