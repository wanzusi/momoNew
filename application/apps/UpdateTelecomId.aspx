<%@ Page Language="C#" MasterPageFile="~/NewReconciliation.master" AutoEventWireup="true"
    CodeFile="UpdateTelecomId.aspx.cs" Inherits="UpdateTelecomId" Title="UPDATE TELECOM ID" %>
<%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    0<table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                    <tr>
                        <td class="InterfaceHeaderLabel">
                            UPDATE TRANSACTIONS</td>
                    </tr>
                </table>
                  <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 50%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center">
                            <asp:RadioButtonList ID="rbnMethod" runat="server" AutoPostBack="True"
                                Font-Bold="True" OnSelectedIndexChanged="rbnMethod_SelectedIndexChanged" RepeatDirection="Horizontal" 
                                Width="92%" >
                                <asp:ListItem Value="0">One By One UPDATE</asp:ListItem>
                                <asp:ListItem Value="1">BULK UPLOAD</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
            </td>
        </tr>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
            </td>
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
                        <asp:View ID="View2" runat="server">
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
                            </asp:View>
                          <asp:View ID="View3" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center">
                                    MAKE A BULK UPLOAD</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; text-align: left">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                            text-align: center; height: 22px;">
                                    <table align="center" cellpadding="0" cellspacing="0" style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; border-left: #617da6 1px solid; width: 80%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td class="InterfaceHeaderLabel2" colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                               border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                SELECT FILE TO UPLOAD</td>

                                            <td class="InterfaceHeaderLabel2" colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                 border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:FileUpload ID="FileUpload1" runat="server" Width="50%" /></td>
                                            <td colspan="1" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                                                border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                                                border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                                                border-right-color: #617da6">
                                                <asp:Button ID="Button1" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True" OnClick="Upload_Click" Text="UPLOAD" Width="140px" />
                                                &nbsp;</td>
                                        </tr>

                                    </table>
                                </td>
                                <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; text-align: left">
                                </td>
                            </tr>
                                <%--<tr style="color: #000000">--%>
                                <tr>
                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                            text-align: center; height: 22px;">
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 " style="width: 90%">
                                            <tr style="color: #000000">
                                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                                    <%--<asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="Horizontal" HorizontalAlign="Justify" OnPageIndexChanged="DataGrid2_PageIndexChanged" OnSelectedIndexChanged="DataGrid2_SelectedIndexChanged" Style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                                        text-align: justify" Width="75%">
                                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                                        <EditItemStyle BackColor="#999999" />
                                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                                        <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="Network" HeaderText="Network">
                                                                <HeaderStyle Width="5%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="PegPayId" HeaderText="PegPayId">
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="TelecomId" HeaderText="TelecomId" Visible="true">
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                                    </asp:DataGrid>--%>
                                                </td>
                                            </tr>
                                            <tr style="color: #000000">
                                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 18px; background-color: white; text-align: center"></td>
                                            </tr>
                                        </table>
                                        <%--<asp:Button ID="Button2" runat="server" Font-Size="9pt" Height="23px" Text="Update"
                    OnClientClick="return confirm('Do you really want to update?');" OnClick="btnUpdate_Click"
                    Width="85px" Style="font: menu" />--%></td>
                                </tr>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 5px; text-align: center;">
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
<%--    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </ajaxToolkit:ToolkitScriptManager>--%>
    <br />
</asp:Content>
