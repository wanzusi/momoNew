<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="PayTypes, App_Web_szxjlzxw" title="PAYMENT TYPES" enableviewstatemac="false" %>
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
    <asp:MultiView ID="MultiView2" runat="server">
        <asp:View ID="View3" runat="server">
            <table style="width: 90%" align="center">
                <tr>
                    <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                        <asp:Button ID="btnCallList" runat="server" BorderStyle="Inset" Font-Bold="True"
                            Font-Names="Cambria" Font-Underline="False"
                            Text="PAYMENT TYPE LIST" OnClick="btnCallList_Click" /><asp:Button ID="btnAddDistrict" runat="server"
                                BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria" Font-Strikeout="False"
                                Font-Underline="False" OnClick="btnAddDistrict_Click" Text="ADD PAYMENT TYPE"
                                Width="148px" /></td>
                </tr>
            </table>
            <hr />
        </asp:View>
    </asp:MultiView>
    <asp:MultiView ID="MultiView3" runat="server">
        <asp:View ID="View4" runat="server">
            <table style="width: 90%" align="center">
                <tr>
                    <td style="width: 98%; height: 5px">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                            <tr>
                                <td class="InterfaceHeaderLabel">
                                    PAYMENT TYPE LIST</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 1px">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="width: 98%; height: 2px">
                        &nbsp;<asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            GridLines="Horizontal" HorizontalAlign="Justify" OnItemCommand="DataGrid1_ItemCommand" Style="border-right: #617da6 1px solid;
                            border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                            text-align: justify" Width="100%">
                            <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundColumn DataField="No." HeaderText="No.">
                                    <HeaderStyle Width="5%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PaymentCode" HeaderText="PaymentCode" Visible="False">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:ButtonColumn CommandName="btnEdit" DataTextField="PaymentCode" HeaderText="Edit"
                                    Text="PaymentCode">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Blue" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="PaymentType" HeaderText="Payment Type">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                        </asp:DataGrid></td>
                </tr>
            </table>
        </asp:View>
        &nbsp;
        <asp:View ID="View1" runat="server">
            <table align="center" style="width: 90%">
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                            <tr>
                                <td class="InterfaceHeaderLabel">
                                    ADD/MODIFY PAYMENT TYPE</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 1px; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="98%">
                            <tr>
                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tbody>
                                            <tr>
                                                <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                    PAYMENT TYPE DETAILS</td>
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
                                                    PAYMENT OPTIONS</td>
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
                                                Payment
                                                Code</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtPayCode" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                    Width="60%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                </td>
                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                Active</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow"><asp:CheckBox ID="chkActive" runat="server" Font-Bold="True" Text="Tick" /></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp">
                                                User</td>
                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                            </td>
                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtUser" runat="server" BackColor="#E0E0E0" CssClass="InterfaceTextboxLongReadOnly"
                                                    ReadOnly="True" Width="60%"></asp:TextBox></td>
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
                            OnClick="Button1_Click" Style="font: menu" Text="SAVE PAYMENT TYPE" Width="150px" /></td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 2px; text-align: center">
                        <hr />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>&nbsp;&nbsp;&nbsp;&nbsp;<br />
    
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

    <asp:Label ID="lblcode" runat="server" Text="0" Visible="False"></asp:Label><br />
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtPayCode" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
</asp:Content>



