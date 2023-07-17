<%@ page title="" language="C#" masterpagefile="~/ManagementMaster.master" autoeventwireup="true" inherits="ApproveVendorMgt, App_Web_b9azzvee" enableviewstatemac="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager>
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                            text-align: center">
                            &nbsp;Approve AGENTS</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 95%; border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 30%; height: 18px;
                            text-align: center">
                            AGENT CODE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 24%; height: 18px;
                            text-align: center">
                            FROM DATE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; height: 30px; text-align: center"
                            colspan="2">
                            TO DATE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="1" style="vertical-align: middle; height: 1px;
                            text-align: center">
                        </td>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center;
                            height: 1px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 30%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtCustName" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 24%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td colspan="2" style="border-top-width: 1px; width: 30%; border-left-width: 1px;
                            border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6;
                            vertical-align: middle; border-top-color: #617da6; height: 23px; text-align: center;
                            border-right-width: 1px; border-right-color: #617da6">
                            <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox>&nbsp;</td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" Style="font: menu" />&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
                <hr />
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 2px">
                                    <asp:Button ID="btnApprove" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                        OnClick="btnApprove_Click" Text="APPROVE" Width="150px" />
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 2px; text-align: right;">
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Font-Bold="True" Font-Names="Cambria"
                                        Font-Size="12pt" OnCheckedChanged="chkTransactions_CheckedChanged" Text="Select All Agents"
                                        Visible="False" /></td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 5px">
                                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnPageIndexChanged="DataGrid1_PageIndexChanged"
                                        Width="100%" Style="text-align: justify; font: menu; border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;"
                                        Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
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
                                           
                                            <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="VendorCode" HeaderText="AGENT CODE">
                                            </asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="Vendor" HeaderText="NAME">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TranType" HeaderText="TRAN TYPE">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="OvaChoice" HeaderText="OVA CHOICE">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SpId" HeaderText="OVA NAME">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Date" HeaderText="CREATED DATE">
                                            </asp:BoundColumn>
                                            
                                            <asp:TemplateColumn HeaderText="SELECT">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.IsApproved") %>'
                                                        />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:DataGrid></td>
                            </tr>
                        </table>
                    </asp:View>
                    &nbsp;
                </asp:MultiView></td>
        </tr>
    </table>
    <br />
    <br />
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txtfromDate">
    </ajaxToolkit:CalendarExtender>
</asp:Content>
