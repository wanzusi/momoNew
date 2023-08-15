<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="ReverseTelecomTransaction, App_Web_rnqohcbv" title="Untitled Page" enableviewstatemac="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" style="width: 90%">
        <tr>
            <td style="width: 100%; height: 2px; text-align: center">
                <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                    <tr>
                        <td colspan="3" style="vertical-align: top; height: 5px; text-align: left">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel" style="text-align: center;">
                                        Reverse TRANSACTION</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="vertical-align: top; height: 5px; text-align: left">
                           <center><table style="width: 60%; margin-left:20%; margin-right:20%">
                                <tr>
                                    <td style="height: 21px">
                                        Company</td>
                                    <td style="height: 21px">
                                        Pegpay Id</td>
                                    <td style="height: 21px">
                                        Telecom Id</td>
                                </tr>
                                <tr>
                                    <td style="height: 21px; Width=40%">
                                        <asp:DropDownList ID="dll_vendorCode" runat="server" Width="100%">
                                        </asp:DropDownList></td>
                                    <td style="height: 21px; Width=30%">
                                        <asp:TextBox ID="txt_pegpayId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                          ></asp:TextBox></td>
                                    <td style="height: 21px; Width=30%">
                                        <asp:TextBox ID="txtm_telecomId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                            ></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                    </td>
                                    <td style="height: 21px">
                                        <asp:Button ID="btn_findit" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                    OnClick="btnfindit_Click" Style="font: menu" Text="FIND IT" Width="150px" /></td>
                                    <td style="height: 21px">
                                    </td>
                                </tr>
                            </table></center> 
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
                        <td style="vertical-align: top; width: 50%; height: 10px; text-align: left">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                <tr>
                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                        PegPayId</td>
                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                    </td>
                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                        <asp:TextBox ID="txtPegpayId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                            Enabled="False" Width="60%"></asp:TextBox>
                                        <asp:Label ID="lbl_amount" runat="server" Text="Label" Visible="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                        Amount</td>
                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                        &nbsp;</td>
                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                            Width="60%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                        Record Date</td>
                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                    </td>
                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                        <asp:Label ID="lbl_recorddate" runat="server" Text="-"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                        </td>
                        <td style="vertical-align: top; width: 48%; height: 10px; text-align: center">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                <tr>
                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                        Telecom Id&nbsp;</td>
                                    <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                    </td>
                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                        <asp:TextBox ID="txt_telecomId" runat="server" CssClass="InterfaceTextboxLongReadOnly"
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
                <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                    OnClick="Button1_Click" Style="font: menu" Text="REVERSE TRANSACTION" Width="150px" /></td>
        </tr>
        <tr>
            <td style="width: 100%; height: 2px; text-align: center">
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
