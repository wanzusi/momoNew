<%@ page language="C#" masterpagefile="~/Internetwork.master" autoeventwireup="true" inherits="InternetworkPendingTransactions, App_Web_jd1ezfxo" title="RESEND INTERNETWORK TRANSACTION" enableviewstatemac="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            TRANSACTIONS TO RESEND</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%; height: 5px">
                &nbsp;<asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View4" runat="server">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                            <tr>
                             <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 15%; height: 18px;
                                    text-align: center">
                                    STATUS</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 15%; height: 18px;
                                    text-align: center">
                                    TELECOM</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 15%; height: 18px;
                                    text-align: center">
                                    PHONE</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 15%; height: 18px;
                                    text-align: center">
                                    AGENT-REF</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 15%; height: 18px;
                                    text-align: center">
                                    TELECOM- REF</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 15%; height: 18px;
                                    text-align: center">
                                    FROM DATE</td>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 15%; height: 18px;
                                    text-align: center">
                                    TO DATE</td>
                            </tr>
                            <tr>
                                <td class="ddcolortabsline2" colspan="6" style="vertical-align: middle; text-align: center;
                                    height: 1px;">
                                </td>
                            </tr>
                            <tr>
                             <td style="vertical-align: middle; width: 15%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    &nbsp;<asp:DropDownList ID="ddStatus" runat="server" CssClass="InterfaceDropdownList"
                                        Width="95%" Style="font: menu">
                                    </asp:DropDownList></td>
                                <td style="vertical-align: middle; width: 15%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                        Width="95%" Style="font: menu" OnDataBound="cboVendor_DataBound">
                                    </asp:DropDownList></td>
 <td style="vertical-align: middle; width: 15%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:TextBox ID="phone" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="vertical-align: middle; width: 15%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:TextBox ID="txtpartnerRef" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="vertical-align: middle; width: 15%; height: 23px; text-align: center;
                                    border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                                    border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                                    border-right-color: #617da6;">
                                    <asp:TextBox ID="txtSearch" runat="server" Style="font: menu" Width="90%"></asp:TextBox>&nbsp;</td>
                                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                    width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                    border-right-color: #617da6">
                                    <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                                <td style="vertical-align: middle; width: 15%; height: 23px; text-align: center;
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
                                <td style="width: 76%; text-align: center; height: 25px;">
                                    <asp:Button ID="btnReverse" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReverse_Click"
                                        Text="REVERSE TRANSACTIONS" Width="150px" Style="font: menu" Visible="true" />
                                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="btnResend" runat="server" Font-Size="9pt"
                                        Height="23px" OnClick="btnResend_Click" Text="RESEND TRANSACTIONS" Width="150px"
                                        Style="font: menu" /></td>
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
                        <asp:DataGrid ID="dataGridResults" runat="server" AllowPaging="True"  PageSize="30" AutoGenerateColumns="False" CellPadding="4"
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
                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                            Width="40px" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                 <asp:BoundColumn DataField="CurrentTable" HeaderText="CurrentTable">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VendorTranId" HeaderText="VendorId">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RetryId" HeaderText="Retry Id">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Sender" HeaderText="Sender" Visible="False">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Receiver" HeaderText="Receiver">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="From" HeaderText="From">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="To" HeaderText="To">
                                    <HeaderStyle Width="7%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranId" HeaderText="TelecomId">
                                    <HeaderStyle Width="6%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                  <HeaderStyle Width="6%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Date" HeaderText="Tran Date">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Status" HeaderText="Status"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Reason"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></asp:View>
                    
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
