<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="CheckOvaBalance, App_Web_tn372hl1" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                           OVA Accounts</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                            &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                <table style="width: 100%">
                            <tr>
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            CHOOSE NETWORK
                            </td> 
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            CHOOSE OVA
                            </td> 
                            </tr>
                            <tr> 
                            <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="ddNetwork" runat="server" CssClass="InterfaceDropdownList"
                                Width="45%" style="font: menu" OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="ddNetwork_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>        
                            <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboOvaAccount" runat="server" CssClass="InterfaceDropdownList"
                                Width="45%" style="font: menu" OnDataBound="cboOvaAccount_DataBound" OnSelectedIndexChanged="cboOvaAccount_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            </td>
                            
                            </tr> 
               </table> 
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
                        &nbsp;<table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 1px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                    text-align: justify" Width="100%">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="SenderId" HeaderText="Account Name">
                            <HeaderStyle Width="25%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Balance" HeaderText="Account Balance">
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="35%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Msisdn" HeaderText="Account Msisdn">
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="35%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SpId" HeaderText="Sp Id">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
    </table>
    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"
        visible="False"></cr:crystalreportviewer>
</asp:Content>