<%@ page language="C#" masterpagefile="~/Main.master" autoeventwireup="true" inherits="AddCorporateBeneficiary, App_Web_6l6kiw0m" title="CUSTOMER BENEFICIARIES" enableviewstatemac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center; height: 19px;">
                            ADD CUSTOMER</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center">
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                    style="width: 40%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; text-align: center">
                            <asp:RadioButtonList ID="rbnMethod" runat="server" AutoPostBack="True"
                                Font-Bold="True" OnSelectedIndexChanged="rbnMethod_SelectedIndexChanged" RepeatDirection="Horizontal"
                                Width="92%">
                                <asp:ListItem Value="0">ONE BY ONE</asp:ListItem>
                                <asp:ListItem Value="1">BULK UPLOAD</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; text-align: center; height: 905px;">
                &nbsp;<asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View5" runat="server">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center; height: 16px;">
                                    Enter CUSTOMER DETAILS</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style12" width="92%">
                                        <tr>
                                            <td style="vertical-align: top; width: 50%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel2" style="height: 18px">
                                                                CUSTOMER DETAILS</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 5px; text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 4px; text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 100%; height: 5px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">
                                                            &nbsp;Customer ID</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtCode" runat="server" 
                                                                CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" 
                                                                Width="438px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">
                                                            Full Name</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                            <asp:TextBox ID="txtFname" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" 
                                                                Width="439px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">
                                                            Customer Type</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                                                            <asp:TextBox ID="txtType" runat="server" 
                                                                CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" 
                                                                Width="438px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp">Active</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableRightRow">
                            <asp:CheckBox ID="chkActive" runat="server" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center; height: 16px;">
                                    <asp:Button ID="btnSave" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                                Height="30px" OnClick="btnSave_Click" Text="SAVE" Width="140px" Visible="true" /></td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top;
                                    text-align: center">
                                    UPLOAD BENEFICIARY FILE</td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    text-align: center">
                                </td>
                            </tr>
                            
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: left">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: left">
                                    <table align="center" style="width: 80%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="width: 35%">
                                                Browse Beneficiary File:</td>
                                            <td class="InterFaceTableLeftRowUp" style="width: 65%">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="70%" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center; height: 13px;">
                                    <asp:Button ID="Button1" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                                Height="30px" OnClick="Button1_Click" Text="UPLOAD" Width="140px" /></td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    text-align: center; height: 20px;">
                                    <asp:Label ID="lblQn" runat="server" Font-Bold="True" ForeColor="Maroon" Text="."></asp:Label></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                <asp:Button
                                        ID="btnYes" runat="server" Font-Bold="True" OnClick="btnYes_Click" Text="Yes"
                                        Width="109px" /><asp:Button ID="btnNo" runat="server" Font-Bold="True" OnClick="btnNo_Click"
                                            Text="No" Width="94px" /></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterFaceTableLeftRowUp InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                            vertical-align: top; padding-top: 25px; text-align: center"><asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="Horizontal" HorizontalAlign="Justify" PageSize="3" Style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                                        text-align: justify" Width="100%">
                                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                <EditItemStyle BackColor="#999999" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                            Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                                <Columns>
                                    <asp:BoundColumn DataField="CustomerCode" HeaderText="CustomerCode">
                                        <HeaderStyle Width="10%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Name" HeaderText="Name">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TypeCode" HeaderText="TypeCode">
                                        <HeaderStyle Width="30%" />
                                        <ItemStyle Width="80px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Reason" HeaderText="Name At Telecom">
                                        <HeaderStyle Width="30%" />
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                            </asp:DataGrid></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></asp:View>
                    <asp:View ID="View6" runat="server">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View4" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                    style="width: 90%">
                            <tr>
                                <td class="InterfaceHeaderLabel2" colspan="4" style="vertical-align: top; height: 2px; text-align: center">
                                    Rejected Records</td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    text-align: center; height: 20px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    text-align: center; height: 20px;">
                                    <table align="center" style="border-right: #617da6 1px solid; border-top: #617da6 1px solid;
                                        border-left: #617da6 1px solid; width: 50%; border-bottom: #617da6 1px solid">
                                        <tr>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:RadioButton ID="rdPdf" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                    Text="PDF" /></td>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:RadioButton ID="rdExcel" runat="server" Font-Bold="True" GroupName="FileFormat"
                                                    Text="EXCEL" /></td>
                                            <td style="border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid;
                                                width: 100px; border-bottom: #617da6 1px solid">
                                                <asp:Button ID="btnConvert" runat="server" Font-Size="9pt" Height="23px" OnClick="btnConvert_Click"
                                                    Style="font: menu" Text="Convert" Width="85px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    text-align: center">
                                    <asp:Button ID="btnReturn" runat="server" Font-Size="9pt" Height="23px" OnClick="btnReturn_Click"
                                        Style="font: menu" Text="Return" Width="105px" /></td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableRightRow" colspan="4" style="vertical-align: top; width: 100%;
                                    height: 2px; background-color: white; text-align: center">
                                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="Horizontal" HorizontalAlign="Justify" PageSize="3" Style="border-right: #617da6 1px solid;
                                        border-top: #617da6 1px solid; font: menu; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;
                                        text-align: justify" Width="100%">
                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                        <EditItemStyle BackColor="#999999" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#003366" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="White" HorizontalAlign="Center"
                                            Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <ItemStyle BackColor="InactiveCaption" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CustomerCode" HeaderText="CustomerCode">
                                                <HeaderStyle Width="5%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Name" HeaderText="Name">
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TypeCode" HeaderText="TypeCode">
                                                <HeaderStyle Width="30%" />
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Reason" HeaderText="Reason">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#FEFECE" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" ForeColor="Black" />
                                    </asp:DataGrid></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView></asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="padding-bottom: 10px; vertical-align: top; width: 870px; text-align: center">
                &nbsp;<asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblPath" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>





