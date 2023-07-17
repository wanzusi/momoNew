<%@ Page Language="C#" MasterPageFile="~/NewAccounts.master" AutoEventWireup="true" CodeFile="SwitchAccount.aspx.cs"
    Inherits="SwitchAccount" Title="SWITCH AIRTEL ACCOUNT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            SWITCH AIRTEL ACCOUNTS</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%">
                            <tr>
                                
                                <td style="width: 66%; text-align: center; height: 25px;">
                                    <asp:Button ID="btnActivate" runat="server" Font-Size="9pt" Height="23px" OnClick="btnActivate_Click"
                                        Text="ACTIVATE" Width="150px" Style="font: menu" />
                                 </td>
                            </tr>
                        </table>
                        <hr />
                        <asp:Label ID="lblTotal" runat="server" Text="." Font-Bold="True" ForeColor="#0000C0"></asp:Label></asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View2" runat="server">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                             Width="100%" Style="text-align: justify;
                            font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                            border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Pin" HeaderText="Pin">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                       <asp:BoundColumn DataField="Active" HeaderText="Active">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                               
                                <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.Active") %>'
                                            Width="40px" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></asp:View>
                    <asp:View ID="View3" runat="server">
                        <table align="center" style="width: 90%">
                            <tr>
                                <td style="width: 100%; height: 2px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                                    <tr>
                                                        <td class="InterfaceHeaderLabel" style="text-align: center;">
                                                            UPDATE PENDING TRANSACTION</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 5px; text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                DISTRICT DETAILS</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                            </td>
                                            <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                DISTRICT REGION</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">
                                                            PegPayId</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtVendorId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Enabled="False" Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            Amount</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                            </td>
                                            <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            TelecomId</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            <asp:TextBox ID="txtTelecomRef" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                Width="60%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 20px">
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="txtMark" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="Button1_Click" Style="font: menu" Text="MARK AS SENT" Width="150px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 100%; height: 2px; text-align: center">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
    </table>
    <br />
</asp:Content>
