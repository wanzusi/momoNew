<%@ page language="C#" masterpagefile="~/Reconcilation.master" autoeventwireup="true" inherits="ReconcileVASTransactions, App_Web_tk1t64pb" enableviewstatemac="false" %>
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
  
  <table cellpadding="0" cellspacing="0" class="style12" style="width: 90%">
        <tr>
            <td style="text-align: center; vertical-align: middle; height: 41px;">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                <tr>
                                    <td class="InterfaceHeaderLabel">
                                        VAS TRANSACTIONS FILE RECONCILIATION</td>
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
    <table style="width: 90%" align="center">
                    <tr>
                        <td style="width: 100%; text-align: center; height: 2px;"><table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                            <tr>
                                <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                                    <table style="width: 98%" align="center" cellpadding="0" cellspacing="0" >
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    VAS TRANSACTION TYPE</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left" >
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    FILE To process</td>
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
                                                Select</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:DropDownList ID="cboVASTxn" runat="server" CssClass="InterfaceDropdownList"
                                                    OnDataBound="cboVASTxn_DataBound" Style="font: menu" Width="95%">
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                File</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                                                </tr>
                                            </table>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="RECONCILE" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                    </tr>
                </table>      
 </asp:Content>

