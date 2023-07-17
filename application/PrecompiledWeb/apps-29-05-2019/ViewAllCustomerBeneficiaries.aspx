<%@ page language="C#" masterpagefile="~/ReportMaster.master" autoeventwireup="true" inherits="General_ViewUsers, App_Web_cdgctkdq" title="CUSTOMER BENEFICIARIES" enableviewstatemac="false" %>
 <%@ Register 
 Assembly="AjaxControlToolkit" 
 Namespace="AjaxControlToolkit" 
 TagPrefix="ajaxToolkit" %>
 <%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 98%; height: 2px">
                <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptGlobalization="true"
                    EnableScriptLocalization="true">
                </ajaxToolkit:ToolkitScriptManager><table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                        style="width: 90%">
                    <tr style="color: #000000">
                        <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                                                text-align: center">
                            &nbsp;CUSTOMER BENEFICIARY</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 5px">
                <table align="center" cellpadding="0" cellspacing="0" style="width: 95%; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;">
                    <tr>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 30%; height: 18px;
                            text-align: center">
                            Search String(Names)</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 29%; height: 18px;
                            text-align: center">
                            Phone Number</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; height: 18px;
                            text-align: center" colspan="2">
                            Beneficary Type</td>
                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 17%; height: 18px;
                            text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td class="ddcolortabsline2" colspan="5" style="vertical-align: middle; text-align: center; height: 1px;">
                            </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; width: 30%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            &nbsp;<asp:TextBox ID="txtSearch" runat="server" style="font: menu" Width="90%"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 29%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:TextBox ID="txtSearchPhone" runat="server" Style="font: menu" Width="90%"></asp:TextBox></td>
                        <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6;
                            border-bottom-width: 1px; border-bottom-color: #617da6; vertical-align: middle;
                            border-top-color: #617da6; height: 23px; text-align: center; border-right-width: 1px;
                            border-right-color: #617da6">
                            &nbsp;<asp:DropDownList ID="cboBeneficiaryType" runat="server" CssClass="InterfaceDropdownList"
                                OnDataBound="cboBeneficiaryType_DataBound" Width="85%" style="font: menu">
                            </asp:DropDownList></td>
                        <td style="vertical-align: middle; width: 17%; height: 23px; text-align: center; border-top-width: 1px; border-left-width: 1px; border-left-color: #617da6; border-bottom-width: 1px; border-bottom-color: #617da6; border-top-color: #617da6; border-right-width: 1px; border-right-color: #617da6;">
                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                Text="Search" Width="85px" style="font: menu" />&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px;">
            <hr />
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 98%; height: 1px">
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View3" runat="server">
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
                    </asp:View>
                </asp:MultiView></td>
        </tr>
        <tr>
            <td style="width: 98%; height: 2px">
                &nbsp;<asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnItemCommand="DataGrid1_ItemCommand"
                    OnPageIndexChanged="DataGrid1_PageIndexChanged" Width="100%" style="text-align: justify; font: menu; border-right: #617da6 1px solid; border-top: #617da6 1px solid; border-left: #617da6 1px solid; border-bottom: #617da6 1px solid;" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Justify">
                    <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                    <ItemStyle BackColor="InactiveCaption" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="RecordID" HeaderText="Beneficary ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="No." HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="CustomerCode" HeaderText="CustomerCode" Visible="False">
                            <HeaderStyle Width="5%" />
                            
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                        </asp:BoundColumn>
                        <asp:ButtonColumn CommandName="btnEdit" DataTextField="FullName" HeaderText="Edit"
                            Text="FullName" Visible="false">
                            <HeaderStyle Width="13%" />
                              <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="Blue"  Width="13%" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="FullName" HeaderText="Full Name">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Title" HeaderText="Title">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Phone" HeaderText="Telephone">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        
                        <asp:BoundColumn DataField="NetworkCode" HeaderText="Network" >
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TypeName" HeaderText="Type">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Location" HeaderText="Location">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Active" HeaderText="Active">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RecordedBy" HeaderText="Created By" Visible="true">
                            <HeaderStyle Width="5%" />
                            <ItemStyle Width="5%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Created" DataField="Date">
                            <HeaderStyle Width="15%" />
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#FEFECE" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></asp:View>
                    <asp:View ID="View2" runat="server">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable2 "
                            style="width: 90%">
                            <tr style="color: #000000">
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="4" style="vertical-align: top;
                                    height: 40px; text-align: left">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="InterfaceInforTable "
                                        style="width: 90%">
                                        <tr style="color: #000000">
                                            <td class="InterfaceHeaderLabel" colspan="2" style="vertical-align: top; height: 19px;
                                                text-align: center">
                                                EDIT CUSTOMER BENEFICIARY</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterfaceInforFonts21" colspan="4" style="vertical-align: top; height: 2px;
                                    background-color: white; text-align: left">
                                </td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    text-align: left">
                                    Customer Code</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left">
                                    <asp:TextBox ID="txtCode" runat="server" BackColor="LemonChiffon" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth" OnTextChanged="txtCode_TextChanged"></asp:TextBox></td>
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    width: 61px; text-align: left">
                                    Phone</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                    text-align: left">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    text-align: left; height: 24px;">
                                    First Name</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtFname" runat="server" BackColor="LemonChiffon" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    width: 61px; text-align: left; height: 24px;">
                                    Email</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                    text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    height: 24px; text-align: left">
                                    Last Name</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; height: 24px;
                                    text-align: left">
                                    <asp:TextBox ID="txtlname" runat="server" BackColor="LemonChiffon" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    width: 61px; height: 24px; text-align: left">
                                    Location</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                    height: 24px; text-align: left">
                                    <asp:TextBox ID="txtLocation" runat="server" CssClass="DataEntryFormTableTextbox DataEntryFormTableTextboxWidth"></asp:TextBox></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    height: 22px; text-align: left">
                                    Type</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; height: 22px;
                                    text-align: left">
                                    <asp:DropDownList ID="cboType" runat="server" BackColor="LemonChiffon" CssClass="SystemDropdownListOthers DataEntryFormTableTextboxWidth"
                                        OnDataBound="cboType_DataBound" Width="90%">
                                    </asp:DropDownList></td>
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="1" style="vertical-align: top;
                                    width: 61px; height: 22px; text-align: left">
                                    Active</td>
                                <td class="InterfaceInforFonts21" colspan="1" style="vertical-align: top; width: 30%;
                                    height: 22px; text-align: left">
                                    <asp:CheckBox ID="chkActive" runat="server" /></td>
                            </tr>
                            <tr style="color: #000000">
                                <td class="InterfaceInforFonts22 InterfaceTableColor" colspan="4" style="padding-bottom: 25px;
                                    vertical-align: top; padding-top: 25px; text-align: center">
                                    <asp:Button ID="btnSave" runat="server" CssClass="DataEntryFormTableButtons" Font-Bold="True"
                                        Height="30px" OnClick="btnSave_Click" Text="SAVE" Width="140px" /></td>
                            </tr>
                        </table>
                        <br />
                        <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
                        <asp:Label ID="lblCustomerCode" runat="server" Text="0" Visible="False"></asp:Label></asp:View>
                </asp:MultiView></td>
        </tr>
    </table>
    <br />
    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"
        visible="False"></cr:crystalreportviewer>
    <br />
</asp:Content>

