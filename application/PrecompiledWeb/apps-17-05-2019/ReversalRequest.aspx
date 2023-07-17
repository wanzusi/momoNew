<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="ReversalRequest, App_Web_sxogr9jo" title="Untitled Page" enableviewstatemac="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid;
        border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" id="TABLE1">
        <tr>
            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                text-align: center">
                AGENT</td>
            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                text-align: center">
                Status</td>
            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                text-align: center">
                AGENT-REF</td>
            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                text-align: center">
                FROM DATE</td>
            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                text-align: center">
                TO DATE</td>
        </tr>
        <tr>
            <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center;
                height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="vertical-align: middle; width: 20%; height: 24px; text-align: center;
                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                border-right-color: #617da6;">
                &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                    Width="95%" Style="font: menu">
                </asp:DropDownList></td>
            <td style="vertical-align: middle; width: 17%; height: 24px; text-align: center;
                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                border-right-color: #617da6;">
                <asp:DropDownList ID="dll_status" runat="server" CssClass="InterfaceDropdownList"
                    Width="95%" Style="font: menu">
                    <asp:ListItem Text="VALIDATED" Value="VALIDATED" />
                    <asp:ListItem Text="SUCCESS" Value="SUCCESS" />
                    <asp:ListItem Text="FAILED" Value="FAILED" />
                    <%--<asp:ListItem Text="All" Value="0" />--%>
                </asp:DropDownList></td>
            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                width: 17%; border-top-color: #617da6; height: 24px; text-align: center; border-right-width: 1px;
                border-right-color: #617da6">
                <asp:TextBox ID="txtpartnerRef" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                width: 17%; border-top-color: #617da6; height: 24px; text-align: center; border-right-width: 1px;
                border-right-color: #617da6">
                <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
            <td style="vertical-align: middle; width: 17%; height: 24px; text-align: center;
                border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                border-right-color: #617da6;">
                <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                border-right-color: #617da6">
                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                    Style="font: menu" Text="Search" Width="85px" /></td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
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
                    <asp:BoundColumn DataField="VendorTranId" HeaderText="Agent Ref">
                        <HeaderStyle Width="10%" />
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PegpayId" HeaderText="Pegpay Tranasction Id">
                        <HeaderStyle Width="10%" />
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TelecomId" HeaderText="Telecom Id">
                        <HeaderStyle Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TranAmount" HeaderText="Amount" ItemStyle-CssClass="rightColumnAlign">
                        <HeaderStyle Width="10%" CssClass="rightColumnAlign" />
                        <ItemStyle Width="120px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PaymentDate" HeaderText="Tran Date">
                        <HeaderStyle Width="15%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Status" HeaderText="Status">
                        <HeaderStyle Width="15%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                        <HeaderStyle Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Phone" HeaderText="Receiver">
                        <HeaderStyle Width="10%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MailCount" HeaderText="_l" ItemStyle-CssClass='hideColumn'>
                        <HeaderStyle Width="0%" CssClass="hideColumn" />
                    </asp:BoundColumn>
                    <asp:ButtonColumn CommandName="btnEdit" HeaderText="Reverse" Text="Reverse">
                        <HeaderStyle Width="10%" />
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="Blue" />
                    </asp:ButtonColumn>
                </Columns>
                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid>
        </asp:View>
        <asp:View ID="View2" runat="server">
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View3" runat="server">
            <table align="center" style="width: 90%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                        <tr>
                                            <td class="InterfaceHeaderLabel" style="text-align: center; height: 20px;">
                                                Reverse TRANSACTION</td>
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
                                                    Transaction DETAILS</td>
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
                                                    Transaction Details</td>
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
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                PegPayId</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtPegyapId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Enabled="False" Width="60%"></asp:TextBox>
                                                <asp:Label ID="lbl_amount" runat="server" Text="Label" Visible="False"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Phone</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="InterfaceTextboxLongReadOnly"
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
                                                Telecom Id&nbsp;</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtTelecomId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Telecom Reversal Id</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txt_ReversalId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Reason</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="InterfaceTextboxLongReadOnly"
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
                            OnClick="Button1_Click" Style="font: menu" Text="REVERSE TRANSACTION" Width="150px"
                            BackColor="green" ForeColor="#ffffff" />
                        <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                            OnClick="RejectTransaction_Click" Style="font: menu" Text="REJECT" Width="150px"
                            BackColor="#ff0000" ForeColor="#ffffff" /></td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <hr />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
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
