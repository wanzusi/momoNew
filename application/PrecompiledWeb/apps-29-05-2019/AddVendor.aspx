<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="AddVendor, App_Web_kmhtkbhf" title="NEW VENDOR" culture="auto" uiculture="auto" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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
                                        CREATE AGENT</td>
                                </tr>
                            </table>
            </td>
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
                                                    AGENT DETAILS</td>
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
                                                    System Accessiblity Details</td>
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
                                                Code</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Bill System Code</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtBillSystemCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Contact Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtcontact" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Relationship Manager</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                             <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtAccountManager" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Relationship Representative</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                             <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtAccountRep" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    <asp:TextBox ID="minBalance" runat="server" Visible="False"></asp:TextBox></td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Email</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                        Confirm Email</td>
                                                    <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                    </td>
                                                    <td class="InterFaceTableRightRow" style="height: 20px">
                                                        <asp:TextBox ID="txtconfirmemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                            Width="60%"></asp:TextBox></td>
                                                </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Pegasus Charge Type</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:DropDownList ID="cboChargeType" runat="server" CssClass="InterfaceDropdownList"
                                                    OnDataBound="cboChargeType_DataBound" Style="font: menu" Width="95%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Pegasus Charge</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtPegPayCharge" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr><%--
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp">
                                                        Is Active</td>
                                                    <td class="InterFaceTableMiddleRowUp">
                                                    </td>
                                                    <td class="InterFaceTableRightRow"><asp:CheckBox ID="chkIsActive" runat="server" Text="Tick To Activate" Font-Bold="True" /></td>
                                                </tr>
                                        <tr>--%>
                                            <td class="InterFaceTableLeftRowUp">
                                                Is Required Certificate&nbsp;</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:CheckBox ID="chkcert" runat="server" Text="Tick If Certificate" Font-Bold="True" /></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                User</td>
                                            <td class="InterFaceTableMiddleRowUp">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtUser" runat="server" BackColor="#E0E0E0" CssClass="InterfaceTextboxLongReadOnly"
                                                    ReadOnly="True" Width="60%"></asp:TextBox></td>
                                        </tr>
                                            </table>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 2px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 5px; text-align: center">
                                    <asp:MultiView ID="MultiView2" runat="server">
                                        <asp:View ID="View2" runat="server">
                                            <table cellpadding="0" cellspacing="0" class="style12" align="center" width="92%">
                                                <tr>
                                                    <td style="width: 50%; vertical-align: top; height: 5px; text-align: left;">
                                                        <table style="width: 98%" align="center" cellpadding="0" cellspacing="0" >
                                                            <tbody>
                                                                <tr>
                                                                    <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                        Password</td>
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
                                                                        Email</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="vertical-align: top; height: 1px; text-align: left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; width: 50%; height: 10px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp">
                                                                    Reset</td>
                                                                <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:CheckBox ID="chkResetPassword" runat="server" Text="Tick To Reset" Font-Bold="True" AutoPostBack="True" OnCheckedChanged="chkResetPassword_CheckedChanged" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                    </td>
                                                    <td style="vertical-align: top; width: 48%; height: 10px; text-align: left">
                                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                            <tr>
                                                                <td class="InterFaceTableLeftRowUp">
                                                                    Resend</td>
                                                                <td class="InterFaceTableMiddleRowUp">
                                                                </td>
                                                                <td class="InterFaceTableRightRow">
                                                                    <asp:CheckBox ID="chkResend" runat="server" Text="Tick To Resend" Font-Bold="True" AutoPostBack="True" OnCheckedChanged="chkResend_CheckedChanged" /></td>
                                                            </tr>
                                                        </table>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="vertical-align: top; height: 5px; text-align: center" class="InterFaceTableLeftRowUp">
                                            </td>
                                                </tr>
                                            </table>
                                            </asp:View>
                                    </asp:MultiView></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; height: 5px; text-align: center">
                                    <table style="width: 80%" align="center">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 35%">
                                                Browse PegPay Certificate</td>
                                            <td class="InterFaceTableLeftRowUp" style="width: 65%">
                                                <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center; height: 30px;" class="InterFaceTableLeftRowUp">
                                                                <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px"
                                                                    Text="SAVE AGENT" Width="150px" Font-Bold="True" OnClick="btnOK_Click" style="font: menu" /></td>
                    </tr>
                </table>
        </asp:View>
        &nbsp;
    </asp:MultiView>
                <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label><br />

</asp:Content>

