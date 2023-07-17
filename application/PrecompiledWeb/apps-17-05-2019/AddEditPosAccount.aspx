<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="AddPosOwnerKYC, App_Web_bs7zgocn" title="Untitled Page" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center; height: 50px;">
                &nbsp;<table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
                    <tr>
                        <td style="vertical-align: middle; height: 41px; text-align: center">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel">
                                        CREATE/EDIT POS ACCOUNT</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table align="center" style="width: 90%">
                                    <tr>
                                        <td style="width: 100%; height: 2px; text-align: center">
                                            <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                                <tr>
                                                    <td style="vertical-align: top; width: 71%; height: 5px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                        ACCOUNT DETAILS</td>
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
                                                    <td style="vertical-align: top; width: 71%; height: 5px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px">
                                                                    Code</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtVendorCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%" Enabled="False"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px">
                                                                    Trading Name</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtFname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="height: 20px; width: 218px;">
                                                                    Address</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtLname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px; height: 20px;">
                                                                    </td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px">
                                                                    </td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px; height: 20px">
                                                                    User Name</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px">
                                                                    Password</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px; height: 20px">
                                                                    Spid</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    <asp:TextBox ID="txtSpid" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                        Width="60%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px; height: 20px">
                                                                    </td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px; height: 21px">
                                                                    </td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 21px">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 21px">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp" style="width: 218px; height: 20px;">
                                                                    </td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                                </td>
                                                                <td class="InterFaceTableRightRow" style="height: 20px">
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="width: 100%; height: 30px; text-align: center">
                                            <asp:Button ID="btnOK" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                OnClick="btnOK_Click" Style="font: menu" Text="SAVE DETAILS" Width="150px" /></td>
                                    </tr>
                                </table>
                            </asp:View>
                            &nbsp;
                        </asp:MultiView>
                                    </td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding-top: 30px; text-align: center; height: 1px;">
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; width: 870px; text-align: center">
                &nbsp;<asp:Label ID="lblCompanyCode" runat="server" Text="0" Visible="False"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblVendorCode" runat="server" Text="0" Visible="False"></asp:Label></td>
        </tr>
    </table>


</asp:Content>

