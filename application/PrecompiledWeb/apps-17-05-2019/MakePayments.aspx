<%@ page language="C#" masterpagefile="~/CustomerPayments.master" autoeventwireup="true" inherits="AddCorporateBeneficiary, App_Web_iahhviua" title="MAKE PAYMENTS" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center; height: 19px;">
                            Make Payments To BENEFICIARies</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 50%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center">
                            <asp:RadioButtonList ID="rbnMethod" runat="server" AutoPostBack="True"
                                Font-Bold="True" OnSelectedIndexChanged="rbnMethod_SelectedIndexChanged" RepeatDirection="Horizontal"
                                Width="92%">
                                <asp:ListItem Value="0">SINGLE PAYMENT</asp:ListItem>
                                <asp:ListItem Value="1">BULK PAYMENT</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <br />
                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View4" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center; height: 19px;">
                                    Search For Beneficiary</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="6" style="vertical-align: top;
                                    height: 26px; text-align: center">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="6" style="vertical-align: top;
                                    height: 26px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                                                text-align: center">
                                    Beneficary Name</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                    Phone Number</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                                BeneFICIARY TYPE&nbsp;</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; height: 1px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                &nbsp;<asp:TextBox ID="txtsearchBenName" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:TextBox ID="txtSearchBenPhone" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:DropDownList ID="cboType" runat="server" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                                    OnDataBound="cboType_DataBound" Width="90%">
                                                </asp:DropDownList></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" Text="Search" Width="140px" OnClick="btnSearch_Click" />&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: left; height: 19px;">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: center; height: 22px;">
                                    &nbsp;<table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: middle; height: 18px;
                                                text-align: center">
                                    Select Beneficiary</td>
                                        </tr>
                                        <tr>
                                            <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                            <asp:DropDownList ID="cboBeneficiary" runat="server" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                OnDataBound="cboCorporation_DataBound" Width="55%" AutoPostBack="True" OnSelectedIndexChanged="cboBeneficiary_SelectedIndexChanged">
                            </asp:DropDownList>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: left; height: 333px;">
                                    <asp:MultiView ID="MultiView3" runat="server">
                                        <asp:View ID="View6" runat="server">
                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                                                <tr>
                                                    <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 15px;
                                    background-color: white; text-align: left">
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2 InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 18px; text-align: center">
                                                        Beneficiary Details</td>
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
                                                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                &nbsp;Name</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                                                <asp:TextBox ID="txtName" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                                    Enabled="False"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                Type Code</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                                                <asp:TextBox ID="txtBenficiaryType" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                                    Enabled="False"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                Phone</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                                    Enabled="False"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                                </td>
                                                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Email</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                                    Enabled="False"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Location</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                                                <asp:TextBox ID="txtLocation" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                                    Enabled="False"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Active</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                                                <asp:CheckBox ID="chkActive" runat="server" Enabled="False" /></td>
                                                                        </tr>
                                                                    </table>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2 InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 24px; text-align: center">
                                                        PAYMENT DETAILS</td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                            text-align: center; height: 24px;">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 98%; border-bottom: #617da6 1px solid">
                                                            <tr>
                                                                <td  colspan="4" style="vertical-align: middle; height: 18px;
                                                                    text-align: center">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 50%;
                                                                    height: 18px; text-align: center">
                                                                    SELECT FROM ACCOUNT</td>
                                                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                                                                    text-align: center">
                                                                    Account Balance &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                                                    text-align: center">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    <asp:DropDownList ID="cboFromAccount" runat="server" AutoPostBack="True" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                                                        OnDataBound="cboFromAccount_DataBound" OnSelectedIndexChanged="cboFromAccount_SelectedIndexChanged" Width="70%">
                                                                    </asp:DropDownList>&nbsp;</td>
                                                                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    <asp:TextBox ID="txtAccountBalance" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                        Font-Bold="True" Width="70%" Enabled="False"></asp:TextBox>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="InterfaceHeaderLabel2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    ENTER AMOUNT:</td>
                                                                <td colspan="2"  class="InterfaceHeaderLabel2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    Enter Payment Reason</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 18px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                        Font-Bold="True" Width="60%" ForeColor="#C00000"></asp:TextBox></td>
                                                                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Width="85%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    <asp:CheckBox ID="ChkCharge" runat="server" Text="INCLUDE CASHOUT FEE" /></td>
                                                                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    <asp:CheckBox ID="ChkTax" runat="server" Text="INCLUDE RECEIVER'S 1% FEE" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: left; height: 20px;" rowspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 19px; text-align: center">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="btnSave_Click" Text="MAKE PAYMENT" Width="140px" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: left; height: 19px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center">
                                    </td>
                            </tr>
                        </table>
                            </asp:View>
                            <asp:View ID="View5" runat="server"><table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                                <tr>
                                    <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; height: 2px; text-align: center; width: 634px;">
                                        Confirm Payemt And Proceed</td>
                                </tr>
                                <tr>
                                    <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 15px;
                                        background-color: white; text-align: center; width: 634px;">
                                                    <asp:Button ID="btnConfirm" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" Text="YES" Width="94px" OnClick="btnConfirm_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" Text="NO" Width="94px" OnClick="btnCancel_Click" /></td>
                                </tr>
                                <tr>
                                    <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 15px;
                                        background-color: white; text-align: center; width: 634px;">
                                        &nbsp;&nbsp;
                                        <asp:MultiView ID="MultiView4" runat="server">
                                            <asp:View ID="View7" runat="server">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 98%; height: 1px">
                                                            <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                                                border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 60%; border-bottom: #617da6 1px solid">
                                                                <tr>
                                                                    <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                        border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                        border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                        border-right-color: #617da6">
                                                                        <asp:RadioButtonList ID="rbnSchedule" runat="server" AutoPostBack="True" BackColor="WhiteSmoke"
                                                                            Font-Bold="True" OnSelectedIndexChanged="rbnApproval_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                                            Width="100%">
                                                                            <asp:ListItem Value="1">Send Payment Batch </asp:ListItem>
                                                                            <asp:ListItem Value="2">Schedule Payment Batch </asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 98%; height: 1px">
                                                            <asp:MultiView ID="MultiView5" runat="server">
                                                                <asp:View ID="View9" runat="server">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
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
                                                                            <td class="ddcolortabsline2" colspan="2" style="vertical-align: middle; height: 1px;
                                                                                text-align: center">
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
                                                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                                border-right-color: #617da6">
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
                                                                </asp:View>
                                                            </asp:MultiView></td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                        </asp:MultiView></td>
                                </tr>
                                <tr>
                                    <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: center; width: 634px;">
                                        </td>
                                </tr>
                                <tr>
                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 19px; text-align: center; width: 634px;">
                                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                            <tr>
                                                <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                        <tr>
                                                            <td class="InterFaceTableLeftRowUp">
                                                                &nbsp;Name</td>
                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                            </td>
                                                            <td class="InterFaceTableRightRow">
                                        <asp:TextBox ID="txtViewName" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="InterFaceTableLeftRowUp">
                                                                Type</td>
                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                            </td>
                                                            <td class="InterFaceTableRightRow">
                                        <asp:TextBox ID="txtViewType" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="InterFaceTableLeftRowUp">
                                                                Phone</td>
                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                            </td>
                                                            <td class="InterFaceTableRightRow">
                                        <asp:TextBox ID="txtviewPhone" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False" ReadOnly="True"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                </td>
                                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                        <tr>
                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                Email</td>
                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                            </td>
                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                        <asp:TextBox ID="txtviewEmail" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                Location</td>
                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                            </td>
                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                        <asp:TextBox ID="txtViewLocation" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                Active</td>
                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                            </td>
                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled="False" /></td>
                                                        </tr>
                                                    </table>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: top; height: 5px; text-align: left" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                                    &nbsp;<table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                                            border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 98%; border-bottom: #617da6 1px solid">
                                                        <tr>
                                                            <td  colspan="4" style="vertical-align: middle; height: 18px;
                                                                    text-align: center">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 50%;
                                                                    height: 18px; text-align: center">
                                                                FROM ACCOUNT</td>
                                                            <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; height: 18px;
                                                                    text-align: center">
                                                                Account Balance &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                                                    text-align: center">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                &nbsp;<asp:DropDownList ID="cboViewAccount" runat="server" AutoPostBack="True" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                                                Enabled="False" OnDataBound="cboFromAccount_DataBound" OnSelectedIndexChanged="cboFromAccount_SelectedIndexChanged" Width="70%">
                                                            </asp:DropDownList></td>
                                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                            <asp:TextBox ID="txtViewAccountBalance" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                Font-Bold="True" Width="70%" Enabled="False"></asp:TextBox>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" class="InterfaceHeaderLabel2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                AMOUNT:</td>
                                                            <td colspan="2"  class="InterfaceHeaderLabel2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                &nbsp;Payment Reason</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 18px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                            <asp:TextBox ID="txtViewAmount" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Font-Bold="True" ForeColor="#C00000" Width="60%" Enabled="False"></asp:TextBox></td>
                                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                <asp:TextBox ID="txtViewReason" runat="server" TextMode="MultiLine" Width="85%"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                width: 50%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                border-right-color: #617da6">
                                                                <asp:CheckBox ID="chkviewCashOut" runat="server" Text="INCLUDE CASHOUT FEE" /></td>
                                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                border-right-color: #617da6"><asp:CheckBox ID="chkReceiverTax" runat="server" Text="INCLUDE RECEIVER'S 1% TAX" OnCheckedChanged="CheckBox2_CheckedChanged" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                                    border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                                    border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                                    border-right-color: #617da6">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="vertical-align: top; height: 15px; text-align: center">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        </td>
                                </tr>
                            </table>
                            </asp:View>
                        </asp:MultiView></asp:View>
                    <asp:View ID="View2" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center" rowspan="2">
                                    MAKE A BULK PAYMENT</td>
                            </tr>
                            <tr style="color: #000000">
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; text-align: left">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                            text-align: center; height: 22px;">
                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 259px; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                SelECT BeneFICIARY TYPE</td>
                                            <td class="InterfaceHeaderLabel2" colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                ENTER PAYMENT REASON</td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 259px; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:DropDownList ID="cboType2" runat="server" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                OnDataBound="cboType2_DataBound" Width="70%">
                                    </asp:DropDownList></td>
                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:TextBox ID="txtBulkpaymentReason" runat="server" TextMode="MultiLine" Width="85%"></asp:TextBox>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 259px; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:CheckBox ID="chkCashout2" runat="server" Text="INCLUDE CASHOUT FEE" /></td>
                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="50%" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: center">
                                    <asp:Button ID="Button1" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="Button1_Click" Text="UPLOAD" Width="140px" /></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; text-align: center">
                                    </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: left">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 5px; text-align: center;">
                                    </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; height: 2px; text-align: center">
                                    Confirm Payemt And Proceed</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: left">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    text-align: center">
                                    <asp:Label ID="lblQn" runat="server" Font-Bold="True" ForeColor="Maroon" Text="."></asp:Label></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                    <asp:Button
                                        ID="btnYes" runat="server" Font-Bold="True" OnClick="btnYes_Click" Text="Yes"
                                        Width="109px" />&nbsp;
                                    <asp:Button ID="btnNo" runat="server" Font-Bold="True" OnClick="btnNo_Click"
                                            Text="No" Width="94px" /></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 18px; background-color: white; text-align: center">
                                    </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                    <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="Horizontal" HorizontalAlign="Justify"
                                        OnPageIndexChanged="DataGrid1_PageIndexChanged" Style="border-right: #617da6 1px solid;
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
                                            <asp:BoundColumn DataField="No." HeaderText="NO.">
                                                <HeaderStyle Width="5%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Contact" HeaderText="CONTACT" Visible="true">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Type" HeaderText="TYPE">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Charge" HeaderText="Charge">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Network" HeaderText="Network">
                                                <HeaderStyle Width="15%" />
                                                <ItemStyle Width="15%" />
                                            </asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    </asp:DataGrid></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 18px; background-color: white; text-align: center">
                                    </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Text="."></asp:Label>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                    <asp:Label ID="lblTotalCharge" runat="server" Font-Bold="True" Text="."></asp:Label></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center">
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View8" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                            style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 30%;
                                    height: 1px; background-color: white; text-align: center">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 30%;
                                    height: 1px; text-align: center">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: middle; height: 18px;
                                                text-align: center">
                                                DOWNLOAD REPORT</td>
                                        </tr>
                                        <tr>
                                            <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:DropDownList ID="cboFileFormat" runat="server" Font-Bold="True" Width="15%">
                                        <asp:ListItem Value="1">PDF</asp:ListItem>
                                        <asp:ListItem Value="2">EXCEL</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;<asp:Button ID="Button3" runat="server" Font-Bold="True" OnClick="Button3_Click"
                                        Text="Download" Width="17%" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:Button ID="Button2" runat="server" Font-Bold="True" OnClick="Button2_Click"
                                        Text="Return" Width="20%" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 30%;
                                    height: 18px; background-color: white; text-align: center">
                                    </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel" colspan="4" style="vertical-align: top; width: 30%;
                                    height: 1px; text-align: center">
                                    FAILED RECORD(S)</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 30%;
                                    height: 1px; background-color: white; text-align: center">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 30%;
                                    height: 2px; background-color: white; text-align: center">
                                    <table align="center" style="border-right: #b1b5b9 1px solid; border-top: #b1b5b9 1px solid;
                                        border-left: #b1b5b9 1px solid; width: 98%; border-bottom: #b1b5b9 1px solid">
                                        <tr>
                                            <td style="vertical-align: top; text-align: left">
                                                <asp:DataGrid ID="DataGrid6" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="Horizontal" OnPageIndexChanged="DataGrid6_PageIndexChanged"
                                                    PageSize="5" ShowFooter="True" Width="100%">
                                                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                                    <EditItemStyle BackColor="#999999"/>
                                                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"  />
                                                    <AlternatingItemStyle  BackColor="White" ForeColor="#284775"/>
                                                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"  />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="No." HeaderText="No.">
                                                            <HeaderStyle Width="5%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Code" HeaderText="Code">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                                            <HeaderStyle Width="15%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                                                            <HeaderStyle Width="45%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:DataGrid></td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; height: 19px; text-align: center">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding-top: 30px; text-align: center">
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; width: 870px; text-align: center">
                &nbsp;<asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblPath" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblBatchCode" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblBatchTotal" runat="server" Enabled="False" Text="0" Visible="False"></asp:Label></td>
        </tr>
    </table>
</asp:Content>





