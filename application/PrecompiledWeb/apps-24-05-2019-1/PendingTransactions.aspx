<%@ page language="C#" masterpagefile="~/Reconcilation.master" autoeventwireup="true" inherits="Reconciliation, App_Web_7q_oyp4k" title="MANUAL RECONCILIATION" enableviewstatemac="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            TRANSACTIONS TO RECONCILE</td>
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
                &nbsp;<asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View4" runat="server">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                            <tr>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    AGENT</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    TELECOM</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    AGENT-REF</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    TELECOM- REF</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    FROM DATE</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    TO DATE</td>
                            </tr>
                            <tr>
                                <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center;
                                    height: 1px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                        Width="95%" Style="font: menu" OnDataBound="cboVendor_DataBound">
                                    </asp:DropDownList></td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:DropDownList ID="cboTelecoms" runat="server" CssClass="InterfaceDropdownList"
                                        Width="88%" Style="font: menu" OnDataBound="cboTelecoms_DataBound">
                                    </asp:DropDownList></td>
                                <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:TextBox ID="txtpartnerRef" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:TextBox ID="txtSearch" runat="server" Style="font: menu" Width="90%"></asp:TextBox>&nbsp;</td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                </td>
                            </tr>
                        </table>
                        <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 98%; border-bottom: #617da6 1px solid">
                            <tr>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    Transaction Type</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                    Transaction Status</td>
                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 34%;
                                    height: 18px; text-align: center">
                                </td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                                    text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; height: 1px;
                                    text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:DropDownList ID="cboTranType" runat="server" CssClass="InterfaceDropdownList"
                                        Width="95%" Style="font: menu" OnDataBound="cboVendor_DataBound">
                                    </asp:DropDownList>&nbsp;</td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 20%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:DropDownList ID="ddTranStatus" runat="server" CssClass="InterfaceDropdownList"
                                        Width="95%" Style="font: menu">
                                    </asp:DropDownList>&nbsp;</td>
                                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    &nbsp;</td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                            Text="Search" Width="85px" Style="font: menu" /></asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 19%; text-align: left; height: 25px;">
                                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Bold="True"
                                        OnCheckedChanged="CheckBox2_CheckedChanged" Text="Select All" /></td>
                                <td style="width: 66%; text-align: center; height: 25px;">
                                    <asp:Button ID="btnReverse" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReverse_Click"
                                        Text="REVERSE TRANSACTIONS" Width="150px" Style="font: menu" />
                                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btnResend" runat="server" Font-Size="9pt"
                                        Height="23px" OnClick="btnResend_Click" Text="RESEND TRANSACTIONS" Width="150px"
                                        Style="font: menu" /></td>
                                <td style="width: 30%; text-align: right; height: 25px;">
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" Font-Bold="True"
                                        OnCheckedChanged="chkSelect_CheckedChanged" Text="Select All" /></td>
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
                            OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" Style="text-align: justify;
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
                                <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.Approved") %>'
                                            Width="40px" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="PegPayTranId" HeaderText="PegPayID"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FromAccount" HeaderText="From Account" Visible="False">
                                    <HeaderStyle Width="30%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Phone" HeaderText="Phone">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CustName" HeaderText="Name" Visible="False">
                                    <HeaderStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TransactionType" HeaderText="Tran Type">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Company" HeaderText="Agent">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranAmount" HeaderText="Amount"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PayDate" HeaderText="Tran Date">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Status" HeaderText="Status"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Reason"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnEdit" DataTextField="PegPayTranId" HeaderText="Mark AS Sent"
                                    Text="Mark As Sent">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
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
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
