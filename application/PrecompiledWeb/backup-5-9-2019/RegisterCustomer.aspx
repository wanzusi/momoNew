<%@ page language="C#" masterpagefile="~/Customers.master" autoeventwireup="true" inherits="RegisterCustomer, App_Web_vmk6cjzg" title="Register Customer" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Import
  Namespace="System.Threading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; text-align: center; width: 100%;">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                        style="width: 90%">
                                        <tr style="color: #000000">
                                            <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center">
                                                REGISTER / UPDATE CUSTOMER DETAILS</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" style="padding-bottom: 10px; vertical-align: top;
                                    text-align: center; width: 100%;">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                        style="width: 50%">
                                        <tr style="color: #000000">
                                            <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center">
                                                &nbsp;<asp:RadioButtonList ID="rbAccountType" runat="server" RepeatDirection="Horizontal"
                                    Width="100%" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; font: menu;" AutoPostBack="True" Font-Size="X-Small" OnSelectedIndexChanged="rbAccountType_SelectedIndexChanged">
                                </asp:RadioButtonList>
                                               </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; height: 2px; text-align: center; width: 664px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; text-align: center; width: 100%;">
                                    <asp:MultiView ID="MultiView1" runat="server">
                                        <asp:View ID="View1" runat="server">
                                            <br />
                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                                                style="width: 90%">
                                                <tr>
                                                    <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 15px;
                                                        background-color: white; text-align: left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2 InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 18px; text-align: center">
                                                RETAIL ACCOUNT</td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 24px; text-align: center">
                                                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                                            <tr>
                                                                <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                &nbsp;First
                                                Name</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%; height: 20px;">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtFname" runat="server" ></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                Last Name</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtLName" runat="server"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                Email</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                confirm Email</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtEmailReconf" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                Gender</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                <asp:RadioButtonList ID="rbnGender" runat="server" RepeatDirection="Horizontal"
                                                    Width="200px">
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                Phone</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtphone" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                Address</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                    Height="30px" TextMode="MultiLine"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                                </td>
                                                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                PassPort</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtPassport" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Driving Permit</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtDrivingPermit" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                OtherID</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtOtherID" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                Place of Work</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:TextBox ID="txtplaceofwork" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                    Height="30px" TextMode="MultiLine"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Pegasus Charge Type</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                                                <asp:DropDownList ID="cboChargeType" runat="server" CssClass="InterfaceDropdownList"
                                                                                    OnDataBound="cboChargeType_DataBound" Style="font: menu" Width="95%">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Pegasus Charge</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                                                <asp:TextBox ID="txtPegpayCharge" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Active</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                <asp:CheckBox ID="chkActive" runat="server" /></td>
                                                                        </tr>
                                                                    </table>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" rowspan="1" style="vertical-align: top;
                                                        height: 20px; text-align: center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 19px; text-align: center">
                                                <asp:Button ID="btnSave" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                                Height="30px" Text="SAVE" Width="140px" OnClick="btnSave_Click" />
                                                <asp:Label ID="lblun" runat="server" Font-Bold="True" ForeColor="#FF0000" Visible="False"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        &nbsp;
                                        <asp:View ID="View2" runat="server">
                                            <br />
                                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                                                style="width: 90%">
                                                <tr>
                                                    <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 15px;
                                                        background-color: white; text-align: left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterfaceHeaderLabel2 InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 18px; text-align: center">
                                                        CORPORATE &nbsp;ACCOUNT</td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 24px; text-align: center">
                                                        <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                                            <tr>
                                                                <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                Customer Name</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtFname2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                        Contact Person</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                                                Email</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtEmail2" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp">
                                                        Reconfirm Email</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow">
                                                        <asp:TextBox ID="txtEmailReconf2" runat="server"  CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                                                </td>
                                                                <td style="vertical-align: top; width: 48%; height: 5px; text-align: left">
                                                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Contact</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                        <asp:TextBox ID="txtPhone2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Address</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"
                                                            Height="30px" TextMode="MultiLine"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Pegasus Charge Type</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                                                <asp:DropDownList ID="cboChargeType2" runat="server" CssClass="InterfaceDropdownList" OnDataBound="cboChargeType2_DataBound"
                                                                                    Style="font: menu" Width="60%">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Pegasus Charge</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                                                <asp:TextBox ID="txtPegPayCharge2" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRowUp" style="height: 20px">
                                                                                Active</td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 20px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRow" style="height: 20px">
                                                        <asp:CheckBox ID="chkActive2" runat="server" /></td>
                                                                        </tr>
                                                                    </table>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" rowspan="1" style="vertical-align: top;
                                                        height: 20px; text-align: center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                                        height: 19px; text-align: center">
                                                        <asp:Button ID="Button2" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                                Height="30px" Text="SAVE" Width="140px" OnClick="Button2_Click" />&nbsp;
                                                        <asp:Label ID="lblun2" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; padding-top: 30px; text-align: center; width: 664px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-bottom: 10px; vertical-align: top; text-align: center; height: 71px; width: 664px;">
                                    <asp:Label ID="lblSave" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                                    <asp:Label ID="lblusername" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblpassword" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblIBAccountActive" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblEmailSent" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblMBAccountActive" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <asp:Label ID="lblNewPassword" runat="server" Text="Label" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblStatus" runat="server" Text="Label" Visible="False"></asp:Label>
                                    <ajaxToolkit:ScriptManager ID="ScriptManager1" runat="server">
                                    </ajaxToolkit:ScriptManager>
                                    <asp:Label ID="Label4" runat="server" Text="0" Visible="False"></asp:Label></td>
                            </tr>
                        </table>

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

    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        TargetControlID="txtphone" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
    <br />
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        TargetControlID="txtPhone2" ValidChars="0123456789">
    </ajaxToolkit:FilteredTextBoxExtender>
</asp:Content>

