<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="TransferFunds, App_Web_xqryrb4f" title="TRANFER FUNDS" enableviewstatemac="false" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="padding-bottom: 10px; vertical-align: top; height: 50px; text-align: center">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                            style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                                    text-align: center">
                                    TRANSFER FUNDS</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                        <asp:MultiView ID="MultiView2" runat="server">
                            <asp:View ID="View2" runat="server">
                                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                                    style="width: 90%">
                                    <tr style="color: #000000">
                                        <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                            background-color: white; text-align: left">
                                        </td>
                                    </tr>
                                    <tr style="color: #000000">
                                        <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                            height: 24px; text-align: left">
                                            <table id="TABLE1" align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                                border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: middle; height: 18px;
                                                        text-align: center">
                                                        Fun TransFEr Account Details</td>
                                                </tr>
                                                <tr>
                                                    <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                                        text-align: center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 50%;
                                                        height: 18px; text-align: center">
                                                        &nbsp;PeGASUS ACCOUNT</td>
                                                    <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 50%;
                                                        height: 18px; text-align: center">
                                                        PeGASUS ACCOUNT Balance</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                        <table align="center" style="width: 80%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 50%; text-align: center">
                                        <asp:TextBox ID="txtPegpayAccount" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 50%; text-align: center">
                                        <asp:TextBox ID="txtPegPayBalance" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
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
                                                        Select Account : &nbsp;&nbsp;
                                                        <asp:DropDownList ID="cboCustomerAccount" runat="server" Width="35%" OnDataBound="cboCustomerAccount_OnDataBound">
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                        Amount To Transfer: &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtAmount" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Width="40%" Font-Bold="True" ForeColor="Red" ></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                        </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="color: #000000">
                                        <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                                            vertical-align: top; padding-top: 25px; height: 17px; text-align: center">
                                            <asp:Button ID="btnSave" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="btnSave_Click" Text="Process Tranfer" Width="140px" /></td>
                                    </tr>
                                </table>
                            </asp:View>
                            &nbsp;&nbsp;&nbsp;<asp:View ID="View3" runat="server"><table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                                    style="width: 90%">
                                <tr style="color: #000000">
                                    <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; height: 2px; text-align: center">
                                        Confirm Payemt And Proceed</td>
                                </tr>
                                <tr style="color: #000000">
                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                        height: 24px; text-align: center">
                                    </td>
                                </tr>
                                <tr style="color: #000000">
                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                        height: 24px; text-align: center"><table id="Table2" align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                                border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: middle; height: 18px;
                                                        text-align: center">
                                                    Fun TransFEr Account Details</td>
                                            </tr>
                                            <tr>
                                                <td class="ddcolortabsline2" colspan="4" style="vertical-align: middle; height: 1px;
                                                        text-align: center">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 50%;
                                                        height: 18px; text-align: center">
                                                    &nbsp;PeGASUS ACCOUNT</td>
                                                <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 50%;
                                                        height: 18px; text-align: center">
                                                    PeGASUS ACCOUNT Balance</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                    <table align="center" style="width: 80%">
                                                        <tr>
                                                            <td class="InterFaceTableLeftRowUp" style="width: 50%; text-align: center">
                                        <asp:TextBox ID="txtViewPegPayAccount" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
                                                            <td class="InterFaceTableLeftRowUp" style="width: 50%; text-align: center">
                                        <asp:TextBox ID="txtViewPegpayBalance" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False"></asp:TextBox></td>
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
                                                    Select Account : &nbsp;
                                                    <asp:DropDownList ID="cboviewAccount" runat="server" Width="35%" OnDataBound="cboviewAccount_OnDataBound" Enabled="False">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                    Amount To Transfer: &nbsp;&nbsp;
                                                    <asp:TextBox ID="txtViewAmount" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" Enabled="False" Width="40%" Font-Bold="True" ForeColor="Red"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="vertical-align: middle; height: 18px; text-align: center">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style="color: #000000">
                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                            height: 17px; text-align: center">
                                        &nbsp;</td>
                                </tr>
                                <tr style="color: #000000">
                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                                            vertical-align: top; padding-top: 25px; text-align: center">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="btnCancel_Click" Text="NO" Width="140px" />
                                                    <asp:Button ID="btnConfirm" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="btnConfirm_Click" Text="YES" Width="140px" /></td>
                                </tr>
                            </table>
                            </asp:View>
                        </asp:MultiView>
                        <asp:Label ID="lblCustName" runat="server" Visible="False"></asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: top; padding-top: 30px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px; vertical-align: top; width: 870px; text-align: center">
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                            TargetControlID="txtAmount" ValidChars="0123456789">
                        </ajaxToolkit:FilteredTextBoxExtender>
</asp:Content>

