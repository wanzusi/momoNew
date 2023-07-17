<%@ Page Language="C#" MasterPageFile="~/NewPayments.master" AutoEventWireup="true" CodeFile="ViewCorporateBatches.aspx.cs" Inherits="ViewCorporateBatches" Title="VIEW PAYMENT BATCHES" %>
<%--<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>--%>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
    &nbsp;<table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            Corporate Payment Batches</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
                <hr />
    </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
                <asp:Label ID="lblBatchCode" runat="server" Text="0" Visible="False"></asp:Label></td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            Batch Type</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            FROM DAte</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                            To date</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:DropDownList ID="cboType" runat="server" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                OnDataBound="cboType_DataBound" Width="90%">
                            </asp:DropDownList>&nbsp;</td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtfromDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:TextBox ID="txttoDate" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" />&nbsp;</td>
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
                &nbsp;<asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify" OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="NO.">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False">
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnView" DataTextField="BatchCode" HeaderText="VIEW DETAILS"
                            Text="BatchNo">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="Status" HeaderText="STATUS" Visible="true">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Total" HeaderText="AMOUNT">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="120px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordedBy" HeaderText="UPLOADED">
                            <HeaderStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Date" HeaderText="DATE">
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
            </td>
        </tr>
    </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 2px">
                        Batch Details </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 5px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:Button ID="btnReturn" runat="server" Font-Bold="True" OnClick="btnReturn_Click"
                            Text="RETURN" Width="100px" /></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px; text-align:right;">
                        </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                        &nbsp;<asp:DataGrid ID="DataGrid2" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid2_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BatchNo" HeaderText="BatchNo" Visible="False"></asp:BoundColumn>
                                 <asp:BoundColumn DataField="RecordId" HeaderText="RecordId" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficaryAccount" HeaderText="CONTACT">
                                <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficiaryName" HeaderText="NAME">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Title" HeaderText="Title">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Location" HeaderText="Location">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranCharge" HeaderText="Pegasus Charge">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PayOutFee" HeaderText="MNO Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CashOutFee" HeaderText="CashOut Fee" >
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Exclude">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                        Checked='<%# DataBinder.Eval(Container, "DataItem.Excluded") %>'
                                            Width="5%" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="5%" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblPegasusTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:Label ID="lblMnoFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp; &nbsp;&nbsp;
                        <asp:Label ID="lblCashoutFee" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="lblAllTotal" runat="server" Font-Bold="True" Text="."></asp:Label></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View3" runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 98%; height: 1px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 98%; height: 1px"><table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                                    <tr>
                                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; height: 18px;
                            text-align: center" colspan="4">
                                            &nbsp;Payment Batch</td>
                                    </tr>
                                    <tr>
                                        <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; text-align: center; height: 1px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width:50%;">
                                        </td>
                                        <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width:50%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="vertical-align: middle;
                                height: 18px; text-align: center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                            <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                                <tr>
                                                    <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                    &nbsp;From Account</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:DropDownList ID="cboFromAccount" runat="server" AutoPostBack="True" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                                                        OnDataBound="cboFromAccount_DataBound" OnSelectedIndexChanged="cboFromAccount_SelectedIndexChanged" Width="80%">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp">
                                                                    Total Amount</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtBatchTotal" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Width="80%" Enabled="False"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                    </td>
                                                    <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                    Account Balance</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtAccountBalance" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Width="80%" Enabled="False"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                            &nbsp;<table align="center" cellpadding="0" cellspacing="0" style="width: 60%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                                                <tr>
                                                    <td style="vertical-align: middle; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;" colspan="2">
                                            <asp:RadioButtonList BackColor="WhiteSmoke" ID="rbnSchedule" OnSelectedIndexChanged="rbnApproval_SelectedIndexChanged" RepeatDirection="Horizontal" runat="server" Width="100%" Font-Bold="True" >
                                                <asp:ListItem Value="1">Pay Now </asp:ListItem>
                                                <asp:ListItem Value="2">Schedule Payment Batch </asp:ListItem>
                                            </asp:RadioButtonList></td>
                                                </tr>
                                            </table>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                            <table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                                text-align: center">
                                                                SET SCHEDULE DATE</td>
                                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                                text-align: center">
                                                                Hour</td>
                                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                                                                Min</td>
                                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                            text-align: center">
                                                                AM/PM</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ddcolortabsline2" colspan="1" style="vertical-align: middle; height: 1px;
                                                                text-align: center">
                                                            </td>
                                                            <td class="ddcolortabsline2" colspan="1" style="vertical-align: middle; height: 1px;
                                                                text-align: center">
                                                            </td>
                                                            <td class="ddcolortabsline2" colspan="2" style="vertical-align: middle; text-align: center; height: 1px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                border-right-color: #617da6">
                                                                <asp:TextBox ID="txtScheduleDate" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                border-right-color: #617da6">
                                                                <asp:DropDownList ID="cboHour" runat="server" Width="70%">
                                                                    <asp:ListItem Selected="True" Value="0">HH</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem></asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td style="vertical-align: middle; width: 25%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                                                                <asp:DropDownList ID="cbomin" runat="server" Width="70%">
                                                                    <asp:ListItem Selected="True" Value="MM">MM</asp:ListItem>
                                                                    <asp:ListItem>00</asp:ListItem>
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                    <asp:ListItem>32</asp:ListItem>
                                                                    <asp:ListItem>33</asp:ListItem>
                                                                    <asp:ListItem>34</asp:ListItem>
                                                                    <asp:ListItem>35</asp:ListItem>
                                                                    <asp:ListItem>36</asp:ListItem>
                                                                    <asp:ListItem>37</asp:ListItem>
                                                                    <asp:ListItem>38</asp:ListItem>
                                                                    <asp:ListItem>39</asp:ListItem>
                                                                    <asp:ListItem>40</asp:ListItem>
                                                                    <asp:ListItem>41</asp:ListItem>
                                                                    <asp:ListItem>42</asp:ListItem>
                                                                    <asp:ListItem>43</asp:ListItem>
                                                                    <asp:ListItem>44</asp:ListItem>
                                                                    <asp:ListItem>45</asp:ListItem>
                                                                    <asp:ListItem>46</asp:ListItem>
                                                                    <asp:ListItem>47</asp:ListItem>
                                                                    <asp:ListItem>48</asp:ListItem>
                                                                    <asp:ListItem>49</asp:ListItem>
                                                                    <asp:ListItem>50</asp:ListItem>
                                                                    <asp:ListItem>51</asp:ListItem>
                                                                    <asp:ListItem>52</asp:ListItem>
                                                                    <asp:ListItem>53</asp:ListItem>
                                                                    <asp:ListItem>54</asp:ListItem>
                                                                    <asp:ListItem>55</asp:ListItem>
                                                                    <asp:ListItem>56</asp:ListItem>
                                                                    <asp:ListItem>57</asp:ListItem>
                                                                    <asp:ListItem>58</asp:ListItem>
                                                                    <asp:ListItem>59</asp:ListItem>
                                                                    <asp:ListItem></asp:ListItem>
                                                                </asp:DropDownList>&nbsp;</td>
                                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                                                                <asp:DropDownList ID="cboDaystatus" runat="server" Width="70%">
                                                                    <asp:ListItem Selected="True">AM/PM</asp:ListItem>
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                    <asp:ListItem>PM</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                    </table>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="vertical-align: middle; height: 15px; text-align: center">
                                            </td>
                                    </tr>
                                </table>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View4" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 1px;">
                        Rejected Batches</td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:DataGrid ID="DataGrid3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid3_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BatchID" HeaderText="BatchID" Visible="False">
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnView" DataTextField="BatchCode" HeaderText="VIEW DETAILS"
                            Text="BatchNo">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="Status" HeaderText="STATUS" Visible="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Total" HeaderText="AMOUNT">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RecordedBy" HeaderText="UPLOADED">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Date" HeaderText="DATE">
                                    <HeaderStyle Width="20%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View5" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 1px">
                        Rejected Batch Details</td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:DataGrid ID="DataGrid4" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BatchNo" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="No." HeaderText="NO.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficaryAccount" HeaderText="CONTACT" Visible="true">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="BeneficiaryName" HeaderText="NAME">
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TypeName" HeaderText="TYPE">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                 <asp:BoundColumn DataField="Title" HeaderText="Title">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                 <asp:BoundColumn DataField="Location" HeaderText="Location">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TranCharge" HeaderText="Charge">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PayOutFee" HeaderText="MNO Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CashOutFee" HeaderText="CashOut Fee">
                                    <HeaderStyle Width="10%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NetworkCode" HeaderText="Network">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <asp:Label ID="lblShowTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" id="Table2">
                            <tr>
                                <td class="InterfaceHeaderLabel2" style="vertical-align: middle; height: 18px;
                            text-align: center" colspan="4">
                                    Approve Payment Batch</td>
                            </tr>
                            <tr>
                                <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; text-align: center; height: 1px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width:50%;">
                                    COMMENT</td>
                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width:50%;">
                                    COMMENT (IF ANY)</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="vertical-align: middle;
                                height: 18px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="vertical-align: middle; width: 50%;
                                height: 18px; text-align: center">
                                    <asp:TextBox ID="txtCommentSent" runat="server" ReadOnly="True" TextMode="MultiLine"
                                        Width="85%"></asp:TextBox></td>
                                <td  colspan="2" style="vertical-align: middle; width: 50%;
                                height: 18px; text-align: center">
                                    <asp:TextBox ID="txtCommentToSend" runat="server" TextMode="MultiLine" Width="85%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                    <asp:Button ID="Button8" runat="server" Font-Bold="True" OnClick="Button8_Click"
                                        Text="MODIFY" Width="86px" Visible="False" />&nbsp;
                                    <asp:Button ID="Button5" runat="server" Font-Bold="True" OnClick="Button5_Click"
                                        Text="RE-SUBMIT" Width="116px" />
                                    <asp:Button ID="Button6" runat="server" Font-Bold="True" OnClick="Button6_Click"
                                        Text="DECLINE" Width="83px" /></td>
                            </tr>
                            <tr>
                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                    <asp:Label ID="lblUserFrom" runat="server" Text="." Visible="False"></asp:Label><asp:Label
                                        ID="lblLevelFrom" runat="server" Text="." Visible="False"></asp:Label><asp:Label
                                            ID="lblRoleFrom" runat="server" Text="." Visible="False"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView4" runat="server">
        <asp:View ID="View6" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 98%; height: 1px;">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="InterfaceHeaderLabel2" style="width: 98%; height: 1px">
                        Batch Audit trail</td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                    </td>
                </tr>
            </table>
            <asp:DataGrid ID="DataGrid5" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <Columns>
                    <asp:BoundColumn DataField="BatchCode" HeaderText="BatchCode" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="No." HeaderText="NO.">
                        <HeaderStyle Width="5%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Comment" HeaderText="COMMENT" Visible="true">
                        <HeaderStyle Width="20%" />
                        <ItemStyle Width="20%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RecordedBy" HeaderText="RECORDED BY">
                        <HeaderStyle Width="25%" />
                        <ItemStyle Width="25%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Date" HeaderText="DATE/TIME">
                        <HeaderStyle Width="20%" />
                    </asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid></asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView5" runat="server">
        <asp:View ID="View7" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 98%; height: 1px;">
                        </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 19px"><table align="center" cellpadding="0" cellspacing="0" style="width: 80%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" id="TABLE1">
                        <tr>
                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; height: 18px;
                            text-align: center" colspan="4">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Approve Payment Batch</td>
                        </tr>
                        <tr>
                            <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                        </tr>
                        <tr>
                            <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width:38%;">
                                &nbsp;ACTION</td>
                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width:30%;">
                                &nbsp;AUTHORIZER</td>
                            <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                            text-align: center; width:50%;">
                                COMMENT&nbsp;(IF ANY)</td>
                        </tr>
                        <tr>
                            <td colspan="4" style="vertical-align: middle;
                                height: 18px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="vertical-align: middle; width: 38%;
                                height: 18px; text-align: center"><asp:RadioButtonList BackColor="WhiteSmoke" CssClass="InterfaceDropdownList" ID="rbnApproval" OnSelectedIndexChanged="rbnApproval_SelectedIndexChanged" RepeatDirection="Horizontal" runat="server" Width="80%" Font-Bold="True" >
                                    <asp:ListItem Value="1">Approve </asp:ListItem>
                                    <asp:ListItem Value="2">Reject </asp:ListItem>
                                </asp:RadioButtonList></td>
                                 <td colspan="2" style="vertical-align: middle; width: 30%;
                                height: 18px; text-align: center">
                                                <asp:DropDownList ID="ddAuthorizer" runat="server" 
                                OnDataBound="ddAuthorizer_DataBound" Width="70%" 
                                                    onselectedindexchanged="ddAuthorizer_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            <td  colspan="2" style="vertical-align: middle; width: 50%;
                                height: 18px; text-align: center"><asp:TextBox Width="85%" ID="txtComment" runat="server" TextMode="MultiLine" /></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnApproval" runat="server" Font-Italic="False" Text="SUBMIT" OnClick="btnApproval_Click" />
                                <asp:Button ID="btnApprovalReturn" runat="server" Text="RETURN" OnClick="btnApprovalReturn_Click" /></td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 22px">
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
   <%--<ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" xmlns:ajaxtoolkit="system.web.ui">
             </ajaxToolkit:ToolkitScriptManager>--%>
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
             </ajaxToolkit:ToolkitScriptManager>
    &nbsp;
    <br />
    <%--<ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate" xmlns:ajaxtoolkit="system.web.ui" 
        >
             </ajaxToolkit:CalendarExtender>--%>
    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" TargetControlID="txttoDate"
        >
             </ajaxToolkit:CalendarExtender>
    <%--<ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" 
        TargetControlID="txtfromDate" xmlns:ajaxtoolkit="system.web.ui">--%>
    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" 
        TargetControlID="txtfromDate">
             </ajaxToolkit:CalendarExtender>
    <%--<ajaxToolkit:CalendarExtender id="CalendarExtender3" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" 
        TargetControlID="txtScheduleDate" xmlns:ajaxtoolkit="system.web.ui">--%>
    <ajaxToolkit:CalendarExtender id="CalendarExtender3" runat="server" CssClass="MyCalendar"
        Format="dd MMMM yyyy" PopupPosition="TopLeft" 
        TargetControlID="txtScheduleDate" >
             </ajaxToolkit:CalendarExtender>
    <asp:Label ID="Label1" runat="server" Text="0" Visible="False"></asp:Label><br />
    <%--<CR:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"
        visible="False" xmlns:cr="crystaldecisions.web"></CR:crystalreportviewer>--%>
    <%--<CR:crystalreportviewer id="CrystalReportViewer2" runat="server" autodatabind="true"
        visible="False" ></CR:crystalreportviewer>--%>
</asp:Content>

