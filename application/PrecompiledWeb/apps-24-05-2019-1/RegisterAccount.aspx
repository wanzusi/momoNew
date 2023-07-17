<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="AddCorporateBeneficiary, App_Web_j01zgch7" title="ACCOUNTS" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center; height: 50px;">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center; height: 19px;">
                            EDIT/ADD&nbsp; ACCOUNT</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 70%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center; height: 19px;">
                                    Search For Company</td>
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
                                                Company Name</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                                Company Code</td>
                                            <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 25%; height: 18px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                                text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                &nbsp;<asp:TextBox ID="txtsearchName" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
                                            <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                width: 25%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                    <asp:TextBox ID="txtSearchCode" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                        Width="90%"></asp:TextBox></td>
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
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 60%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: middle; height: 18px;
                                                text-align: center">
                                    Select Company</td>
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
                            <asp:DropDownList ID="cboCompanyCode" runat="server" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                OnDataBound="cboCompanyCode_DataBound" Width="55%" AutoPostBack="True" OnSelectedIndexChanged="cboCompanyCode_SelectedIndexChanged">
                            </asp:DropDownList>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center">
                                    </td>
                            </tr>
                        </table>
                            </asp:View>
                            &nbsp;
                        </asp:MultiView>
                                    <asp:MultiView ID="MultiView2" runat="server">
                                        <asp:View ID="View2" runat="server">
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
                                                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
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
                                                                                <asp:TextBox ID="txtAccountName" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                                </td>
                                                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Account Number</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px; width: 66%;">
                                                                                <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Account
                                                                                Type
                                                                            </td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px; width: 66%;">
                                                                                <asp:DropDownList ID="cboAccountType" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                                    OnDataBound="cboAccountType_DataBound" Width="90%" AutoPostBack="True" OnSelectedIndexChanged="cboAccountType_SelectedIndexChanged">
                                                                                <asp:ListItem>--Select Account Type--</asp:ListItem>
                                                                                <asp:ListItem Value="ESCROW">ESCROW Account</asp:ListItem>
                                                                                <asp:ListItem Value="COMMISSION">Commission Account</asp:ListItem>
                                                                                <asp:ListItem Value="AIRTIMESUSP">AirTime Suspense</asp:ListItem>
                                                                            </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Network</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px; width: 66%;">
                                                                                <asp:DropDownList ID="cboNetwork" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                                                    OnDataBound="cboNetwork_DataBound" Width="90%">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Active</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px; width: 66%;">
                                                                                <asp:CheckBox ID="chkActive" runat="server" /></td>
                                                                        </tr>
                                                                    </table>
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
                                                        <asp:Button ID="btnSave" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="btnSave_Click" Text="SAVE ACCOUNT" Width="140px" />
                                                        <asp:Button ID="btnCancel" runat="server" Font-Bold="True" Text="RETURN" Width="140px" OnClick="btnCancel_Click" /></td>
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
        <tr>
            <td style="vertical-align: top; padding-top: 30px; text-align: center; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; width: 870px; text-align: center">
                &nbsp;<asp:Label ID="lblCompanyCode" runat="server" Text="0" Visible="False"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label></td>
        </tr>
    </table>
</asp:Content>





