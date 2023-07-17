<%@ Page Title="" Language="C#" MasterPageFile="~/NewReconciliation.master" AutoEventWireup="true" CodeFile="ActivateRecon.aspx.cs" Inherits="ActivateRecon" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Import
    Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="text-align: center; vertical-align: middle; height: 41px;">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">ACTIVATE RECONCILIATION</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" class="style12">
        <tr>
            <td style="width: 100%"></td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View2" runat="server">
            <table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                <tr>
                    <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                        <table style="width: 98%" align="center" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td class="InterfaceHeaderLabel2" style="height: 18px; text-align: center;">Activation Section</td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="vertical-align: top; height: 1px; text-align: left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 50%; height: 10px; text-align: left">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                            <tr>
                                <td class="InterFaceTableLeftRowUp">Activate</td>
                                <td class="InterFaceTableMiddleRowUp" style="width: 2%"></td>
                                <td class="InterFaceTableRightRow">
                                    <asp:CheckBox ID="chkactivate" runat="server" Text="Tick To Activate" Font-Bold="True" AutoPostBack="True" OnCheckedChanged="chkResetPassword_CheckedChanged" /></td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top; width: 2%; height: 10px; text-align: center"></td>
                </tr>
                <tr>
                    <td colspan="3" style="vertical-align: top; height: 5px; text-align: center" class="InterFaceTableLeftRowUp"></td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                        <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                            Text="SAVE" Width="150px" Font-Bold="True" OnClick="btnOK_Click" Style="font: menu" /></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

