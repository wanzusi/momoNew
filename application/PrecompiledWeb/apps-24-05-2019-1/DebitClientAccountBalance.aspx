<%@ page language="C#" masterpagefile="~/AccountantMaster.master" autoeventwireup="true" inherits="DebitClientAccountBalance, App_Web_xkrnfbe6" title="Debit-Client-AccountBalance" enableviewstatemac="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            DEBIT
                            CLIENT</td>
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            AGENT</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            AGENT-ACCOUNT NUMBER</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            AGENT-ACCOUNT BALANCE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            AMOUNT TO DEBIT</td>
                        
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboVendor" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" style="font: menu" OnDataBound="cboVendor_DataBound" OnSelectedIndexChanged="cboVendor_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtAgentAccountNumber"  Enabled="False" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
                            </td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtAgentAccountBalance" Enabled="False" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
                            </td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtAgentDebitAmount"  Font-Bold="True" runat="server" onkeyup = "javascript:this.value=Comma(this.value);" Style="font: menu" Width="90%" ForeColor="#C00000"></asp:TextBox>
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
                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                     <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            PEGASUS ACCOUNT NAME</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            PEGASUS-ACCOUNT NUMBER</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            PEGASUS-ACCOUNT BALANCE</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                            NETWORK</td>
                        
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                    </tr>
                    <tr>
                     <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:DropDownList ID="cboPegasusAccountName" runat="server" CssClass="InterfaceDropdownList"
                                Width="95%" style="font: menu" OnDataBound="cboPegasusAccountName_DataBound" OnSelectedIndexChanged="cboPegasusAccountName_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtPegasusAccountNo"  Enabled="False" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
                            </td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="txtPegasusAccountBalance" Enabled="False" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
                            </td>
                        <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            width: 17%; border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            <asp:TextBox ID="cboNetwork" Enabled="False" runat="server" Style="font: menu" Width="90%"></asp:TextBox>
                            </td>
                    </tr>
                </table>
                 <asp:Button ID="btnDebit" runat="server" Font-Size="9pt" Height="23px" OnClick="btnDebit_Click"
                                Text="DEBIT" Width="85px" style="font: menu" 
                                OnClientClick="return confirm('Do you really want to debit the client?');" /></td>
          
        </tr>
        
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
            </td>
        </tr>
       
        
    </table>
    <br />
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>
    <br />
    
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        Visible="False" />
         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                            
                            ValidChars = ",0123456789"
                             
                            TargetControlID="txtAgentDebitAmount">
                        </ajaxToolkit:FilteredTextBoxExtender>
        <script type ="text/javascript">
 function Comma(Num)
 {
       Num += '';
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       Num = Num.replace(',' , '');Num = Num.replace(',' , '');Num = Num.replace(',' , '');
       x = Num.split('.');
       x1 = x[0];
       x2 = x.length > 1 ? '.' + x[1] : '';
       var rgx = /(\d+)(\d{3})/;
       while (rgx.test(x1))
       x1 = x1.replace(rgx, '$1' + ',' + '$2');
       return x1 + x2;
 } 
    
   </script> 
</asp:Content>

