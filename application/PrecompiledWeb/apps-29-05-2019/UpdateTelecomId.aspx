<%@ page language="C#" masterpagefile="~/Reconcilation.master" autoeventwireup="true" inherits="UpdateTelecomId, App_Web_njxbuc3y" title="UpdateTelecomId" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            UPDATE TRANSACTIONS</td>
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid;
                    border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            NETWORK</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            PEGPAY ID</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            TELECOM ID</td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center;
                            height: 1px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboNetwork" runat="server" CssClass="InterfaceDropdownList"
                                Width="85%" Style="font: menu" OnDataBound="cboNetwork_DataBound">
                            </asp:DropDownList></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtpegpayid" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="TelecomId" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" Text="Update"
                    OnClientClick="return confirm('Do you really want to update?');" OnClick="btnUpdate_Click"
                    Width="85px" Style="font: menu" /></td>
        </tr>
        <tr>
            
                <td style="width: 98%; height: 1px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table>
                                <tr>
                                    <td style="height: 22px">
                                        Sent To Telecom</td>
                                    <td style="width: 322px; height: 22px">
                                        <asp:CheckBox ID="CbxSentToMomo" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        Sent To Pegpay</td>
                                    <td style="width: 322px">
                                        <asp:CheckBox ID="CbxSentToPegpay" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        Sent To School</td>
                                    <td style="width: 322px">
                                        <asp:CheckBox ID="CbxSentToSchool" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        Payment Id</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txtPaymentId" runat="server" Enabled="False" Width="231px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Payment Amount</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txtPaymentAmount" runat="server" Enabled="False" Width="231px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="width: 322px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Momo Amount</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txtMomoAmount" runat="server" Width="231px" Enabled="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        MomoId</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txt_momoId" runat="server" Width="231px" Enabled="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Telecom Id</td>
                                    <td style="width: 322px">
                                        <asp:TextBox ID="txt_TelecomIds" runat="server" Width="231px" Enabled="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="width: 322px">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancle" Width="157px" ForeColor="red"
                                            OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnFinish" runat="server" Text="Finish Operation" Width="157px" ForeColor="green"
                                            OnClick="btnFinish_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                    <hr />
                </td>
        </tr>
    </table>
    
    <br />
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
</asp:Content>
