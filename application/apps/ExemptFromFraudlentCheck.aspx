<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ExemptFromFraudlentCheck.aspx.cs" Inherits="ExemptFromFraudlentCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            EXEMPT FROM FRAUDLENT CHECK</td>
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
                            VENDOR</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            TYPE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            PHONE NUMBER</td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center;
                            height: 1px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" Style="font: menu" OnDataBound="cboVendor_DataBound">
                            </asp:DropDownList></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboType" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" Style="font: menu" OnSelectedIndexChanged="cboType_SelectedIndexChanged" AutoPostBack="True" OnDataBound="cboType_DataBound">
                                <asp:ListItem Text="One Number" Value="OTHER"></asp:ListItem>
                                <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtPhone" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 1px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                        </td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                        </td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Exempt" Width="85px" Style="font: menu" />
                        </td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                        </td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center;
                            border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px;
                            border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px;
                            border-right-color: #617da6;">
                        </td>
                    </tr>
                </table>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>

