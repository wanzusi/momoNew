<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="PegasusPayments, App_Web_o7vnt00f" title="NEW PAYMENT" culture="auto" uiculture="auto" enableviewstatemac="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table cellpadding="0" cellspacing="0" class="style12">
        <tr>
            <td style="width: 100%">
            </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
                <tr>
                    <td style="text-align: center; vertical-align: middle; height: 41px;">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                            <tr>
                                <td class="InterfaceHeaderLabel">
                                    CHECK OUT</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 90%" align="center">
                <tr>
                    <td style="width: 100%; text-align: center; height: 2px;">
                        <table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                            <tr>
                                <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                                    <table style="width: 98%" align="center" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    TRANSACTION DETAILS</td>
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
                                            <td class="InterFaceTableLeftRowUp">
                                                Item Description</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 5px">
                                                Total Price for Item</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 5px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 5px">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Customers Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtcontact" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                        <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" Text="PAY FOR ITEM"
                            Width="150px" Font-Bold="True" OnClick="btnOK_Click" Style="font: menu" /></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />
</asp:Content>
