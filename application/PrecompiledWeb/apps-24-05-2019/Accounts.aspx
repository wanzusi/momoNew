<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="AccountStatments, App_Web_o7vnt00f" title="ACCOUNTS" enableviewstatemac="false" %>
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
                            Account INFOR</td>
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
                <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 90%; border-bottom: #617da6 1px solid">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            Customer/AGENT Code</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="2" style="vertical-align: middle; height: 1px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtCustomerCode" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Style="font: menu" Text="Search" Width="85px" /></td>
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
                                <td style="width: 98%; height: 2px">
                                    <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                                        border-left: #617da6 1px solid; width: 50%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:RadioButton ID="rdPdf" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                    Text="PDF" /></td>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:RadioButton ID="rdExcel" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                    Text="EXCEL" /></td>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnConvert_Click"
                                                    Style="font: menu" Text="Convert" Width="85px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 2px">
                                </td>
                            </tr>
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
                     <asp:BoundColumn DataField="RecordId" HeaderText="RecordId" Visible="false">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerCode" HeaderText="Customer Code" Visible="false">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AccountName" HeaderText="Account Name">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AccountNumber" HeaderText="Account Number">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="AccountBalance" HeaderText="Account Balance">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Network" HeaderText="Network">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                         <asp:BoundColumn DataField="Type" HeaderText="Account Type">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="10%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid></td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                            style="width: 70%">
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 15px;
                                    background-color: white; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterfaceHeaderLabel2 InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    height: 18px; text-align: center">
                                    Account Details</td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    height: 24px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 50%; height: 10px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">
                                                            &nbsp;Company Name</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">
                                                            Company &nbsp;Code</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtCompanyCode" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">
                                                            Account Name</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtAccountName" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                            </td>
                                            <td style="vertical-align: top; width: 48%; height: 10px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            Account Number</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="width: 66%; height: 20px">
                                                            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            Network</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="width: 66%; height: 20px">
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                            Account Type</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="width: 66%; height: 20px">
                                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                Enabled="False"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" rowspan="2" style="vertical-align: top;
                                    height: 20px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    height: 19px; text-align: center">
                                    &nbsp;<asp:Button ID="btnReturn" runat="server" Font-Bold="True" OnClick="btnReturn_Click"
                                        Text="RETURN" Width="140px" /></td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    height: 19px; text-align: left">
                                </td>
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

