<%@ page language="C#" masterpagefile="~/ReportMaster2.master" autoeventwireup="true" inherits="Transactions, App_Web_tk1t64pb" title="TRANSACTIONS" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            RECONCILIATION REPORT</td>
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            AGENT</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            AGENT-REF</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            RECON STATUS</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            FROM DATE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            TO DATE</td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" style="font: menu" OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtpartnerRef" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:DropDownList ID="ddReconStatus" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" style="font: menu">
                            </asp:DropDownList></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            </td>
                    </tr>
                </table>
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            &nbsp;FROM ACCOUNT
                        </td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            To ACCOUNT</td>
                        <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width: 34%;">
                            CUST NAME</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            Transaction Type</td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtAccount" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtSearch" runat="server" style="font: menu" Width="90%"></asp:TextBox></td>
                        <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtCustName" runat="server" Style="font: menu" Width="90%"></asp:TextBox>&nbsp;</td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:DropDownList ID="cboTranType" runat="server" CssClass="InterfaceDropdownList"
                                OnDataBound="cboTranType_DataBound" Style="font: menu" Width="95%">
                            </asp:DropDownList>&nbsp;</td>
                    </tr>
                </table>
            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" /></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
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
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                    GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" PageSize="50" Style="border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; width: 100%;
                    border-bottom: #617da6 1px solid; text-align: justify" Width="100%">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                        Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="RecordId" HeaderText="TransId" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FromAccount" HeaderText="From Account" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerRef" HeaderText="Phone">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CustName" HeaderText="Name">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        
                        <asp:BoundColumn DataField="TranType" HeaderText="Tran Type">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Vendor" HeaderText="Agent">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PayDate" HeaderText="Tran Date">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        
                         <asp:BoundColumn DataField="Status" HeaderText="Status">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TranAmount" HeaderText="Amount">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReconType" HeaderText="Recon Type" Visible="true">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReconciledBy" HeaderText="ReconciledBy" Visible="true">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReconAtPegpay" HeaderText="IsAtVendor" Visible="true">
                            <HeaderStyle Width="1%" />
                            <ItemStyle Width="1%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReconAtTelecom" HeaderText="IsAtTelecom" Visible="true">
                            <HeaderStyle Width="1%" />
                            <ItemStyle Width="1%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ReconciledDate" HeaderText="ReconciledDate" Visible="true">
                            <HeaderStyle Width="10%" />
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                </asp:DataGrid></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:Label ID="lblTotal" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="."></asp:Label></td>
        </tr>
    </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
</asp:Content>

